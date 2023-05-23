using BestVia.Models;
using Newtonsoft.Json;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Net.Http.Json;

namespace BestVia.Client.Services
{
    public interface IOrderServicesClient
    {
        Task<List<Order>> ListAsync();
        Task<List<Order>> ListIdAsync(string userId);
        Task<Order> GetIdAsync(string id);
        Task<ApiRespone> GetNameAsync(string name);
        Task<ApiRespone> EditAsync(Order order);
        Task<ApiRespone> AddAsync(Order order);
        Task<ApiRespone> ComplainAsync(Complain complain);
        Task<List<Order>> ListRefAsync(SearchModel searchModel);
        Task<List<Order>> SearchListAsync(SearchModel searchModel);
    }

    public class OrderServicesClient : IOrderServicesClient
    {
        private readonly HttpClient _httpClient;
        public OrderServicesClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiRespone> AddAsync(Order order)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/order/add", order);

            return await response.Content.ReadFromJsonAsync<ApiRespone>();

        }

        public async Task<ApiRespone> ComplainAsync(Complain complain)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/order/complain", complain);

            return await response.Content.ReadFromJsonAsync<ApiRespone>();
        }

        public async Task<ApiRespone> EditAsync(Order order)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/order/edit", order);

            return await response.Content.ReadFromJsonAsync<ApiRespone>();
        }

        public async Task<Order> GetIdAsync(string id)
        {
            var respone = await _httpClient.GetFromJsonAsync<ApiRespone>($"/api/order/get_id/{id}");
            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var ptype = JsonConvert.DeserializeObject<Order>(data);
                    return ptype;
                }
                catch (Exception ex)
                {

                }

            }
            return new Order();
        }

        public Task<ApiRespone> GetNameAsync(string name)
        {
            throw new NotImplementedException();
        }
        public async Task<List<Order>> ListIdAsync(string userId)
        {
            var respone = await _httpClient.GetFromJsonAsync<ApiRespone>($"/api/order/get_list_userid/{userId}");
            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var users = JsonConvert.DeserializeObject<List<Order>>(data);
                    return users;
                }
                catch (Exception ex)
                {

                }

            }
            return new List<Order>();
        }
        public async Task<List<Order>> ListAsync()
        {
            var respone = await _httpClient.GetFromJsonAsync<ApiRespone>("/api/order/get_list");
            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var users = JsonConvert.DeserializeObject<List<Order>>(data);
                    return users;
                }
                catch (Exception ex)
                {

                }

            }
            return new List<Order>();
        }
        public async Task<List<Order>> ListRefAsync(SearchModel search)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/order/get_list_ref", search);

            var respone = await response.Content.ReadFromJsonAsync<ApiRespone>();

            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var users = JsonConvert.DeserializeObject<List<Order>>(data);
                    return users;
                }
                catch (Exception ex)
                {

                }

            }
            return new List<Order>();
        }
    
        public async Task<List<Order>> SearchListAsync(SearchModel searchModel)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/order/search", searchModel);

            var respone = await response.Content.ReadFromJsonAsync<ApiRespone>();

            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var users = JsonConvert.DeserializeObject<List<Order>>(data);
                    return users;
                }
                catch (Exception ex)
                {

                }

            }
            return new List<Order>();
        }
    }
    
}
