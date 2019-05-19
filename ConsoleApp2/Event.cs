using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp2
{
    public class Event
    {

        public int id { get; set; }

        public string title { get; set; }

        public string slug { get; set; }

        public Event(int nid, string ntitle, string nslug)
        {
            id = nid;
            title = ntitle;
            slug = nslug;
        }

    }
}
