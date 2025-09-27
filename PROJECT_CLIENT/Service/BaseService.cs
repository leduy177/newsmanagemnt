using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using PROJECT_CLIENT.DTO;

namespace PROJECT_CLIENT.Service
{
    public class BaseService
    {
        private readonly string? _rootUrl;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BaseService()
        {
            var config = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            _rootUrl = config.GetSection("ApiUrls")["MyApi"];
            _httpContextAccessor = new HttpContextAccessor(); // Inject context accessor manually
        }

        public async Task<T?> GetData<T>(string url, string? accepttype = null)
        {
            T? result = default;
            using var client = new HttpClient();
            url = _rootUrl + url;

            AddJwtHeader(client);

            var response = await client.GetAsync(url);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                result = await response.Content.ReadFromJsonAsync<T>();
                return result;
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                throw new UnauthorizedAccessException("Unauthorized: You must log in.");
            }
            else
            {
                throw new Exception(response.StatusCode.ToString());
            }
        }

        public async Task<HttpStatusCode> PushData<T>(string url, T value, string? accepttype = null)
        {
            using var client = new HttpClient();
            url = _rootUrl + url;
            AddJwtHeader(client);

            var jsonStr = JsonSerializer.Serialize(value);
            HttpContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> PutData<T>(string url, T value)
        {
            using var client = new HttpClient();
            url = _rootUrl + url;
            AddJwtHeader(client);

            var json = JsonSerializer.Serialize(value);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(url, content);
            return response.StatusCode;
        }

        public async Task<HttpStatusCode> DeleteData(string url)
        {
            using var client = new HttpClient();
            url = _rootUrl + url;
            AddJwtHeader(client);

            var response = await client.DeleteAsync(url);
            return response.StatusCode;
        }

        public async Task<LoginResultDto> LoginAsync(LoginDto dto)
        {
            string url = _rootUrl + "auth/login";
            using var client = new HttpClient();

            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                using var doc = JsonDocument.Parse(responseContent);
                string? token = doc.RootElement.GetProperty("token").GetString();
                return new LoginResultDto { Token = token };
            }
            else
            {
                using var doc = JsonDocument.Parse(responseContent);
                var message = doc.RootElement.TryGetProperty("message", out var msgProp)
                    ? msgProp.GetString()
                    : "Đăng nhập thất bại.";
                return new LoginResultDto { Message = message };
            }
        }


        private void AddJwtHeader(HttpClient client)
        {
            var token = GetAccessToken();
            if (!string.IsNullOrEmpty(token))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
        }

        private string? GetAccessToken()
        {
            var user = _httpContextAccessor.HttpContext?.User;
            return user?.Claims.FirstOrDefault(c => c.Type == "AccessToken")?.Value;
        }
        public async Task<HttpResponseMessage> PostWithToken<T>(string url, T data, string token)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await client.PostAsync(_rootUrl + url, content);
        }

        public async Task<HttpResponseMessage> PutWithToken<T>(string url, T data, string token)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            return await client.PutAsync(_rootUrl + url, content);
        }

        public async Task<HttpResponseMessage> DeleteWithToken(string url, string token)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return await client.DeleteAsync(_rootUrl + url);
        }
        public async Task<(bool IsSuccess, string? UserId, string? Error)> RegisterAsync(RegisterDto dto)
        {
            string url = _rootUrl + "Register/register-account";

            using var client = new HttpClient();
            var json = JsonSerializer.Serialize(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(url, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                using var doc = JsonDocument.Parse(responseContent);
                var userId = doc.RootElement.GetProperty("userId").GetString();
                return (true, userId, null);
            }
            else
            {
                using var doc = JsonDocument.Parse(responseContent);
                var error = doc.RootElement.TryGetProperty("message", out var messageProp)
                            ? messageProp.GetString()
                            : "Đăng ký thất bại.";
                return (false, null, error);
            }
        }
        public async Task<T?> GetWithToken<T>(string url, string token)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetAsync(_rootUrl + url);
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<T>();
            }
            return default;
        }
        public async Task<(bool IsSuccess, string Message)> PostAndReadMessageAsync<T>(string url, T data, string token)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PostAsync(_rootUrl + url, content);
            var responseContent = await response.Content.ReadAsStringAsync();

            string message;
            try
            {
                using var doc = JsonDocument.Parse(responseContent);
                message = doc.RootElement.TryGetProperty("message", out var msgProp)
                    ? msgProp.GetString() ?? response.ReasonPhrase ?? ""
                    : response.ReasonPhrase ?? "";
            }
            catch
            {
                message = responseContent; // fallback nếu không parse được
            }

            return (response.IsSuccessStatusCode, message);
        }



        public async Task<(bool IsSuccess, string Message)> PutAndReadMessageAsync<T>(string url, T data, string token)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var json = JsonSerializer.Serialize(data);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync(_rootUrl + url, content);
            var message = await response.Content.ReadAsStringAsync();

            return (response.IsSuccessStatusCode, message);
        }

        public async Task<(bool IsSuccess, string Message)> DeleteAndReadMessageAsync(string url, string token)
        {
            using var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var response = await client.DeleteAsync(_rootUrl + url);
            var message = await response.Content.ReadAsStringAsync();

            return (response.IsSuccessStatusCode, message);
        }
    }
}
