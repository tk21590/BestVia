using BestVia.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http.Json;

namespace BestVia.Client.Services
{
    public interface IProxyServicesClient
    {
        Task<List<Proxy>> ListAsync();
        Task<List<Proxy>> ListOrderIdAsync(int orderid);
        Task<ApiRespone> AddAsync(Proxy Proxy);
        Task<ApiRespone> AddRangeAsync(Proxy[] Proxys);
    }

    public class ProxyServicesClient : IProxyServicesClient
    {
        private readonly HttpClient _httpClient;
        public ProxyServicesClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiRespone> AddAsync(Proxy Proxy)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Proxy/add", Proxy);

            return await response.Content.ReadFromJsonAsync<ApiRespone>();

        }

        public async Task<ApiRespone> AddRangeAsync(Proxy[] Proxys)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Proxy/add_range", Proxys);

            return await response.Content.ReadFromJsonAsync<ApiRespone>();
        }

        public async Task<List<Proxy>> ListAsync()
        {
            var respone = await _httpClient.GetFromJsonAsync<ApiRespone>("/api/Proxy/get_list");
            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var Proxys = JsonConvert.DeserializeObject<List<Proxy>>(data);
                    return Proxys;
                }
                catch (Exception ex)
                {

                }

            }
            return new List<Proxy>();
        }

        public async Task<List<Proxy>> ListOrderIdAsync(int orderid)
        {
            var respone = await _httpClient.GetFromJsonAsync<ApiRespone>($"/api/Proxy/get_list_order/{orderid}");
            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var Proxys = JsonConvert.DeserializeObject<List<Proxy>>(data);
                    return Proxys;
                }
                catch (Exception ex)
                {

                }

            }
            return new List<Proxy>();
        }

    }
}
