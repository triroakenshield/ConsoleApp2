using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    public class Event_desc
    {

        public int id { get; set; }
        public long publication_date { get; set; }

        public List<Dictionary<string, long>> dates { get; set; }
        public Dictionary<string, int> place { get; set; }

        public List<string> categories { get; set; }


        public string title { get; set; }
        public string description { get; set; }
        public string body_text { get; set; }

        public string site_url { get; set; }        

        public string slug { get; set; }


    }
}
