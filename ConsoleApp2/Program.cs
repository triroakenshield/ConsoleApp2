using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Npgsql;
using Newtonsoft.Json;
//using System.Runtime.Serialization.Json;
//using System.Runtime.Serialization;

namespace ConsoleApp2
{
    public class Program
    {
        public static void Main(string[] args)
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
            MainAsync(client).Wait();
        }

        //public News news = null;

        public static void TestConnect()
        {
            string conn_param = "Server=127.0.0.1;Port=5432;User Id=postgres;Password=test;Database=events_db;"; //Например: "Server=127.0.0.1;Port=5432;User Id=postgres;Password=mypass;Database=mybase;"
            string sql = "select version();";
            NpgsqlConnection conn = new NpgsqlConnection(conn_param);
            NpgsqlCommand comm = new NpgsqlCommand(sql, conn);
            conn.Open(); //Открываем соединение.
            string res = comm.ExecuteScalar().ToString(); //Выполняем нашу команду.
            conn.Close(); //Закрываем соединение.
        }

        public static string SQLWriteEvent(Event_desc evdesc, GPoint p)
        {
            try
            {
                Dictionary<string, long> dd = evdesc.dates[0];
                return $"INSERT INTO events(out_id, title, descr, categories, coords, url, start_date, end_date) " +
                    $"VALUES ({evdesc.id}, \'{Regex.Escape(evdesc.title)}\', \'{Regex.Escape(evdesc.description)}\', \'{String.Join(", ", evdesc.categories)}\', {p.ToString()}, \'{Regex.Escape(evdesc.site_url)}\', FROM_UNIXTIME({dd["start"]}), FROM_UNIXTIME({dd["end"]}));";
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        public static void ExSQL(NpgsqlConnection conn, string ssql)
        {
            NpgsqlCommand comm = new NpgsqlCommand(ssql, conn);
            conn.Open(); //Открываем соединение.
            string res = comm.ExecuteScalar().ToString(); //Выполняем нашу команду.
            conn.Close(); //Закрываем соединение.
        }

        public static void TestSerializer()
        {
            Event ev = new Event(0, "test1", "test2");
            News news = new News(1, null, null, new Event[] { ev });
            //DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(News));
            //string json = JsonConvert.SerializeObject(news, Formatting.Indented);
        }

        public static async Task EventAsync(HttpClient client, Event ev)
        {
            //
            try
            {
                string uuri = $"https://kudago.com/public-api/v1.4/events/{ev.id}/?lang=&fields=&expand=";
                var stringTask = client.GetStringAsync(uuri);
                var msg = await stringTask;
                if (msg != null)
                {
                    Event_desc evdesc = JsonConvert.DeserializeObject<Event_desc>(msg);
                    if (evdesc.place != null) PlaceAsync(client, evdesc).Wait();
                }
                else
                {
                    //
                }
            }
            catch (Exception ex)
            {
                //
            }
        }

        public static async Task PlaceAsync(HttpClient client, Event_desc evdesc)
        {
            int pid = evdesc.place["id"];
            string uuri = $"https://kudago.com/public-api/v1.4/places/{pid}/?lang=&fields=&expand=";
            var stringTask = client.GetStringAsync(uuri);
            var msg = await stringTask;
            Place plc = JsonConvert.DeserializeObject<Place>(msg);
            File.AppendAllText("res.txt", SQLWriteEvent(evdesc, plc.coords));
        }

        public static async Task MainAsync(HttpClient client)
        {

            long unixTimeStampInSeconds = (DateTime.Now.ToUniversalTime().Ticks - 621355968000000000) / 10000000;
            string uuri = $"https://kudago.com/public-api/v1.4/events/?lang=&fields=&expand=&order_by=&text_format=&ids=&location=ekb&actual_since={unixTimeStampInSeconds}&is_free=&categories=&lon=&lat=&radius=";
            var stringTask = client.GetStringAsync(uuri);
            News news = JsonConvert.DeserializeObject<News>(await stringTask);
            int scount = news.count;

            foreach (Event ev in news.results)
            {
                EventAsync(client, ev).Wait();
                scount--;
            }

            while (scount > 0)
            {
                stringTask = client.GetStringAsync(news.next);
                news = JsonConvert.DeserializeObject<News>(await stringTask);
                foreach (Event ev in news.results)
                {
                    EventAsync(client, ev).Wait();
                    scount--;
                }
            }

            //Console.Write(msg);
            Console.Write("end");
            Console.Read();
        }
    }
}
