namespace Advance_dotnet_concept.C_Sharp_Dotnet.ThreadSyncronization
{
    internal class SyncronizationMain
    {
        public static void Main()
        {
            var lockAndMonitor = new LockAndMonitor();
            //Parallel.For(0, 5, i =>
            // {
            //     lockAndMonitor.MonitorDoWorkThreadSafe();
            // });

            //Parallel.For(0, 5, i =>
            //{
            //    lockAndMonitor.LockDoWorkThreadSafe();
            //});

            var mutexAndSemaphore = new MutexAndSemaphore();
            //Parallel.For(0, 5, i =>
            //{
            //    mutexAndSemaphore.MutexThreadSafe();
            //});

            Parallel.For(0, 5, i =>
            {
                mutexAndSemaphore.SemaphoreDoWork();
            });

        }
    }
}
