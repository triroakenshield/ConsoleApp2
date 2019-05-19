using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ConsoleApp2
{
    public class News
    {
        public int count { get; set; }

        public string next { get; set; }

        public string previous { get; set; }

        public Event[] results { get; set; }

        public News(int ncount, string nnext, string nprevious, Event[] nresults)
        {
            count = ncount;
            next = nnext;
            previous = nprevious;
            results = nresults;
        }

    }
}
