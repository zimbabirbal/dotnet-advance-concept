using Advance_dotnet_concept.CommonHelpers.ExtensionMethodHelper;
using System.Diagnostics;

namespace Advance_dotnet_concept.C_Sharp_Dotnet.Async_Await
{
    internal class MakeTeaAsyncAwaitExample
    {
        public static async Task Main()
        {
            Stopwatch stopwatch = Stopwatch.StartNew();
            //Syncronous call
            //total taken time ~ 14s
            //PrepareTea();

            //total taken time ~ 14s
            //when every await found the current processing thread will return and delegate the task to
            //other threads or can again picks by same thread it doesn' garauntee it will by other threads
            //var task = PrepareTeaTaskDelayAsync();
            //$"I'm waiting for here".Dump();     
            //await task;

            //total taken time ~ 12s
            await PrepareTeaTaskWithAsync();
            stopwatch.Stop();
            $"\nTotal time elapsed: {stopwatch.ElapsedMilliseconds}ms".Dump();
            Console.ReadLine();
        }

        /// <summary>
        /// Syncronous call
        /// </summary>
        private static void PrepareTeaSyncronously()
        {
            $"Lit a fire".Dump();
            $"Put a kettle on fire.".Dump();
            Thread.Sleep(1000);
            $"Pour water in kettle".Dump();
            $"Take a cup".Dump();
            Thread.Sleep(1000);
            $"Add sugar and tea".Dump();
            Thread.Sleep(2000);
            $"Wait for water to boil".Dump();
            Thread.Sleep(7000);
            $"Pour water in cup and stir it".Dump();
            Thread.Sleep(3000);
            $"Voila!!! cup of tea is ready".Dump();
        }

        /// <summary>
        /// Used async/await with task delay
        /// Main thread will not block here
        /// </summary>
        /// <returns></returns>
        private static async Task PrepareTeaTaskDelayAsync()
        {
            $"Lit a fire".Dump();
            $"Put a kettle on fire.".Dump();
            await Task.Delay(1000);
            $"Pour water in kettle".Dump();
            $"Take a cup".Dump();
            await Task.Delay(1000);
            $"Add sugar and tea".Dump();
            await Task.Delay(2000);
            $"Wait for water to boil".Dump();
            await Task.Delay(7000);
            $"Pour water in cup and stir it".Dump();
            await Task.Delay(3000);
            $"Voila!!! cup of tea is ready".Dump();
        }

        /// <summary>
        /// Optimum Async/Await
        /// </summary>
        /// <returns></returns>
        public static async Task PrepareTeaTaskWithAsync()
        {
            $"Lit a fire".Dump();
            $"Put a kettle on fire.".Dump();
            var task1 = Task.Delay(1000);
            $"Pour water in kettle".Dump();
            $"Take a cup".Dump();
            var task2 = Task.Delay(1000);
            $"Add sugar and tea".Dump();
            var task3 = Task.Delay(2000);

            Task.WaitAll(task1, task2, task3);

            $"Wait for water to boil".Dump();
            await Task.Delay(7000);
            $"Pour water in cup and stir it".Dump();
            await Task.Delay(3000);
            $"Voila!!! cup of tea is ready".Dump();

        }
    }
}
