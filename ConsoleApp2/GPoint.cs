using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace ConsoleApp2
{
    public class GPoint
    {

        public double lat { get; set; }

        public double lon { get; set; }

        public override string ToString()
        {
            return $"PointFromText('POINT({lat.ToString(CultureInfo.InvariantCulture)} {lon.ToString(CultureInfo.InvariantCulture)})')";
        }

    }
}
