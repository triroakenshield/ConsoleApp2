using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace ConsoleApp1
{
    public static class timepad_api
    {

        public static HttpClient GetClient()
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(
                  "application/json"
                )
            );
            client.DefaultRequestHeaders.Add(
              "User-Agent",
              ".NET Foundation Repository Reporter"
            );
            return client;
        }

        public static async Task EventsAsync(HttpClient client, List<Event> rList, int count, int poz)
        {
            string uuri = $"https://api.timepad.ru/v1/events.json?limit={count}&skip={poz}&cities=Екатеринбург&fields=location";
            var stringTask = client.GetStringAsync(uuri);
            var msg = await stringTask;
            JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(msg);
            Event ev;
            foreach (JObject obj in jObject["values"])
            {
                rList.Add(obj.ToObject<Event>());
            }
        }

        public static async Task MainAsync(HttpClient client)
        { //https://api.timepad.ru/v1/events.json?limit=1&skip=0&cities=Екатеринбург&fields=location
            string uuri = "https://api.timepad.ru/v1/events.json?limit=1&skip=0&cities=Екатеринбург&fields=location";
            var stringTask = client.GetStringAsync(uuri);
            var msg = await stringTask;
            JObject jObject = Newtonsoft.Json.Linq.JObject.Parse(msg);
            int ccount = (jObject["total"] as JToken).Value<int>();
            JArray arr = jObject["values"] as JArray;
            if (arr.Count > 0)
            {
                List<Event> rList = new List<Event>();
                rList.Add((arr[0] as JObject).ToObject<Event>());
                if (ccount > 1)
                {
                    int poz = 1;
                    while (ccount > 0)
                    {
                        if (ccount > 100)
                        {
                            EventsAsync(client, rList, 100, poz).Wait();
                            ccount -= 100;
                            poz += 100;
                        }
                        else
                        {
                            EventsAsync(client, rList, ccount, poz).Wait();
                            ccount = 0;
                        }
                    }
                }
                File.WriteAllText("res.sql", String.Join("", rList.Select(c => c.GetInsSql()).ToArray()));
            }
        }

    }
}