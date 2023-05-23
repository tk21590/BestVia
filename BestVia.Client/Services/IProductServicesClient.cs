using BestVia.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http.Json;

namespace BestVia.Client.Services
{
    public interface IProductServicesClient
    {
        Task<List<Product>> ListAsync();
        Task<List<Product>> ListIdCateAsync(int id_cate);
        Task<ApiRespone> SearchListAsync(string name);
        Task<Product> GetIdAsync(string id);
        Task<ApiRespone> GetNameAsync(string name);
        Task<ApiRespone> EditAsync(Product product);
        Task<ApiRespone> AddAsync(Product product);
    }

    public class ProductServicesClient : IProductServicesClient
    {
        private readonly HttpClient _httpClient;
        public ProductServicesClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiRespone> AddAsync(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Product/add", product);
            
            return await response.Content.ReadFromJsonAsync<ApiRespone>();

        }

        public async Task<ApiRespone> EditAsync(Product product)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Product/edit", product);

            return await response.Content.ReadFromJsonAsync<ApiRespone>();
        }

        public async Task<Product> GetIdAsync(string id)
        {
            var respone = await _httpClient.GetFromJsonAsync<ApiRespone>($"/api/product/get_id/{id}");
            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var ptype = JsonConvert.DeserializeObject<Product>(data);
                    return ptype;
                }
                catch (Exception ex)
                {

                }

            }
            return new Product();
        }

        public Task<ApiRespone> GetNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Product>> ListAsync()
        {
            var respone = await _httpClient.GetFromJsonAsync<ApiRespone>("/api/Product/get_list");
            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var users = JsonConvert.DeserializeObject<List<Product>>(data);
                    return users;
                }
                catch (Exception ex)
                {

                }

            }
            return new List<Product>();
        }

        public async Task<List<Product>> ListIdCateAsync(int id_cate)
        {

            var respone = await _httpClient.GetFromJsonAsync<ApiRespone>($"/api/Product/get_list_id_category/{id_cate}");
            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var users = JsonConvert.DeserializeObject<List<Product>>(data);
                    return users;
                }
                catch (Exception ex)
                {

                }

            }
            return new List<Product>();
        }

        public Task<ApiRespone> SearchListAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
