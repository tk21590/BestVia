using BestVia.API.Services;
using BestVia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BestVia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]

    public class ProxyController : ControllerBase
    {
        private readonly IProxyServicesAPI _proxyServices;
        public ProxyController(IProxyServicesAPI ProxyServices)
        {
            _proxyServices = ProxyServices;

        }

        [HttpGet]
        [Route("get_list")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _proxyServices.ListAsync());
        }
        [HttpGet]
        [Route("get_list_order/{orderid}")]
        public async Task<IActionResult> GetListOrder(int orderid)
        {
            return Ok(await _proxyServices.ListOrderId(orderid));
        }
    
        [HttpPost]
        [Route("edit")]
        [Authorize(Roles = "Admin,SuperMod")]

        public async Task<IActionResult> EditProxy([FromBody] Proxy Proxy)
        {
            return Ok(await _proxyServices.EditAsync(Proxy));
        }
        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "Admin,SuperMod")]

        public async Task<IActionResult> AddProxy([FromBody] Proxy Proxy)
        {
            return Ok(await _proxyServices.AddAsync(Proxy));
        }
        [HttpPost]
        [Route("add_range")]
        [Authorize(Roles = "Admin,SuperMod")]

        public async Task<IActionResult> AddRangeProxy([FromBody] Proxy[] Proxys)
        {
            return Ok(await _proxyServices.AddRangeAsync(Proxys));
        }

    }
}
