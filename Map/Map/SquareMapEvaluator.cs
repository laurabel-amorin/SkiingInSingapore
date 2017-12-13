using System;
using System.Collections.Generic;

namespace Map
{
    class SquareMapEvaluator
    {
        private MapPath BestMapPath { get; set; }= new MapPath();

        private List<int> encountered= new List<int>();

        private readonly SquareMap map;

        public SquareMapEvaluator(SquareMap map)
        {
            this.map = map;
            BestMapPath = new MapPath();
        }

        public void UpdateConsole()
        {
            Console.WriteLine(
                    $"Current best path: Length: {BestMapPath.Length}, Vertical Drop: {BestMapPath.VerticalDrop}");

        }

        public void DisplayResults()
        {
            Console.WriteLine();
            Console.WriteLine("RESULTS:");
            Console.WriteLine($"Path length: {BestMapPath.Length}");
            Console.WriteLine($"Start at:{BestMapPath.Start.Value} in row {map.GetMapRow(BestMapPath.Start.Index)} column {map.GetMapColumn(BestMapPath.Start.Index)}");
            Console.WriteLine($"End at:{BestMapPath.End.Value} in row {map.GetMapRow(BestMapPath.End.Index)} column {map.GetMapColumn(BestMapPath.End.Index)}");
            Console.WriteLine($"Vertical Drop: {BestMapPath.VerticalDrop}");
            Console.WriteLine($"Email: {BestMapPath.Length}{BestMapPath.VerticalDrop}@redmart.com");
            Console.WriteLine();
        }

        public void EvaluatePathsFromLocale(Locale locale)
        {
            var currentPath = new MapPath
            {
                Start = locale
            };
            EvaluatePath(locale, currentPath);
        }

        private void EvaluatePath(Locale assessLocale, MapPath currentMapPath)
        {
            encountered.Add(assessLocale.Index);
            var viableNeighbours = map.GetViableNeighbours(assessLocale);
            if ((viableNeighbours == null) || (viableNeighbours.Count == 0))
            {
                currentMapPath.End = assessLocale;
                AssessPath(currentMapPath);
                return;
            }

            currentMapPath.Length++;
            foreach (var viableNeighbour in viableNeighbours)
            {
                var path = new MapPath
                {
                    Start = currentMapPath.Start,
                    Length = currentMapPath.Length
                };
                EvaluatePath(viableNeighbour, path);
            }
        }

        private void AssessPath(MapPath mapPath)
        {
            if ((BestMapPath.Start.Index == 0) && (BestMapPath.End.Index == 0))
            {
                BestMapPath = mapPath;
                UpdateConsole();
            }
            else
            {
                if (mapPath.Length < BestMapPath.Length)
                {
                    return;
                }

                if (mapPath.Length == BestMapPath.Length)
                {
                    if (mapPath.VerticalDrop <= BestMapPath.VerticalDrop)
                    {
                        return;
                    }
                }
                BestMapPath = mapPath;
                UpdateConsole();
            }         
        }
    }
}
