using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace ParaExample0324
{
    public class ParallelDemo
    {
        private Stopwatch stopWatch = new Stopwatch();

        public void Run1()
        {
            Thread.Sleep(2000);
            Console.WriteLine("Task 1 is cost 2 sec");
        }
        public void Run2()
        {
            Thread.Sleep(3000);
            Console.WriteLine("Task 2 is cost 3 sec");
        }

        public void ParallelInvokeMethod()
        {
            stopWatch.Start();
            Parallel.Invoke(Run1, Run2);
            stopWatch.Stop();
            Console.WriteLine("Parallel run " + stopWatch.ElapsedMilliseconds + " ms.");

            stopWatch.Restart();
            Run1();
            Run2();
            stopWatch.Stop();
            Console.WriteLine("Normal run " + stopWatch.ElapsedMilliseconds + " ms.");
        }
        public void ParallelForMethod()
        {
            stopWatch.Start();
            for (int i = 0; i < 10000; i++)
            {
                for (int j = 0; j < 60000; j++)
                {
                    int sum = 0;
                    sum += i;
                }
            }
            stopWatch.Stop();
            Console.WriteLine("NormalFor run " + stopWatch.ElapsedMilliseconds + " ms.");

            stopWatch.Reset();
            stopWatch.Start();
            Parallel.For(0, 10000, item =>
            {
                for (int j = 0; j < 60000; j++)
                {
                    int sum = 0;
                    sum += item;
                }
            });
            stopWatch.Stop();
            Console.WriteLine("ParallelFor run " + stopWatch.ElapsedMilliseconds + " ms.");

        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            ParallelDemo pDemo = new ParallelDemo();

            var task1 = new Task(() =>
            {
                Console.WriteLine("Task 1 Begin");
                System.Threading.Thread.Sleep(2000);
                Console.WriteLine("Task 1 Finish");
            });
            var task2 = new Task(() =>
            {
                Console.WriteLine("Task 2 Begin");
                System.Threading.Thread.Sleep(3000);
                Console.WriteLine("Task 2 Finish");
            });


            task1.Start();
            task2.Start();
            var result = task1.ContinueWith<string>(task =>
            {
                Console.WriteLine("task1 finished!");
                return "This is task result!";
            });

            Console.WriteLine(result.Result.ToString());

            //Console.Read();

            //pDemo.ParallelInvokeMethod();


            pDemo.ParallelForMethod();


            Console.ReadKey();

        }
    }
}
