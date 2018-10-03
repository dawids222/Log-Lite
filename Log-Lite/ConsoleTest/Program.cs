using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MyLogger.Instance.Info("Test");

            //var watch = System.Diagnostics.Stopwatch.StartNew();

            //for (int i = 0; i < 10000; i++)
            //{
            //    MyLogger.Instance.Info("Test");
            //}

            //watch.Stop();
            //var elapsedMs = watch.ElapsedMilliseconds;
            //Console.WriteLine(elapsedMs);

            Console.ReadKey();
        }
    }
}
