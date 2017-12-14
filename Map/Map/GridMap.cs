using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map
{
    public class GridMap
    {
        public int[] MapArray => mapArray;
        public Locale[] LocaleArray => localeArray;
        public int XLength => xLength;
        public int YLength => yLength;

        private readonly int xLength;
        private readonly int yLength;
        private readonly int[] mapArray;
        private readonly Locale[] localeArray;

        public GridMap(List<string> mapLines)
        {
            if ((mapLines==null)||(mapLines.Count == 0))
            {
                throw new GridMapParseException("No map provided");
            }
            var dimensions = mapLines[0].Split(' ');
            int x=0, y=0;
            if ((!int.TryParse(dimensions[0], out x))||(x<=0))
            {
                throw new GridMapParseException("Invalid grid dimensions.");
            }
            if (!int.TryParse(dimensions[0], out y)||(y<=0))
            {
                throw new GridMapParseException("Invalid grid dimensions.");
            }
            xLength = x;
            yLength = y;
            int size = xLength*yLength;
            mapArray = new int[size];
            localeArray= new Locale[size];
            int index = 0;
            for(int i=1; i < mapLines.Count; i++)
            {
                var line = mapLines[i].Split(' ');
                for (int j = 0; j < line.Length; j++)
                {
                    int intValue;                 
                    if (int.TryParse(line[j], out intValue))
                    {
                        mapArray[index] = intValue;
                        localeArray[index] = new Locale
                        {
                            Index = index,
                            Value = intValue
                        };
                        index++;
                    }
                }
            }
            localeArray = localeArray.OrderByDescending(l => l.Value).ToArray();

        }

        public int GetAtIndex(int index)
        {
            return mapArray[index];
        }


        public int GetMapRow(int index)
        {
            return (index /XLength)+1;
        }

        public int GetMapColumn(int index)
        {
            return (index % XLength)+1;
        }


        public List<Locale> GetViableNeighbours(Locale locale)
        {
            List<Locale> neighbours = new List<Locale>();
            var eastLocale = GetEastLocale(locale.Index);
            if ((eastLocale != null) && (locale.Value > eastLocale.Value))
            {
                neighbours.Add(eastLocale);
            }

            var westLocale = GetWestLocale(locale.Index);
            if ((westLocale != null) && (locale.Value > westLocale.Value))
            {
                neighbours.Add(westLocale);
            }

            var northLocale = GetNorthLocale(locale.Index);
            if ((northLocale != null) && (locale.Value > northLocale.Value))
            {
                neighbours.Add(northLocale);
            }

            var southLocale = GetSouthLocale(locale.Index);
            if ((southLocale != null) && (locale.Value > southLocale.Value))
            {
                neighbours.Add(southLocale);
            }
            return neighbours;
        } 

        private Locale GetNorthLocale(int index)
        {
            int northIndex = GetUp(index);
            if (northIndex == -1)
            {
                return null;
            }
            return new Locale
            {
                Index = northIndex,
                Value = mapArray[northIndex]
            };
        }

        private Locale GetSouthLocale(int index)
        {
            int southIndex = GetDown(index);
            if (southIndex == -1)
            {
                return null;
            }
            return new Locale
            {
                Index = southIndex,
                Value = mapArray[southIndex]
            };
        }

        private Locale GetEastLocale(int index)
        {
            int eastIndex = GetRight(index);
            if (eastIndex == -1)
            {
                return null;
            }
            return new Locale
            {
                Index = eastIndex,
                Value = mapArray[eastIndex]
            };
        }

        private Locale GetWestLocale(int index)
        {
            int westIndex = GetLeft(index);
            if (westIndex == -1)
            {
                return null;
            }
            return new Locale
            {
                Index = westIndex,
                Value = mapArray[westIndex]
            };
        }
        private int GetUp(int index)
        {
            if (index >= xLength)
            {
                return index - xLength;
            }
            return -1;
        }

        private int GetDown(int index)
        {
            int lastRowBoundary = xLength * (yLength - 1);
            if (index < lastRowBoundary)
            {
                return index + xLength;
            }
            return -1;
        }

        private int GetLeft(int index)
        {
            if ((index % xLength) != 0)
            {
                return index - 1;
            }
            return -1;
        }

        private int GetRight(int index)
        {
            if ((index % xLength) != (xLength - 1))
            {
                return index + 1;
            }
            return -1;
        }
    }

    public class GridMapParseException : Exception
    {
        public GridMapParseException(string message) : base(message)
        {

        }
    }

}
