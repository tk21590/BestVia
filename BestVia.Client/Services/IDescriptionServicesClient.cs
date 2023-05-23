using BestVia.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http.Json;

namespace BestVia.Client.Services
{
    public interface IDescriptionServicesClient
    {
        Task<List<Description>> ListAsync();
        Task<Description> GetIdAsync(string id);
        Task<Description> GetHeaderAsync(string header);
        Task<ApiRespone> EditAsync(Description description);
        Task<ApiRespone> AddAsync(Description description);
    }

    public class DescriptionServicesClient : IDescriptionServicesClient
    {
        private readonly HttpClient _httpClient;
        public DescriptionServicesClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<ApiRespone> AddAsync(Description Description)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Description/add", Description);

            return await response.Content.ReadFromJsonAsync<ApiRespone>();

        }

        public async Task<ApiRespone> EditAsync(Description Description)
        {
            var response = await _httpClient.PostAsJsonAsync("/api/Description/edit", Description);

            return await response.Content.ReadFromJsonAsync<ApiRespone>();
        }

        public async Task<Description> GetIdAsync(string id)
        {
            var respone = await _httpClient.GetFromJsonAsync<ApiRespone>($"/api/Description/get_id/{id}");
            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var Description = JsonConvert.DeserializeObject<Description>(data);
                    return Description;
                }
                catch (Exception ex)
                {

                }

            }
            return null;
        }

        public async Task<Description> GetHeaderAsync(string header)
        {
            var respone = await _httpClient.GetFromJsonAsync<ApiRespone>($"/api/description/get_header/{header}");

            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var description = JsonConvert.DeserializeObject<Description>(data);
                    return description;
                }
                catch (Exception ex)
                {

                }

            }
            return new();
        }


        public async Task<List<Description>> ListAsync()
        {
            var respone = await _httpClient.GetFromJsonAsync<ApiRespone>("/api/Description/get_list");
            if (respone.success)
            {
                string data = respone.data.ToString();
                try
                {
                    var users = JsonConvert.DeserializeObject<List<Description>>(data);
                    return users;
                }
                catch (Exception ex)
                {

                }

            }
            return new List<Description>();
        }


    }
}
