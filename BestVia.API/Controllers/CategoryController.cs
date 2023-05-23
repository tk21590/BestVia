using BestVia.API.Services;
using BestVia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace BestVia.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServicesAPI _cateServices;
        public CategoryController(ICategoryServicesAPI cateServices)
        {
            _cateServices = cateServices;

        }

        [HttpGet]
        [Route("get_list")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _cateServices.ListAsync());
        }
        [HttpGet]
        [Route("get_id/{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            return Ok(await _cateServices.GetIdAsync(id));
        }

        [HttpGet]
        [Route("get_name/{name}")]
        public async Task<IActionResult> GetName(string name)
        {
            return Ok(await _cateServices.GetNameAsync(name));
        }

        [HttpGet]
        [Route("search/{name}")]
        public async Task<IActionResult> SearchName(string name)
        {
            return Ok(await _cateServices.SearchListAsync(name));
        }
        [HttpPost]
        [Route("edit")]
        public async Task<IActionResult> EditCate([FromBody] Category cate)
        {
            return Ok(await _cateServices.EditAsync(cate));
        }
        [HttpPost]
        [Route("add")]
        public async Task<IActionResult> AddCate([FromBody] Category cate)
        {
            return Ok(await _cateServices.AddAsync(cate));
        }


    }
}
