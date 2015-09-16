using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ElasticSearch;

namespace ElasticSearch.DistributedLock.TestApp
{
    public partial class Form1 : Form
    {
        ElasticSearchDistributedLock mLock;

        public Form1()
        {
            InitializeComponent();
        }

        private void buttonAquire_Click(object sender, EventArgs e)
        {
            mLock = new ElasticSearchDistributedLock(textBoxName.Text);
            var result = mLock.Aquire();

            ShowMessage("Aquire: " + result.ToString());
        }

        private void ShowMessage(string msg)
        {
            textBoxLog.AppendText(msg);
            textBoxLog.AppendText(Environment.NewLine);
        }

        private void buttonRelease_Click(object sender, EventArgs e)
        {
            if (mLock != null)
            {
                var result = mLock.Release();

                ShowMessage("Release: " + result.ToString());
            }
            else
            {
                ShowMessage("Release: NO CURRENT LOCK");
            }
        }

        private async void buttonChaos_Click(object sender, EventArgs e)
        {
            var time = TimeSpan.FromSeconds(10);

            if (!string.IsNullOrEmpty(textBoxChaos.Text))
            {
                TimeSpan.TryParse(textBoxChaos.Text, out time);
            }

            var threads = 10;

            if (!string.IsNullOrEmpty(textBoxChaosThreads.Text))
            {
                int.TryParse(textBoxChaosThreads.Text, out threads);
            }

            ShowMessage(string.Format("CHAOS: Starting... run for {0} with {1} thread(s)", time.ToString(), threads));

           var result =  await RunTest(time, threads);

            ShowMessage(string.Format("CHAOS: Finished... ran for {0}", result.Elapsed));
            ShowMessage(string.Format("Success:{0}", result.Success));
            ShowMessage(string.Format("Failures:{0}", result.Failure));
            ShowMessage(string.Format("Locks per second:{0}", (result.Success / result.Elapsed.TotalSeconds)));

        }

        private Task<ChaosResult> RunTest(TimeSpan duration, int numberTasks)
        {
            return Task.Factory.StartNew<ChaosResult>(() =>
            {

                var noop = new Action(() => { });
                var success = 0;
                var failure = 0;

                var sw = new Stopwatch();
                sw.Start();

                while (sw.Elapsed < duration)
                {
                    Parallel.For(1, numberTasks, (a) =>
                    {
                        using (var dLock = new ElasticSearchDistributedLock("lock_" + a.ToString()))
                        {
                            if (dLock.Aquire())
                            {
                                noop();
                                success++;
                            }
                            else
                            {
                                failure++;
                            }
                        }
                    });
                }

                sw.Stop();

                var result = new ChaosResult();
                result.Elapsed = sw.Elapsed;
                result.Success = success;
                result.Failure = failure;

                return result;

            });
        }

        private class ChaosResult
        {
            public TimeSpan Elapsed { get; set; }
            public int Success { get; set; }
            public int Failure { get; set; }

        }

    }

}
