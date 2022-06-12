namespace Advance_dotnet_concept.C_Sharp_Dotnet.ThreadSyncronization
{
    internal class SyncronizationMain
    {
        public static void Main()
        {
            var a = new LockAndMonitor();
            Parallel.For(0, 5, i =>
             {
                 a.MonitorDoWorkThreadSafe();
             });

            //Parallel.For(0, 5, i =>
            //{
            //    a.LockDoWorkThreadSafe();
            //});
        }
    }
}
