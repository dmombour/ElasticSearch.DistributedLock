Using ElasticSearch we can create a distributed lock class to syncronize resources / operations across processes and machines.

using (var dLock = new ElasticSearchDistributedLock("mylock"))
{
	if (dLock.Aquire())
	{
		noop();
	}
}
