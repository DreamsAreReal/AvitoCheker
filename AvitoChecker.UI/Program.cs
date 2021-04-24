using System;
using System.Threading;
using System.Threading.Tasks;

namespace AvitoChecker.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            // Fuck work. Ok?
            // Todo: Rewrite.
            Console.WriteLine("Максимальное количество потоков?");
            Startup.MaxThreadCount = int.Parse(Console.ReadLine());

            
            Task.Run((() =>
            {
                Startup.Configure();
                
            }));


            while (true)
            {
                Console.ReadKey();
            }
        }
    }
}
