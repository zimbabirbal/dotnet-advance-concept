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
            //when every await found the current processing thread will return and delegate the task to other thread
            //which avaliable from threads pool.
            //var task = PrepareTeaTaskDelayAsync();
            //$"I'm waiting for here".DumpConsole();     
            //await task;

            //total taken time ~ 12s
            await PrepareTeaTaskWithAsync();
            stopwatch.Stop();
            $"\nTotal time elapsed: {stopwatch.ElapsedMilliseconds}ms".DumpConsole();
            Console.ReadLine();
        }

        /// <summary>
        /// Syncronous call
        /// </summary>
        private static void PrepareTeaSyncronously()
        {
            $"Lit a fire".DumpConsole();
            $"Put a kettle on fire.".DumpConsole();
            Thread.Sleep(1000);
            $"Pour water in kettle".DumpConsole();
            $"Take a cup".DumpConsole();
            Thread.Sleep(1000);
            $"Add sugar and tea".DumpConsole();
            Thread.Sleep(2000);
            $"Wait for water to boil".DumpConsole();
            Thread.Sleep(7000);
            $"Pour water in cup and stir it".DumpConsole();
            Thread.Sleep(3000);
            $"Voila!!! cup of tea is ready".DumpConsole();
        }

        /// <summary>
        /// Used async/await with task delay
        /// Main thread will not block here
        /// </summary>
        /// <returns></returns>
        private static async Task PrepareTeaTaskDelayAsync()
        {
            $"Lit a fire".DumpConsole();
            $"Put a kettle on fire.".DumpConsole();
            await Task.Delay(1000);
            $"Pour water in kettle".DumpConsole();
            $"Take a cup".DumpConsole();
            await Task.Delay(1000);
            $"Add sugar and tea".DumpConsole();
            await Task.Delay(2000);
            $"Wait for water to boil".DumpConsole();
            await Task.Delay(7000);
            $"Pour water in cup and stir it".DumpConsole();
            await Task.Delay(3000);
            $"Voila!!! cup of tea is ready".DumpConsole();
        }

        /// <summary>
        /// Optimum Async/Await
        /// </summary>
        /// <returns></returns>
        public static async Task PrepareTeaTaskWithAsync()
        {
            $"Lit a fire".DumpConsole();
            $"Put a kettle on fire.".DumpConsole();
            var task1 = Task.Delay(1000);
            $"Pour water in kettle".DumpConsole();
            $"Take a cup".DumpConsole();
            var task2 = Task.Delay(1000);
            $"Add sugar and tea".DumpConsole();
            var task3 = Task.Delay(2000);

            Task.WaitAll(task1, task2, task3);

            $"Wait for water to boil".DumpConsole();
            await Task.Delay(7000);
            $"Pour water in cup and stir it".DumpConsole();
            await Task.Delay(3000);
            $"Voila!!! cup of tea is ready".DumpConsole();

        }
    }
}
