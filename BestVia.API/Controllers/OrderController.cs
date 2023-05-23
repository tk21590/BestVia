using BestVia.API.Services;
using BestVia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BestVia.API.Services;
using BestVia.Models;
using Microsoft.AspNetCore.Authorization;

namespace BestVia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class OrderController : ControllerBase
    {
        private readonly IOrderServicesAPI _orderServices;
        public OrderController(IOrderServicesAPI orderServices)
        {
            _orderServices = orderServices;

        }

        [HttpGet]
        [Route("get_list")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _orderServices.ListAsync());
        }
        [HttpGet]
        [Route("get_list_userid/{userId}")]
        public async Task<IActionResult> GetList(string userId)
        {
            return Ok(await _orderServices.ListIdAsync(userId));
        }
        [HttpPost]
        [Route("get_list_ref")]
        public async Task<IActionResult> GetListRef([FromBody ]SearchModel search)
        {
            return Ok(await _orderServices.ListRefAsync(search));
        }

        [HttpGet]
        [Route("get_id/{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            return Ok(await _orderServices.GetIdAsync(id));
        }


        [HttpPost]
        [Route("search")]
        public async Task<IActionResult> SearchNumber([FromBody] SearchModel search)
        {
            return Ok(await _orderServices.SearchListAsync(search));
        }
        [HttpPost]
        [Route("edit")]
        [Authorize(Roles = "Admin,SuperMod")]
        public async Task<IActionResult> EditOrder([FromBody] Order order)
        {
            return Ok(await _orderServices.EditAsync(order));
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddOrder([FromBody] Order order)
        {
            return Ok(await _orderServices.AddAsync(order));
        }

        [HttpPost]
        [Route("complain")]
        public async Task<IActionResult> Complain([FromBody] Complain complain)
        {
            return Ok(await _orderServices.ComplainAsync(complain));
        }
    }
}
