using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Map
{
    public class MapPath
    {
        public Locale Start { get; set; }= new Locale();
        public Locale End { get; set; }= new Locale();
        public int Length { get; set; }
        public int VerticalDrop => Start.Value - End.Value;
    }
}
