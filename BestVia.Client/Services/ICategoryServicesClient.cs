using BestVia.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http.Json;

namespace BestVia.Client.Services
{
    public interface ICategoryServicesClient
    {
        Task<List<Category>> ListAsync();
        Task<ApiRespone> SearchListAsync(string name);
        Task<Category> GetIdAsync(string id);
        Task<ApiRespone> GetNameAsync(string name);
        Task<ApiRespone> EditAsync(Category cate);
        Task<ApiRespone> AddAsync(Category cate);
    }

    public class CategoryServicesClient : ICategoryServicesClient
    {
        private readonly HttpClient _httpClient;
        public CategoryServicesClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiRespone> AddAsync(Category cate)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/category/add", cate);
            
            return await response.Content.ReadFromJsonAsync<ApiRespone>();

        }

        public async Task<ApiRespone> EditAsync(Category cate)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/category/edit", cate);

            return await response.Content.ReadFromJsonAsync<ApiRespone>();
        }

        public async Task<Category> GetIdAsync(string id)
        {
            var respone = await _httpClient.GetFromJsonAsync<ApiRespone>($"/api/category/get_id/{id}");
            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var ptype = JsonConvert.DeserializeObject<Category>(data);
                    return ptype;
                }
                catch (Exception ex)
                {

                }

            }
            return new Category();
        }

        public Task<ApiRespone> GetNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Category>> ListAsync()
        {
            var respone = await _httpClient.GetFromJsonAsync<ApiRespone>("/api/category/get_list");
            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var users = JsonConvert.DeserializeObject<List<Category>>(data);
                    return users;
                }
                catch (Exception ex)
                {

                }

            }
            return new List<Category>();
        }

        public Task<ApiRespone> SearchListAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
