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
            var watch = System.Diagnostics.Stopwatch.StartNew();
            
            string directory = Path.GetDirectoryName(Path.GetDirectoryName(Directory.GetCurrentDirectory()));
            var text = File.ReadLines(directory+@"\map.txt").ToList();
            var map = new GridMap(text);
            Console.WriteLine($"You have successfully created a {map.XLength}x{map.XLength} grid map");

            var mapEvaluator= new GridMapEvaluator(map);
            Console.WriteLine();
            Console.WriteLine("Working...");
            for(int i=0; i<map.LocaleArray.Length; i++)
            {
                var locale = map.LocaleArray[i];
                if (locale.Value == 0)
                {
                    continue;
                }

                if (locale.Encountered)
                {
                    continue;
                }

                mapEvaluator.EvaluatePathsFromLocale(map.LocaleArray[i]);
            }

            mapEvaluator.DisplayResults();
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;
            Console.WriteLine($"ExecutionTime: {elapsedMs/1000.0} seconds");
            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
        }


    }
}
