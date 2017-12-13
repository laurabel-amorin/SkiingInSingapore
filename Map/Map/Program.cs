using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.InteropServices;

namespace Map
{
    class Program
    {
        static void Main(string[] args)
        {
            string directory = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            string text = File.ReadAllText(directory+@"\map.txt");
            var map = new SquareMap(text, 1000);
            Console.WriteLine($"You have successfully created a {map.SideLength}x{map.SideLength} square map");

            List<int> encounteredIndices = new List<int>();
            var mapEvaluator= new SquareMapEvaluator(map);
            Console.WriteLine();
            Console.WriteLine("Working...");
            foreach (var locale in map.LocaleArray)
            {
                if (encounteredIndices.Contains(locale.Index))
                {
                    continue;
                }
                if (locale.Value == 0)
                {
                    continue;
                }
                mapEvaluator.EvaluatePathsFromLocale(locale);
            }

            mapEvaluator.DisplayResults();
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }


    }
}
