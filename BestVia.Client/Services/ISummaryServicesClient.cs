using BestVia.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http.Json;
using System.Net.WebSockets;

namespace BestVia.Client.Services
{
    public interface ISummaryServicesClient
    {

        Task<OrderStatistics> TongKetDoanhThu(SearchModel searchModel);
        Task<List<History>> DanhSachLichSuDatHang(SearchModel searchModel);
        Task<List<History>> DanhSachLichSuToanBo(SearchModel searchModel);
        Task<List<History>> DanhSachLichSuQuangCao();
        Task<ApiRespone> XoaLichSu(SearchModel searchModel);
        Task<UserInfo> LayThongTinUser(string userId, bool is_ref);
    }

    public class SummaryServicesClient : ISummaryServicesClient
    {
        private readonly HttpClient _httpClient;
        public SummaryServicesClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<OrderStatistics> TongKetDoanhThu(SearchModel searchModel)
        {
            try
            {
                var respone = await _httpClient.PostAsJsonAsync("/api/summary/get_data", searchModel);
                var body_respone = await respone.Content.ReadFromJsonAsync<ApiRespone>();
                if (body_respone.success)
                {
                    var data = body_respone.data.ToString();
                    OrderStatistics obj = JsonConvert.DeserializeObject<OrderStatistics>(data);
                    return obj;
                }
                return new OrderStatistics();
            }
            catch (Exception ex)
            {

                return new OrderStatistics();
            }

        }

        public async Task<List<History>> DanhSachLichSuToanBo(SearchModel searchModel)
        {
            try
            {
                var respone = await _httpClient.PostAsJsonAsync("/api/summary/get_history_all", searchModel);
                var body_respone = await respone.Content.ReadFromJsonAsync<ApiRespone>();
                if (body_respone.success)
                {
                    var data = body_respone.data.ToString();
                    List<History> obj = JsonConvert.DeserializeObject<List<History>>(data);
                    return obj;
                }
            }
            catch (Exception ex)
            {

            }
            return new List<History>();

        }
        public async Task<List<History>> DanhSachLichSuDatHang(SearchModel searchModel)
        {
            try
            {
                var respone = await _httpClient.PostAsJsonAsync("/api/summary/get_history_orders", searchModel);
                var body_respone = await respone.Content.ReadFromJsonAsync<ApiRespone>();
                if (body_respone.success)
                {
                    var data = body_respone.data.ToString();
                    List<History> obj = JsonConvert.DeserializeObject<List<History>>(data);
                    return obj;
                }
            }
            catch (Exception ex)
            {

            }
            return new List<History>();

        }
        public async Task<UserInfo> LayThongTinUser(string userId, bool is_ref)
        {
            try
            {
                var body_respone = await _httpClient.GetFromJsonAsync<ApiRespone>($"/api/summary/get_userinfo/{userId}/{is_ref}");
                if (body_respone.success)
                {
                    var data = body_respone.data.ToString();
                    UserInfo obj = JsonConvert.DeserializeObject<UserInfo>(data);
                    return obj;
                }
            }
            catch (Exception)
            {

            }
            return null;

        }

        public async Task<ApiRespone> XoaLichSu(SearchModel searchModel)
        {
            var respone = await _httpClient.PostAsJsonAsync("/api/summary/delete_history", searchModel);
            return await respone.Content.ReadFromJsonAsync<ApiRespone>();


        }

        public async Task<List<History>> DanhSachLichSuQuangCao()
        {
            try
            {
                var body_respone = await _httpClient.GetFromJsonAsync<ApiRespone>($"/api/summary/get_history_last");
                if (body_respone.success)
                {
                    var data = body_respone.data.ToString();
                    List<History> obj = JsonConvert.DeserializeObject<List<History>>(data);
                    return obj;
                }
            }
            catch (Exception)
            {

            }
            return null;
        }
    }
}
