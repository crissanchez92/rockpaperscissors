using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using WebApiClient.HttpClientUtil;
using Newtonsoft.Json;

namespace WebApiClient
{
    class Program
    {
        static void Main()
        {
            RunAsync().Wait();
        }

        static async Task RunAsync()
        {
            using (var client = new HttpClient())
            {
                // TODO - Send HTTP requests
                client.BaseAddress = new Uri("http://localhost:54743/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Test request
                HttpResponseMessage response = await client.PostAsync("api/championship/result?first=gil&second=cris", null);
                if (response.IsSuccessStatusCode) 
                {
                    string top = response.Content.ReadAsStringAsync().Result;
                    dynamic element = JsonConvert.DeserializeObject(top);
                    foreach(var player in element.players)
                    {
                        var playerA = player.value;
                    }
                }
            }
        }
    }
}
