using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace ConsoleApp1
{
    public class Location
    {
        public double[] coordinates { get; set; }

        public override string ToString()
        {
            return $"PointFromText('POINT({coordinates[0].ToString(CultureInfo.InvariantCulture)} {coordinates[1].ToString(CultureInfo.InvariantCulture)})')";
        }
    }
}
