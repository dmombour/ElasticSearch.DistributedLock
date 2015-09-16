using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Nest;

namespace ElasticSearch
{
    /// <summary>
    /// This class is used as a way to create a cross machine lock using elastic search as it's singular resource manager.
    /// </summary>
    public class ElasticSearchDistributedLock : IDisposable
    {
        private string mNamedLock = "";
        private IElasticClient mClient;
        private bool mAquired;
        private Stopwatch mStopwatch;
        private static string mOwner;
        private readonly string mIndex;
        private const string mType = "reference";

        public bool IsDisposed { get; private set; }        

        /// <summary>
        /// C/tor
        /// </summary>
        /// <param name="namedLock">The name of the lock to create. REQUIRED</param>
        /// <param name="client">An instance of an elastic search NEST client. Will be created automatically by default. Use this if connecting to an elastic cluster.</param>
        /// <param name="indexName">The name of the elastic search index. Default = distributedlocks</param>
        public ElasticSearchDistributedLock(string namedLock, IElasticClient client = null, string indexName = "distributedlocks")
        {
            mIndex = indexName;
            mNamedLock = namedLock;
            mStopwatch = new Stopwatch();

            //Only do this once per process.. 
            if (string.IsNullOrEmpty(mOwner))
            {
                mOwner = BuildOwnerIdentifier();
            }

            //Create a default client if none handed in
            if (client == null)
            {                
                var settings = new ConnectionSettings(defaultIndex: mIndex);
                mClient = new ElasticClient(settings);
            }
            else
            {
                mClient = client;
            }            
        }

        /// <summary>
        /// Aquires a lock on the named resource and returns true if the lock was obtained. False otherwise
        /// </summary>
        /// <param name="numberOfRetries">Number of times to retry getting the lock. Default = 0</param>
        /// <param name="retryWaitTimeInMs">Wait time in ms before retrying the lock.</param>
        /// <param name="Ttlms">Time to Live. This is to prevent crashing processes holding locks that will never expire. Default is 1 minute. It is highly recommended to keep this short as possible</param>
        /// <returns></returns>
        public bool Aquire(int numberOfRetries = 0, int retryWaitTimeInMs = 250, int Ttlms = 60000)
        {
            if (IsDisposed) throw new ObjectDisposedException(mNamedLock);

            mStopwatch.Start();

            mAquired = false;          

            //This is the UPSERT lock document.. done in a func so we can repeat it over again in a retry
            var upsert = new Func<bool>(() => {

                var ttlTicks = DateTimeOffset.Now.AddMilliseconds(Ttlms).Ticks;
                var nowTicks = DateTimeOffset.Now.Ticks;

                var response = mClient.Update<object>(u => u
                .Index(mIndex)
                .Type(mType)
                .Id(mNamedLock)
                //This script ensures that the owner is the same.. so that a single process can grab a named lock over again.. with protection for TTL abandoned cases
                .Script("if (ctx._source.owner != owner && ctx._source.ttl > now ) { assert false } else if (ctx._source.ttl < now) { ctx._source.owner = owner; ctx._source.ttl = ttl; } else { ctx.op = 'noop' }")
                .Params(p => p
                  .Add("owner", mOwner)
                  .Add("now", nowTicks)
                  .Add("ttl", ttlTicks)
                )
                .ScriptedUpsert(true)
                .Upsert(new { owner = mOwner, aquired = DateTimeOffset.Now, ttl = ttlTicks })
                );

                return response.IsValid;

            });

            var retryCount = 0;
            var retry = false;

            do
            {
                try
                {
                    mAquired = upsert();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("ERR:" + ex.ToString());
                }

                retryCount++;

                if (!mAquired && retryCount <= numberOfRetries)
                {
                    Console.WriteLine(string.Format("DistributedLock: FAILED TO AQUIRE LOCK. Time in lock: {0} Retry Count: {1}", mStopwatch.Elapsed.ToString(), retryCount));
                    Thread.Sleep(retryWaitTimeInMs);
                    retry = true;
                }
                else
                {
                    retry = false;
                }

            } while (retry);

            return mAquired;
        }

        /// <summary>
        /// Releases the lock if aquired
        /// </summary>
        /// <param name="numberOfRetries">Number of times to retry on a failed release</param>
        /// <param name="retryWaitTimeInMs">Wait time in ms to re-attempt the release</param>
        /// <returns></returns>
        public bool Release(int numberOfRetries = 0, int retryWaitTimeInMs = 250)
        {
            if (IsDisposed) throw new ObjectDisposedException(mNamedLock);

            if (mAquired)
            {
                var release = new Func<bool>(() => {

                    var released = false;

                    var response = mClient.Delete(mIndex, mType, mNamedLock);
                    if (response.ConnectionStatus.HttpStatusCode == 404)
                    {
                        released = true;
                    }
                    else
                    {
                        released = response.IsValid;
                    }

                    return released;

                });

                var retryCount = 0;
                var retry = false;

                do
                {

                    try
                    {
                        mAquired = !release();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error:" + ex.ToString());
                    }

                    retryCount++;

                    if (mAquired && retryCount <= numberOfRetries)
                    {
                        Console.WriteLine(string.Format("DistributedLock: FAILED TO RELEASE LOCK. Time in lock: {0} Retry Count: {1}", mStopwatch.Elapsed.ToString(), retryCount));
                        Thread.Sleep(retryWaitTimeInMs);
                        retry = true;
                    }
                    else
                    {
                        retry = false;
                    }

                } while (retry);

            }

            mStopwatch.Stop();

            Console.WriteLine(string.Format("DistributedLock: Time in lock: {0}", mStopwatch.Elapsed.ToString()));

            return !mAquired;
        }
        
        /// <summary>
        /// Dispose the lock which will automatically Release
        /// </summary>
        public void Dispose()
        {
            //Swallow the dispose... the TTL will ultimatly allow the lock to release. If you need explicit awareness of the release then use
            //the explicit Release() call.
            try
            {
                Release();
            }
            catch { }

            IsDisposed = true;
        }

        private static string BuildOwnerIdentifier()
        {
            //Why is this the identifier..
            //If we are simply dealing with a single machine.. then a MUTEX is a better solution
            //Since we are going across multi machines... this allows for Multi-process.. multi-machine lock granularity.
            var p = Process.GetCurrentProcess();
            return string.Format("{0}_{1}_{2}", p.MachineName, p.ProcessName, p.Id);
        }
    }
}
