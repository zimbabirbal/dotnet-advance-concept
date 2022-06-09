using Advance_dotnet_concept.CommonHelpers.ExtensionMethodHelper;

namespace Advance_dotnet_concept.C_Sharp_Dotnet.MultiThreading
{
    internal class BackgroundWorkWithTaskAndTPL
    {
        public BackgroundWorkWithTaskAndTPL()
        {
            //1. simple do your work in backgroud with async await, doesn't garauntee it will run in child thread. 
            //var task = SimpleBackgroudWorkWithAsyncAwait();
            //$"Task is completed with response: {task.Result}".Dump();

            //2. using Task do work in background
            //Task represents the asyncronous on going work (operation).
            //more optimized version than regular new Thread() creation with
            //more flexible Adding continuations (Task.ContinueWith)
            //Waiting for multiple tasks to complete(either all or any)
            //Capturing errors in the task and interrogating them later
            //Capturing cancellation(and allowing you to specify cancellation to start with)

            //SimpleTaskTest(); //Example1
            //TaskFactoryTest(); //Example2
            //TaskRunTest(); //Example3 ---should be used for CPU-bound methods

            //TPL
            //TPL provides a handy method for launching parallel tasks and essentially,
            //turning your .NET app into a multi-threaded application
            //Its a wrapper or higher-level abstraction over threading
            //ParallelInvoke();

            //TPL Parallel loops
            //We need to use parallel loops such as Parallel.For and Parallel.ForEach method to speed up operations where an expensive,
            //independent operation needs to be performed for each input of a sequence.
            //This may open the possibility of synchronization problems, becareful
            ParallelForLoopsTest();
        }

        private void ParallelForLoopsTest()
        {
            var listInteger = Enumerable.Range(1,100).ToList();
            var options = new ParallelOptions() { MaxDegreeOfParallelism = 2 };

            Parallel.For(0, 10, i =>
            {
                CalculateMandelbrot();
            });

            Parallel.ForEach(listInteger, options, i => {
                CalculateMandelbrot();
            });

        }

        /// <summary>
        /// Tasks are created and maintained implicitly by the framework.
        /// You should use this technique when you need to process some tasks which do not return any value and do not need more control over tasks.
        /// </summary>
        private void ParallelInvoke()
        {
            //Simple parrallel invoke
            //Parallel.Invoke(
            //    () => { CalculateMandelbrot(); },
            //    () => { CalculateMandelbrot(); },
            //    () => { CalculateMandelbrot(); }
            //);

            //with parrallel options
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;
            ParallelOptions parallelOptions = new ParallelOptions()
            {
                CancellationToken = cancellationToken,
                MaxDegreeOfParallelism = System.Environment.ProcessorCount
            };

            Parallel.Invoke(parallelOptions,
                () => { CalculateMandelbrot(); },
                () => { CalculateMandelbrot(); },
                () => { CalculateMandelbrot(); });


        }

        private async Task TaskRunTest()
        {
            //more info: https://blog.stephencleary.com/2013/11/taskrun-etiquette-examples-dont-use.html
            //smell bad code
            $"Hi from TaskRun".Dump();
            var result1 = await CalculateMandelbrot1Async();
            $"This is result1 {result1}".Dump();
            //good code
            var result2 = await CalculateMandelbrot2Async();
            $"This is result2 {result2}".Dump();
        }

        private void SimpleTaskTest()
        {
            Task t = new Task(() => TestMethod());
            t.Start();
            $"This is awesome from TaskOnly".Dump();
            t.ConfigureAwait(false);
        }

        private async void TaskFactoryTest()
        {
            var task = Task.Factory.StartNew(() => TestMethod());
            $"This is awesome from TaskFactory.".Dump();

            //Task.GetAwaiter().GetResult() is preferred over Task.Wait and Task.Result because it propagates exceptions rather than
            //wrapping them in an AggregateException. However, all three methods cause the potential for deadlock and thread pool starvation issues.
            //They should all be avoided in favor of async/await.
            //task.Wait(); Wait will synchronously block until the task completes
            //task.Result();
            //task.GetAwaiter().GetResult();

            //use this for best cases
            //await task; //await will asynchronously wait until the task completes //more at https://blog.stephencleary.com/2012/07/dont-block-on-async-code.html
            //this tells the Task that it can resume itself on any thread that is available instead of waiting for the
            //thread that originally created it. This will speed up responses and avoid many deadlocks.
            await task.ConfigureAwait(false); //this is recommended one
        }

        private void TestMethod()
        {
            $"This is from test method.".Dump();
        }

        private async Task<string> SimpleBackgroudWorkWithAsyncAwait()
        {
            HttpClient client = new HttpClient();
            var response = client.GetStringAsync(@"http://www.google.com");
            $"Do long running work".Dump(); //non blocking code
            return await response;
        }

        public int CalculateMandelbrot()
        {
            var ran = new Random().Next(1, 100);
            // Tons of work to do in here!
            for (int i = 0; i != 1000; ++i)
            {
                $"Id-{ran}: number = {i}.".Dump();
            }
            return ran;
        }

        //smell bad code
        public Task<int> CalculateMandelbrot1Async()
        {
            return Task.Run(() =>
            {
                // Tons of work to do in here!
                for (int i = 0; i != 10000000; ++i)
                    ;
                return 42;
            });
        }

        //best optimum way
        public Task<int> CalculateMandelbrot2Async()
        {
            return Task.Run(() => CalculateMandelbrot()); //do cpu bounded work
        }
    }
}
