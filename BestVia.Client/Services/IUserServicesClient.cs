using BestVia.Client.Data;
using BestVia.Models;
using Newtonsoft.Json;
using System.Net.Http.Json;

namespace BestVia.Client.Services
{
    public interface IUserServicesClient
    {
        Task<List<UserView>> ListAsync();
        Task<List<UserView>> SearchNameAsync(string username);
        Task<UserView> GetIdAsync(string userid);
        Task<UserView> GetNameAsync(string username);
        Task<ApiRespone> EditAsync(UserView user);
        Task<ApiRespone> AddAsync(UserView user);
    }

    public class UserServicesClient : IUserServicesClient
    {
        private readonly HttpClient _httpClient;
        public UserServicesClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiRespone> AddAsync(UserView user)
        {
            throw new NotImplementedException();
        }

        public async Task<ApiRespone> EditAsync(UserView user)
        {
            var respone = await _httpClient.PostAsJsonAsync("/api/user/edit",user);
            return await respone.Content.ReadFromJsonAsync<ApiRespone>();

        }

        public async Task<UserView> GetIdAsync(string userid)
        {
            var respone = await _httpClient.GetFromJsonAsync<ApiRespone>($"/api/user/get_id/{userid}");
            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var users = JsonConvert.DeserializeObject<UserView>(data);
                    return users;
                }
                catch (Exception ex)
                {

                }

            }
            return null;
        }  
        
        public async Task<List<UserView>> SearchNameAsync(string username)
        {
            var respone = await _httpClient.GetFromJsonAsync<ApiRespone>($"/api/user/search_name/{username}");
            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var users = JsonConvert.DeserializeObject<List<UserView>>(data);
                    return users;
                }
                catch (Exception ex)
                {

                }

            }
            return new List<UserView>();
        }

        public async Task<UserView> GetNameAsync(string username)
        {
            var respone = await _httpClient.GetFromJsonAsync<ApiRespone>($"/api/user/get_name/{username}");
            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var user = JsonConvert.DeserializeObject<UserView>(data);
                    return user;
                }
                catch (Exception ex)
                {

                }

            }
            return null;
        }

        public async Task<List<UserView>> ListAsync()
        {
            var respone = await _httpClient.GetFromJsonAsync<ApiRespone>("/api/user/get_list");
            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var users = JsonConvert.DeserializeObject<List<UserView>>(data);
                    return users;
                }
                catch (Exception ex)
                {

                }

            }
            return new List<UserView>();
        }

        public Task<ApiRespone> SearchListAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
