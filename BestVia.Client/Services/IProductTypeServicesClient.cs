using BestVia.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http.Json;

namespace BestVia.Client.Services
{
    public interface IProductTypeServicesClient
    {
        Task<List<ProductType>> ListAsync();
        Task<ApiRespone> SearchListAsync(string name);
        Task<ProductType> GetIdAsync(string id);
        Task<ApiRespone> GetNameAsync(string name);
        Task<ApiRespone> EditAsync(ProductType ptype);
        Task<ApiRespone> AddAsync(ProductType ptype);
    }

    public class ProductTypeServicesClient : IProductTypeServicesClient
    {
        private readonly HttpClient _httpClient;
        public ProductTypeServicesClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiRespone> AddAsync(ProductType ptype)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/producttype/add", ptype);
            
            return await response.Content.ReadFromJsonAsync<ApiRespone>();

        }

        public async Task<ApiRespone> EditAsync(ProductType ptype)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/producttype/edit", ptype);

            return await response.Content.ReadFromJsonAsync<ApiRespone>();
        }

        public async Task<ProductType> GetIdAsync(string id)
        {
            var respone = await _httpClient.GetFromJsonAsync<ApiRespone>($"/api/producttype/get_id/{id}");
            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var ptype = JsonConvert.DeserializeObject<ProductType>(data);
                    return ptype;
                }
                catch (Exception ex)
                {

                }

            }
            return new ProductType();
        }

        public Task<ApiRespone> GetNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProductType>> ListAsync()
        {
            var respone = await _httpClient.GetFromJsonAsync<ApiRespone>("/api/producttype/get_list");
            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var users = JsonConvert.DeserializeObject<List<ProductType>>(data);
                    return users;
                }
                catch (Exception ex)
                {

                }

            }
            return new List<ProductType>();
        }

        public Task<ApiRespone> SearchListAsync(string name)
        {
            throw new NotImplementedException();
        }
    }
}
