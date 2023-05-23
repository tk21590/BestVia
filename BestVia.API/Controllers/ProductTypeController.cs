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

    public class ProductTypeController : ControllerBase
    {
        private readonly IProductTypeServicesAPI _productTypeServices;
        public ProductTypeController(IProductTypeServicesAPI productTypeServices)
        {
            _productTypeServices = productTypeServices;

        }

        [HttpGet]
        [Route("get_list")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _productTypeServices.ListAsync());
        }
        [HttpGet]
        [Route("get_id/{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            return Ok(await _productTypeServices.GetIdAsync(id));
        }

        [HttpGet]
        [Route("get_name/{name}")]
        public async Task<IActionResult> GetName(string name)
        {
            return Ok(await _productTypeServices.GetNameAsync(name));
        }

        [HttpGet]
        [Route("search/{name}")]
        public async Task<IActionResult> SearchName(string name)
        {
            return Ok(await _productTypeServices.SearchListAsync(name));
        }
        [HttpPost]
        [Route("edit")]
        [Authorize(Roles = "Admin,SuperMod")]

        public async Task<IActionResult> EditProductType([FromBody] ProductType ptype)
        {
            return Ok(await _productTypeServices.EditAsync(ptype));
        }
        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "Admin,SuperMod")]

        public async Task<IActionResult> AddProductType([FromBody] ProductType ptype)
        {
            return Ok(await _productTypeServices.AddAsync(ptype));
        }

    }
}
