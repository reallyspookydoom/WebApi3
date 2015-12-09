using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;


namespace ConsoleClient
{
    public class Program
    {
        private static List<TodoItem> _cache;

        static void Main(string[] args)
        {
            InitializeCache();

            WriteAllItems();
            UpdateFirstItem();
            WriteAllItems();

            Console.WriteLine();
            Console.WriteLine("Press <ENTER> to exit...");
            Console.ReadLine();
        }

        private static void WriteAllItems()
        {
            foreach (TodoItem item in _cache)
            {
                Console.WriteLine(item.Key);
                Console.WriteLine("    Name: " + item.Name);
                Console.WriteLine("    Is completed: " + item.IsComplete);
                Console.WriteLine("    Sub items: " + item.SubItems.Count);
                Console.WriteLine();
            }

            Console.WriteLine("-----------------------------------------------------");
        }

        private async static void UpdateFirstItem()
        {
            _cache[0].Name = "bananarama";
            _cache[0].IsComplete = true;

            using (var client = new HttpClient())
            {
                var content = new StringContent(JsonConvert.SerializeObject(_cache[0]), Encoding.UTF8, "application/json");
                var response = await client.PutAsync("http://localhost:54360/api/todo/" + _cache[0].Key, content);
            }
        }

        private static void InitializeCache()
        {
            var request = WebRequest.Create("http://localhost:54360/api/todo");
            request.Method = "GET";

            try
            {
                var response = request.GetResponse() as HttpWebResponse;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    using (var stream = response.GetResponseStream())
                    {
                        var reader = new StreamReader(stream, Encoding.UTF8);
                        string content = reader.ReadToEnd();

                        _cache = JsonConvert.DeserializeObject<List<TodoItem>>(content);
                    }
                }
                else
                {
                    Console.WriteLine("Status code: {0} || Status description: {1}", response.StatusCode, response.StatusDescription);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
