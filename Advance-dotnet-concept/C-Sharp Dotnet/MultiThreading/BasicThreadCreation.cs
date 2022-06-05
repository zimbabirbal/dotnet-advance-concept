using Advance_dotnet_concept.CommonHelpers.ExtensionMethodHelper;
using System.ComponentModel;
using System.Reflection;

namespace Advance_dotnet_concept.C_Sharp_Dotnet.MultiThreading
{
    /// <summary>
    /// Examples shows various way to delegate work in background thread.
    /// </summary>
    internal class BasicThreadCreation
    {
        public delegate void MyDelegate(string name);
        public BasicThreadCreation()
        {
            UsingBasicThread();
            //UsingThreadPool();
            //UsingBackgroundWorker();
            //UsingBeginInvoke();
        }

        /// <summary>
        /// Executes the specified delegate asynchronously with the specified arguments on the thread
        /// Invoke: will execute on Main Thread
        /// BeginInvoke: will execute in background thread
        /// </summary>
        private void UsingBeginInvoke()
        {
            MyDelegate myDelegate = new MyDelegate(MyTestMethod);
            myDelegate.BeginInvoke("BeginInvoke", null, null);
        }

        public void MyTestMethod(string name)
        {
            $"ThreadId:{Thread.CurrentThread.ManagedThreadId} This is from {name}-{MethodBase.GetCurrentMethod().Name}".Dump();
        }

        /// <summary>
        /// Executes an operation on a separate thread
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        private void UsingBackgroundWorker()
        {
            BackgroundWorker backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += BackgroundWorker_DoWork;
            backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;
            backgroundWorker.RunWorkerAsync(); //start executing of work in background;
        }

        private void BackgroundWorker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            if(!e.Cancelled)
                $"ThreadId:{Thread.CurrentThread.ManagedThreadId} execution completed".Dump();

            $"ThreadId:{Thread.CurrentThread.ManagedThreadId} execution failed".Dump();
        }

        private void BackgroundWorker_DoWork(object? sender, DoWorkEventArgs e)
        {
            $"Executing from {MethodBase.GetCurrentMethod().Name}".Dump();
            DoWork1();
        }

        /// <summary>
        /// Provides a pool of threads that can be used to execute tasks, post work items, process asynchronous I/O, 
        /// wait on behalf of other threads, and process timers.
        /// </summary>
        private void UsingThreadPool()
        {
            $"Executing from {MethodBase.GetCurrentMethod().Name}".Dump();
            ThreadPool.QueueUserWorkItem(ExecuteMethod);
        }

        /// <summary>
        /// Standard basic simple thread creation in .net
        /// </summary>
        private void UsingBasicThread()
        {
            Thread thread1 = new Thread(DoWork1);
            Thread thread2 = new Thread(DoWork2);
            thread1.Priority = ThreadPriority.AboveNormal; //set a thread priority
            thread1.Start();
            thread2.Start();
        }

        public void ExecuteMethod(object state)
        {
            DoWork1();
        }

        public void DoWork1()
        {
            for(int i = 0; i < 1000; i++)
            {
                $"ThreadId:{Thread.CurrentThread.ManagedThreadId}: This is from {MethodBase.GetCurrentMethod().Name} and number is {i}".Dump();
            }
        }
        public void DoWork2()
        {
            for (int i = 0; i < 1000; i++)
            {
                $"ThreadId:{Thread.CurrentThread.ManagedThreadId}: This is from {MethodBase.GetCurrentMethod().Name} and number is {i}".Dump();
            }
        }
    }
}
