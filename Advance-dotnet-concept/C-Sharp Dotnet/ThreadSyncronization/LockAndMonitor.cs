using Advance_dotnet_concept.CommonHelpers.ExtensionMethodHelper;

namespace Advance_dotnet_concept.C_Sharp_Dotnet.ThreadSyncronization
{
    /// <summary>
    /// Lock and Monitor approaches are functionally identical. 
    /// The only difference is that lock is more compact and easier to use.
    /// </summary>
    internal class LockAndMonitor
    {
        private object lockObject = new object();

        /// <summary>
        /// //Lock the object and prevent any other code from executing the code below until the current thread is done executing.
        /// </summary>
        public void MonitorDoWorkThreadSafe()
        {
            Monitor.Enter(lockObject);
            try
            {
                $"This is from {System.Reflection.MethodBase.GetCurrentMethod().Name}".Dump();
                Task.Delay(2000).Wait();
                $"I have waited for 2 sec {System.Reflection.MethodBase.GetCurrentMethod().Name}".Dump();
            }
            finally
            {
                Monitor.Exit(lockObject);
            }
        }
        /// <summary>
        /// //The lock keyword is just a more compact and easier way of doing than with Monitor.
        /// </summary>
        public void LockDoWorkThreadSafe()
        {
            lock (lockObject)
            {
                $"This is from {System.Reflection.MethodBase.GetCurrentMethod().Name}".Dump();
                Task.Delay(2000).Wait();
                $"I have waited for 2 sec {System.Reflection.MethodBase.GetCurrentMethod().Name}".Dump();
            }
        }
    }
}
