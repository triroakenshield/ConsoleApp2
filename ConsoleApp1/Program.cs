using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpClient client = timepad_api.GetClient();
            timepad_api.MainAsync(client).Wait();
            Console.Write("end");
            Console.Read();
        }
    }
}
