using Advance_dotnet_concept.CommonHelpers.ExtensionMethodHelper;

namespace Advance_dotnet_concept.C_Sharp_Dotnet.ThreadSyncronization
{
    /// <summary>
    /// Lock, Monitor and Mutex are exclusive where Semaphore and SemphoreSlim are non-exclusive locks.
    /// Mutex: Same like Lock but it can work accross different processes.
    /// Semaphore:  allow for a specified number of threads to work with the same resources at the same time.
    /// </summary>
    internal class MutexAndSemaphore
    {
        private Mutex _mutex;
        private Semaphore _semaphore;
        public MutexAndSemaphore()
        {
            _mutex = new Mutex();
            _semaphore = new Semaphore(1,3); //Semaphore(initial thread count, max thread count)
        }
        /// <summary>
        /// Mutex(mutual exclusion) is very similar to lock/Monitor. 
        /// The difference is that it can work across multiple processes.
        /// Acquiring and releasing an uncontended Mutex takes a few microseconds — 
        /// about 50 times slower than a lock.
        /// </summary>
        public void MutexThreadSafe()
        {
            try
            {
                bool isSafe = _mutex.WaitOne();
                if (isSafe)
                {
                    $"This is from {System.Reflection.MethodBase.GetCurrentMethod().Name}".Dump();
                    Task.Delay(2000).Wait();
                    $"I have waited for 2 sec {System.Reflection.MethodBase.GetCurrentMethod().Name}".Dump();
                }
            }
            finally
            {
                _mutex.ReleaseMutex();
            }
        }

        public void SemaphoreDoWork()
        {
            try
            {
                $"Thread{Thread.CurrentThread.ManagedThreadId} wants to enter".Dump();
                //The code will stop executing here and wait until the lock is released by the previous thread. 
                _semaphore.WaitOne();
                $"Thread{Thread.CurrentThread.ManagedThreadId} is entered".Dump();
                Task.Delay(3000);
                $"Thread{Thread.CurrentThread.ManagedThreadId} is leaving..".Dump();
            }
            finally {
                _semaphore.Release();
            }

        }
    }
}
