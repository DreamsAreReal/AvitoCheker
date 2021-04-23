using System;

namespace AvitoChecker.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            Startup.Configure();
            Console.WriteLine("Press ANY key to exit");
            Console.ReadKey();
        }
    }
}
