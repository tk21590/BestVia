using BestVia.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http.Json;

namespace BestVia.Client.Services
{
    public interface IKeyServicesClient
    {
        Task<List<Key>> ListAsync();
        Task<Key> GetIdAsync(string id);
        Task<Key> GetNameAsync(string keyid);
        Task<ApiRespone> EditAsync(Key key);
        Task<ApiRespone> AddAsync(Key key);
        Task<ApiRespone> DeleteAsync(int keyid);
    }

    public class KeyServicesClient : IKeyServicesClient
    {
        private readonly HttpClient _httpClient;
        public KeyServicesClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiRespone> AddAsync(Key key)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Key/add", key);
            
            return await response.Content.ReadFromJsonAsync<ApiRespone>();

        }

        public async Task<ApiRespone> DeleteAsync(int keyid)
        {
            return await _httpClient.GetFromJsonAsync<ApiRespone>($"/api/Key/delete/{keyid}");
         
        }

        public async Task<ApiRespone> EditAsync(Key key)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Key/edit", key);

            return await response.Content.ReadFromJsonAsync<ApiRespone>();
        }

        public async Task<Key> GetIdAsync(string id)
        {
            var respone = await _httpClient.GetFromJsonAsync<ApiRespone>($"/api/Key/get_id/{id}");
            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var key = JsonConvert.DeserializeObject<Key>(data);
                    return key;
                }
                catch (Exception ex)
                {

                }

            }
            return null;
        }

        public async Task<Key> GetNameAsync(string keyid)
        {
            var respone = await _httpClient.GetFromJsonAsync<ApiRespone>($"/api/Key/get_name/{keyid}");
            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var key = JsonConvert.DeserializeObject<Key>(data);
                    return key;
                }
                catch (Exception ex)
                {

                }

            }
            return null;
        }

        public async Task<List<Key>> ListAsync()
        {
            var respone = await _httpClient.GetFromJsonAsync<ApiRespone>("/api/Key/get_list");
            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var users = JsonConvert.DeserializeObject<List<Key>>(data);
                    return users;
                }
                catch (Exception ex)
                {

                }

            }
            return new List<Key>();
        }

      
    }
}
