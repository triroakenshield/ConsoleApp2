using System;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    public class Event
    {
        public int id { get; set; }
        public DateTime starts_at { get; set; }
        public string name { get; set; }
        public string url { get; set; }

        public Location location { get; set; }

        public Category[] categories { get; set; }

        public string GetCats()
        {
            return String.Join(", ", this.categories.Select(c => c.name).ToArray());
        }

        public string GetInsSql()
        {
            try
            {
                return $"INSERT INTO events(out_id, title, descr, categories, coords, url, start_date, end_date) " +
$"VALUES ({this.id}, \'{this.name.Replace("'", "\\'")}\', '', \'{this.GetCats()}\', {location.ToString()}, \'{this.url}\', STR_TO_DATE(\'{starts_at.ToString()}\', \"%d.%m.%Y %h:%i:%s\"), STR_TO_DATE(\'{starts_at.ToString()}\', \"%d.%m.%Y %h:%i:%s\"));";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public XmlElement ToXml(XmlDocument doc)
        {
            XmlElement Placemark = doc.CreateElement("Placemark");
            XmlElement title = doc.CreateElement("name");
            title.InnerText = this.name;
            Placemark.AppendChild(title);
            Placemark.AppendChild(this.location.ToXml(doc));
            return Placemark;
        }

    }
}
