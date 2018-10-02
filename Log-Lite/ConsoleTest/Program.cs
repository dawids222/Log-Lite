using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            MyLogger.Instance.Info("test");
            MyLogger.Instance.Warning("test");

            Console.ReadKey();
        }
    }
}
