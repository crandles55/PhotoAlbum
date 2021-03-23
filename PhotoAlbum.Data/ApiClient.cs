using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PhotoAlbum.Data
{
    public interface IApiClient
    {
        Task<T> GetAsync<T>(Uri uri);
    }
    
    public class ApiClient : IApiClient
    {
        public async Task<T> GetAsync<T>(Uri uri)
        {
            var response = SendRequestAsync(uri, HttpMethod.Get);

            response.Result.EnsureSuccessStatusCode();

            var content = await response.Result.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(content);

        }

        private static async Task<HttpResponseMessage> SendRequestAsync(Uri uri, HttpMethod method)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = uri,
            };

            return await SendRequestAsync(request);
        }
        
        private static async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage request)
        {
            using var client = new HttpClient {Timeout = TimeSpan.FromSeconds(5000)};
            return await client.SendAsync(request);
        }
    }
}
