using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace ConsoleApp1
{
    public class Location
    {
        public double[] coordinates { get; set; }

        public XmlElement ToXml(XmlDocument doc)
        {
            XmlElement c = doc.CreateElement("coordinates");
            c.InnerText = $"{coordinates[1].ToString(CultureInfo.InvariantCulture)}, {coordinates[0].ToString(CultureInfo.InvariantCulture)}, 0";
            XmlElement res = doc.CreateElement("Point");
            res.AppendChild(c);
            return res;
        }

        public override string ToString()
        {
            return $"PointFromText('POINT({coordinates[1].ToString(CultureInfo.InvariantCulture)} {coordinates[0].ToString(CultureInfo.InvariantCulture)})')";
        }
    }
}
