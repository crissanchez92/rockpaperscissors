using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace WebApiClient.HttpClientUtil
{
    public class ApiClient
    {
        /// <summary>
        /// Sets common Http client values
        /// </summary>
        /// <param name="client"></param>
        private void SetCommons(HttpClient client)
        {
            client.BaseAddress = new Uri("http://rpsapi.azurewebsites.net/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Gets the top winners from database
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<List<string>> GetTop(int? count)
        {
            try
            {
                List<string> result = new List<string>();
                using (var client = new HttpClient())
                {
                    SetCommons(client);
                    StringBuilder uri = new StringBuilder();
                    uri.Append("api/championship/top");
                    if (count.HasValue)
                    {
                        uri.Append("?count=");
                        uri.Append(count);
                    }
                    var uriii = uri.ToString();
                    HttpResponseMessage response = await client.PostAsync(uri.ToString(), null);
                    response.EnsureSuccessStatusCode();
                    if (response.IsSuccessStatusCode)
                    {
                        dynamic element = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result);
                        foreach (var player in element.players)
                            result.Add((string)player);
                    }
                }
                return result;
            }
            catch(HttpRequestException e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        /// <summary>
        /// Saves the winners on database
        /// </summary>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public async Task<bool> SaveResult(string first, string second)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    SetCommons(client);
                    string uri = string.Format("api/championship/result?first={0}&second={1}", first, second);
                    HttpResponseMessage response = await client.PostAsync(uri, null);
                    response.EnsureSuccessStatusCode();
                    return response.IsSuccessStatusCode;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        /// <summary>
        /// Resets the database and starts over
        /// </summary>
        /// <returns></returns>
        public async Task<bool> Reset()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    SetCommons(client);
                    HttpResponseMessage response = await client.DeleteAsync("api/championship/reset");
                    response.EnsureSuccessStatusCode();
                    return response.IsSuccessStatusCode;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
