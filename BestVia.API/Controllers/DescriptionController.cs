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

    public class DescriptionController : ControllerBase
    {
        private readonly IDescriptionServicesAPI _descriptionSerivices;
        public DescriptionController(IDescriptionServicesAPI descriptionServices)
        {
            _descriptionSerivices = descriptionServices;

        }

        [HttpGet]
        [Route("get_list")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _descriptionSerivices.ListAsync());
        }
        [HttpGet]
        [Route("get_id/{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            return Ok(await _descriptionSerivices.GetIdAsync(id));
        }

        [HttpGet]
        [Route("get_header/{header}")]
        public async Task<IActionResult> GetName(string header)
        {
            return Ok(await _descriptionSerivices.GetHeaderAsync(header));
        }

    
        [HttpPost]
        [Route("edit")]
        [Authorize(Roles = "Admin,SuperMod")]

        public async Task<IActionResult> EditDescription([FromBody] Description Description)
        {
            return Ok(await _descriptionSerivices.EditAsync(Description));
        }
        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "Admin,SuperMod")]

        public async Task<IActionResult> AddDescription([FromBody] Description Description)
        {
            return Ok(await _descriptionSerivices.AddAsync(Description));
        }

    }
}
