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

    public class ViaController : ControllerBase
    {
        private readonly IViaServicesAPI _viaServices;
        public ViaController(IViaServicesAPI viaServices)
        {
            _viaServices = viaServices;

        }

        [HttpGet]
        [Route("get_list")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _viaServices.ListAsync());
        }
        [HttpGet]
        [Route("get_list_order/{orderid}")]
        public async Task<IActionResult> GetListOrder(int orderid)
        {
            return Ok(await _viaServices.ListOrderId(orderid));
        }
        [HttpGet]
        [Route("get_id/{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            return Ok(await _viaServices.GetIdAsync(id));
        }

        [HttpGet]
        [Route("get_name/{name}")]
        public async Task<IActionResult> GetName(string name)
        {
            return Ok(await _viaServices.GetNameAsync(name));
        }

    
        [HttpPost]
        [Route("edit")]
        [Authorize(Roles = "Admin,SuperMod")]

        public async Task<IActionResult> EditVia([FromBody] Via Via)
        {
            return Ok(await _viaServices.EditAsync(Via));
        }
        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "Admin,SuperMod")]

        public async Task<IActionResult> AddVia([FromBody] Via Via)
        {
            return Ok(await _viaServices.AddAsync(Via));
        }
        [HttpPost]
        [Route("add_range")]
        [Authorize(Roles = "Admin,SuperMod")]

        public async Task<IActionResult> AddRangeVia([FromBody] Via[] Vias)
        {
            return Ok(await _viaServices.AddRangeAsync(Vias));
        }

    }
}
