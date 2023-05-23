using Azure;
using BestVia.API.Services;
using BestVia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net.WebSockets;

namespace BestVia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class SummaryController : ControllerBase
    {
        private readonly ISummaryServicesAPI _sumServices;
        private readonly IHistoryServicesAPI _historyServices;
        public SummaryController(ISummaryServicesAPI sumServices, IHistoryServicesAPI historyServices)
        {
            _sumServices = sumServices;
            _historyServices = historyServices;

        }

        [HttpPost]
        [Route("get_data")]
        //Tổng kết đơn hàng trong thời gian tìm kiếm
        public async Task<IActionResult> GetData([FromBody] SearchModel searchModel)
        {
            var orders = await _sumServices.SearchOrders(searchModel);
            if (orders.Count > 0)
            {
                OrderStatistics statistics = new OrderStatistics();
                //Tổng đơn hàng;
                statistics.total_order = orders.Count;
                //Tổng đơn thành công
                var total_order_success = orders.Where(o => o.StatusId == 1);
                //Tổng đơn chưa thành công
                var total_order_not_success = orders.Where(o => o.StatusId != 1);

                //Tổng tiền đặt đơn thành công
                statistics.total_order_success_price = total_order_success.Sum(p => p.TotalPrice);
                //Tổng tiền đặt đơn chưa thành công
                statistics.total_order_not_success_price = total_order_not_success.Sum(p => p.TotalPrice);
                //Tổng đơn thành công
                statistics.count_order_success = total_order_success.Count();
                //Tổng tiền đặt đơn chưa thành công
                statistics.count_order_not_success = total_order_not_success.Count();
                var respone = Core.CreateRespone(respone_code.success, statistics, "");
                return Ok(respone);

            }
            return Ok(Core.CreateRespone(respone_code.error));

        }
        [HttpGet]
        [Route("get_history_last")]
        //Tổng kết đơn hàng trong thời gian tìm kiếm
        public async Task<IActionResult> GetHistoryLast()
        {
            var histories = await _sumServices.SearchHistoryLast();
            if (histories.Count > 0)
            {
                var respone = Core.CreateRespone(respone_code.success, histories, "Load lịch sử thành công!");
                return Ok(respone);

            }
            return Ok(Core.CreateRespone(respone_code.error));

        }

        [HttpPost]
        [Route("get_history_orders")]
        //Tổng kết đơn hàng trong thời gian tìm kiếm
        public async Task<IActionResult> GetHistoryOrder([FromBody] SearchModel searchModel)
        {
            var histories = await _sumServices.SearchHistoryOrder(searchModel);
            if (histories.Count > 0)
            {
                var respone = Core.CreateRespone(respone_code.success, histories, "Load lịch sử thành công!");
                return Ok(respone);

            }
            return Ok(Core.CreateRespone(respone_code.error));

        }
        [HttpPost]
        [Route("delete_history")]
        //Tổng kết đơn hàng trong thời gian tìm kiếm
        public async Task<IActionResult> DeleteHistory([FromBody] SearchModel searchModel)
        {

            var respone = await _historyServices.DeleteAsync(searchModel);
            if (respone)
            {
                return Ok(Core.CreateRespone(respone_code.success,null,"Xóa lịch sử thành công!"));

            }
            else
            {
                return Ok(Core.CreateRespone(respone_code.failed));

            }

        }
        [HttpPost]
        [Route("get_history_all")]
        //Tổng kết đơn hàng trong thời gian tìm kiếm
        public async Task<IActionResult> GetHistoryAll([FromBody] SearchModel searchModel)
        {
            var histories = await _sumServices.SearchHistoryAll(searchModel);
            if (histories.Count > 0)
            {
                var respone = Core.CreateRespone(respone_code.success, histories, "Load lịch sử thành công!");
                return Ok(respone);

            }
            return Ok(Core.CreateRespone(respone_code.error));

        }


        [HttpGet]
        [Route("get_userinfo/{userId}/{is_ref}")]
        //Tổng kết đơn hàng trong thời gian tìm kiếm
        public async Task<IActionResult> GetUserInfo(string userId, bool is_ref)
        {
            var userinfo = await _sumServices.SearchUserInfo(userId, is_ref);
            if (userinfo != null)
            {
                var respone = Core.CreateRespone(respone_code.success, userinfo);
                return Ok(respone);

            }
            return Ok(Core.CreateRespone(respone_code.error));

        }



    }
}
