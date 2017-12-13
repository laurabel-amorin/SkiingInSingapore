using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map
{
    public class SquareMap
    {
        public int[] MapArray => mapArray;
        public Locale[] LocaleArray => localeArray;
        public int SideLength => sideLength;

        private readonly int sideLength;
        private readonly int[] mapArray;
        private readonly Locale[] localeArray;

        public SquareMap(string mapText, int sideLength)
        {
            this.sideLength = sideLength;
            int size = sideLength*sideLength;
            mapArray = new int[size];
            localeArray= new Locale[size];
            mapText = mapText.Replace('\n', ' ');
            string[] mapValues = mapText.Split(' ');
            int index = 0;
            foreach(var value in mapValues)
            {
                int intValue;
                if (int.TryParse(value, out intValue))
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

            localeArray = localeArray.OrderByDescending(l => l.Value).ToArray();

        }

        public int GetAtIndex(int index)
        {
            return mapArray[index];
        }


        public int GetMapRow(int index)
        {
            return (index /SideLength)+1;
        }

        public int GetMapColumn(int index)
        {
            return (index % SideLength)+1;
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
            if (index >= sideLength)
            {
                return index - sideLength;
            }
            return -1;
        }

        private int GetDown(int index)
        {
            int lastRowBoundary = sideLength * (sideLength - 1);
            if (index < lastRowBoundary)
            {
                return index + sideLength;
            }
            return -1;
        }

        private int GetLeft(int index)
        {
            if ((index % sideLength) != 0)
            {
                return index - 1;
            }
            return -1;
        }

        private int GetRight(int index)
        {
            if ((index % sideLength) != (sideLength - 1))
            {
                return index + 1;
            }
            return -1;
        }
    }

    public class SquareMapParseException : Exception
    {
        public SquareMapParseException() : base("An invalid value has ruined the squareness of your map!!!!")
        {

        }
    }

}
