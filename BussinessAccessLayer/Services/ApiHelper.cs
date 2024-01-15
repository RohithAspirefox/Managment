using Management.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Management.Services.Services
{
    public class ApiHelperService : IApiHelperService
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _httpClient;

        public ApiHelperService(IConfiguration configuration)
        {
            _configuration = configuration;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(_configuration["BaseUrl"]);
        }

        public async Task<T?> GetAsync<T>(string endpoint, Dictionary<string, string> headers = null, string token = null)
        {
            AddHeaders(headers, token);

            using (HttpResponseMessage res = await _httpClient.GetAsync(endpoint))
            {
                return await ProcessResponse<T>(res);
            }
        }

        public async Task<T?> PostAsync<T>(string endpoint, object body, Dictionary<string, string> headers = null, string token = null)
        {
            AddHeaders(headers, token);

            var requestBodyString = JsonConvert.SerializeObject(body);
            var httpContent = new StringContent(requestBodyString, Encoding.UTF8, "application/json");

            using (HttpResponseMessage res = await _httpClient.PostAsync(endpoint, httpContent))
            {
                return await ProcessResponse<T>(res);
            }
        }

        public async Task<T?> DeleteAsync<T>(string endpoint, Dictionary<string, string> headers = null, string token = null)
        {
            AddHeaders(headers, token);

            using (HttpResponseMessage res = await _httpClient.DeleteAsync(endpoint))
            {
                return await ProcessResponse<T>(res);
            }
        }

        private void AddHeaders(Dictionary<string, string> headers, string token)
        {
            if (headers != null)
            {
                foreach (var header in headers)
                {
                    _httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);
                }
            }

            // Add Bearer Token if available
            var bearerToken = token ?? string.Empty;
            if (!string.IsNullOrEmpty(bearerToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", bearerToken);
            }
        }

        private async Task<T?> ProcessResponse<T>(HttpResponseMessage res)
        {
            using (HttpContent content = res.Content)
            {
                var response = await content.ReadAsStringAsync();
                if (TryParseJson<T>(response))
                {
                    return JsonConvert.DeserializeObject<T>(response);
                }
                else
                {
                    return default(T);
                }
            }
        }

        private static bool TryParseJson<T>(string jsonString)
        {
            try
            {
                var settings = new JsonSerializerSettings
                {
                    Error = (sender, args) => { args.ErrorContext.Handled = true; },
                    MissingMemberHandling = MissingMemberHandling.Error
                };
                JsonConvert.DeserializeObject<T>(jsonString);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}