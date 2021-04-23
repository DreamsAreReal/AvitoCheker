using System;

namespace AvitoChecker.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Fuck work. Ok?
            // Rewrite.
            Console.WriteLine("Максимальное количество потоков?");
            Startup.MaxThreadCount = int.Parse(Console.ReadLine());


            Startup.Configure();
            Console.WriteLine("Press ANY key to exit");
            Console.ReadKey();
        }
    }
}
