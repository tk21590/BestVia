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

    public class ProductController : ControllerBase
    {
        private readonly IProductServicesAPI _productServices;
        public ProductController(IProductServicesAPI productServices)
        {
            _productServices = productServices;

        }

        [HttpGet]
        [Route("get_list")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _productServices.ListAsync());
        } 
        [HttpGet]
        [Route("get_list_id_category/{id_cate}")]
        public async Task<IActionResult> GetListIdCate(int id_cate)
        {
            return Ok(await _productServices.ListIdCateAsync(id_cate));
        }
        [HttpGet]
        [Route("get_id/{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            return Ok(await _productServices.GetIdAsync(id));
        }

        [HttpGet]
        [Route("get_name/{name}")]
        public async Task<IActionResult> GetName(string name)
        {
            return Ok(await _productServices.GetNameAsync(name));
        }

        [HttpGet]
        [Route("search/{name}")]
        public async Task<IActionResult> SearchName(string name)
        {
            return Ok(await _productServices.SearchListAsync(name));
        }
        [HttpPost]
        [Route("edit")]
        [Authorize(Roles = "Admin,SuperMod")]

        public async Task<IActionResult> EditProduct([FromBody] Product product)
        {
            return Ok(await _productServices.EditAsync(product));
        }
        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "Admin,SuperMod")]

        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            return Ok(await _productServices.AddAsync(product));
        }

    }
}
