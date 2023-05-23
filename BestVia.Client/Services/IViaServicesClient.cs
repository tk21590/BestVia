using BestVia.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http.Json;

namespace BestVia.Client.Services
{
    public interface IViaServicesClient
    {
        Task<List<Via>> ListAsync();
        Task<ApiRespone> SearchListAsync(string name);
        Task<List<Via>> ListOrderIdAsync(int orderid);
        Task<ApiRespone> GetIdAsync(string id);
        Task<ApiRespone> GetNameAsync(string name);
        Task<ApiRespone> EditAsync(Via Via);
        Task<ApiRespone> AddAsync(Via Via);
        Task<ApiRespone> AddRangeAsync(Via[] Vias);
    }

    public class ViaServicesClient : IViaServicesClient
    {
        private readonly HttpClient _httpClient;
        public ViaServicesClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiRespone> AddAsync(Via Via)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Via/add", Via);

            return await response.Content.ReadFromJsonAsync<ApiRespone>();

        }

        public async Task<ApiRespone> AddRangeAsync(Via[] Vias)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Via/add_range", Vias);

            return await response.Content.ReadFromJsonAsync<ApiRespone>();
        }

        public Task<ApiRespone> EditAsync(Via Via)
        {
            throw new NotImplementedException();
        }

        public Task<ApiRespone> GetIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<ApiRespone> GetNameAsync(string name)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Via>> ListAsync()
        {
            var respone = await _httpClient.GetFromJsonAsync<ApiRespone>("/api/Via/get_list");
            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var Vias = JsonConvert.DeserializeObject<List<Via>>(data);
                    return Vias;
                }
                catch (Exception ex)
                {

                }

            }
            return new List<Via>();
        }

        public async Task<List<Via>> ListOrderIdAsync(int orderid)
        {
            var respone = await _httpClient.GetFromJsonAsync<ApiRespone>($"/api/via/get_list_order/{orderid}");
            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var Vias = JsonConvert.DeserializeObject<List<Via>>(data);
                    return Vias;
                }
                catch (Exception ex)
                {

                }

            }
            return new List<Via>();
        }

        public Task<ApiRespone> SearchListAsync(string name)
        {
            return null;
        }
    }
}
