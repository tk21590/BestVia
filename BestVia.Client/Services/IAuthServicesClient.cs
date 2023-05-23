using BestVia.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http.Json;

namespace BestVia.Client.Services
{
    public interface IAuthServicesClient
    {
        Task<ApiRespone> LoginAsync(LoginModel loginModel);
        Task<ApiRespone> RegisterAsync(RegisterModel registerModel);
      
    }

    public class AuthServicesClient : IAuthServicesClient
    {
        private readonly HttpClient _httpClient;
        public AuthServicesClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiRespone> LoginAsync(LoginModel loginModel)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/auth/login", loginModel);

            return await response.Content.ReadFromJsonAsync<ApiRespone>();
        }

        public async Task<ApiRespone> RegisterAsync(RegisterModel registerModel)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/auth/register", registerModel);

            return await response.Content.ReadFromJsonAsync<ApiRespone>();
        }
    }
}
