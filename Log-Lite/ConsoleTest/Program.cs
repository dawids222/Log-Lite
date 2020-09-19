using Log_Lite.LogFormatter;
using Log_Lite.Logger;
using Log_Lite.LogWriter;
using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var formatter = new BasicLogFormatter();
            var writer = new ConsoleLogWriter(formatter);
            var logger = new Logger(writer);
            logger.Info("no i jak?");
            MyLogger.Instance.Info("Rzpoczęcie działania programu");

            var x = GetNumberFromUser();
            var y = GetNumberFromUser();

            var divided = Divide(x, y);
            Console.WriteLine(divided);

            Console.ReadKey();
            MyLogger.Instance.Info("Zakończenie działania programu");
        }

        static int GetNumberFromUser()
        {
            Console.WriteLine("Prosze podać liczbę");
            var number = Convert.ToInt32(Console.ReadLine());

            MyLogger.Instance.Info($"Użytkownik wpisał {number}");

            return number;
        }

        static int Divide(int devided, int divider)
        {
            try
            {
                return devided / divider;
            }
            catch (DivideByZeroException)
            {
                MyLogger.Instance.Error($"Próba dzielenia przez 0!");
                return 0;
            }
        }
    }
}
