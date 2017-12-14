using System;
using System.Collections.Generic;
using System.Linq;

namespace Map
{
    class GridMapEvaluator
    {
        private MapPath BestMapPath { get; set; }= new MapPath();
        private Dictionary<int, bool> localePathsEncountered= new Dictionary<int, bool>();
        private readonly GridMap map;

        public GridMapEvaluator(GridMap map)
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
            //Console.WriteLine($"Start at:{BestMapPath.Start.Value} in row {map.GetMapRow(BestMapPath.Start.Index)} column {map.GetMapColumn(BestMapPath.Start.Index)}");
            //Console.WriteLine($"End at:{BestMapPath.End.Value} in row {map.GetMapRow(BestMapPath.End.Index)} column {map.GetMapColumn(BestMapPath.End.Index)}");
            Console.WriteLine($"Vertical Drop: {BestMapPath.VerticalDrop}");
            Console.WriteLine($"Email: {BestMapPath.Length}{BestMapPath.VerticalDrop}@redmart.com");
            Console.WriteLine();
        }

        public void EvaluatePathsFromLocale(Locale locale)
        {
            if (localePathsEncountered.ContainsKey(locale.Index))
            {
                return;
            }
            if ((locale.Value < BestMapPath.Length)||((locale.Value == BestMapPath.Length)&&(locale.Value<=BestMapPath.VerticalDrop)))
            {
                return;
            }            
            var currentPath = new MapPath
            {
                Start = locale
            };
            EvaluatePath(locale, currentPath);
        }

        private void EvaluatePath(Locale assessLocale, MapPath currentMapPath)
        {   
            /*if (bestLocalePaths.ContainsKey(assessLocale.Index))
            {
                var bestLocalePath = bestLocalePaths[assessLocale.Index];
                currentMapPath.Length += bestLocalePath.Length;
                currentMapPath.End = bestLocalePath.End;
                AssessPath(currentMapPath);
                ConfigureBestLocalePath(currentMapPath);
                return;
            }*/
            localePathsEncountered[assessLocale.Index] = true;
            var viableNeighbours = map.GetViableNeighbours(assessLocale);
            if ((viableNeighbours == null) || (viableNeighbours.Count == 0))
            {
                currentMapPath.Length++;
                currentMapPath.End = assessLocale;
                AssessPath(currentMapPath);
                //ConfigureBestLocalePath(currentMapPath);                
                return;
            }

            currentMapPath.Length++;
            foreach(var viableNeighbour in viableNeighbours)
            {
                 var path = new MapPath
                {
                    Start = currentMapPath.Start,
                    Length = currentMapPath.Length
                };
                EvaluatePath(viableNeighbour, path);
                /*var pathFromNeighbour = new MapPath
                {
                    Start = viableNeighbour,
                    End = path.End,
                    Length = path.Length- currentMapPath.Length
                };
                ConfigureBestLocalePath(pathFromNeighbour);*/
            }
        }

        /*private void ConfigureBestLocalePath(MapPath path)
        {
            var locale = path.Start;
            if ((!bestLocalePaths.ContainsKey(locale.Index))
                || (bestLocalePaths[locale.Index].Length < path.Length)
                || ((bestLocalePaths[locale.Index].Length == path.Length) && (bestLocalePaths[locale.Index].VerticalDrop < path.VerticalDrop)))
            {
                bestLocalePaths[locale.Index] = path;
            }

        }*/

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
