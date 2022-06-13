using Advance_dotnet_concept.CommonHelpers.ExtensionMethodHelper;

namespace Advance_dotnet_concept.C_Sharp_Dotnet.ThreadSyncronization
{
    /// <summary>
    /// Lock, Monitor and Mutex are exclusive where Semaphore and SemphoreSlim are non-exclusive locks.
    /// Mutex: Same like Lock but it can work accross different processes.
    /// Semaphore:  allow for a specified number of threads to work with the same resources at the same time.
    /// SemaphoreSlim: Represents a lightweight and optimized alternative to Semaphore that limits the number of threads 
    /// that can access a resource or pool of resources concurrently.
    /// </summary>
    internal class MutexAndSemaphore
    {
        private Mutex _mutex;
        private Semaphore _semaphore;
        private static SemaphoreSlim _semaphoreSlim;
        public MutexAndSemaphore()
        {
            _mutex = new Mutex();
            _semaphore = new Semaphore(1, 3); //Semaphore(initial thread count, max thread count)
            _semaphoreSlim = new SemaphoreSlim(0, 3);
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
            finally
            {
                _semaphore.Release();
            }

        }

        public void SemaphoreSlimDoWork()
        {
            $"SemaphoreSlim: count{_semaphoreSlim.CurrentCount}".Dump();

            Task[] task = new Task[5];

            for (int i = 0; i < task.Length; i++)
            {
                task[i] = Task.Run(() =>
                {
                    $"{Task.CurrentId} begins and wait for Semaphore".Dump();
                    _semaphoreSlim.Wait();

                    int semaphoreCount;
                    try
                    {
                        $"Task {Task.CurrentId} enter the semaphore.".Dump();
                        Thread.Sleep(2000);
                    }
                    finally
                    {
                        semaphoreCount = _semaphoreSlim.CurrentCount;
                    }
                    $"Task {Task.CurrentId} releases the Semaphore, previous count={semaphoreCount}".Dump();
                });
            }
            _semaphoreSlim.Release(3);
            $"{Task.CurrentId} Tasks can enter the semaphore".Dump();

            Task.WaitAll();
        }
    }
}
