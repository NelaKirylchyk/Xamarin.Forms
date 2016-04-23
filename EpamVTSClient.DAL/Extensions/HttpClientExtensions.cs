using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EpamVTSClient.DAL.Extensions
{
    public static class HttpClientExtensions
    {
        //private static readonly string DefaultUrl = "http://epbyminw3859.minsk.epam.com/api/";
        private static readonly string DefaultUrl = "http://169.254.80.80/api/";

        public static async Task<TResult> GetAsync<TResult>(this HttpClient client, string urlWithParameters)
        {
            HttpResponseMessage httpResponseMessage = await client.GetAsync($"{DefaultUrl}{urlWithParameters}");

            return await DeserializeResponseAsJsonOrThrow<TResult>(httpResponseMessage);
        }

        public static async Task<bool> DeleteItemAsync(this HttpClient client, string urlWithParameters)
        {
            HttpResponseMessage httpResponseMessage = await client.DeleteAsync($"{DefaultUrl}{urlWithParameters}");
            return httpResponseMessage.IsSuccessStatusCode;
        }

        public static async Task<TResult> PostAsync<T, TResult>(this HttpClient client, T model, string apiController)
        {
            var serializeObject = JsonConvert.SerializeObject(model);

            HttpResponseMessage httpResponseMessage =
                await client.PostAsync($"{DefaultUrl}{apiController}",
                    new StringContent(serializeObject, Encoding.UTF8, "application/json"));

            return await DeserializeResponseAsJsonOrThrow<TResult>(httpResponseMessage);
        }

        private static async Task<TResult> DeserializeResponseAsJsonOrThrow<TResult>(HttpResponseMessage httpResponseMessage)
        {
            httpResponseMessage.EnsureSuccessStatusCode();

            var stream = await httpResponseMessage.Content.ReadAsStreamAsync();
            using (stream)
            {
                var jsonSerializer = new JsonSerializer();
                using (var str = new StreamReader(stream))
                {
                    using (var jsonTextReader = new JsonTextReader(str))
                    {
                        return jsonSerializer.Deserialize<TResult>(jsonTextReader);
                    }
                }
            }
        }
    }
}