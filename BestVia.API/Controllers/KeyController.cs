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

    public class KeyController : ControllerBase
    {
        private readonly IKeyServicesAPI _keySerivices;
        public KeyController(IKeyServicesAPI keyServices)
        {
            _keySerivices = keyServices;

        }

        [HttpGet]
        [Route("get_list")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _keySerivices.ListAsync());
        }
        [HttpGet]
        [Route("get_id/{id}")]
        public async Task<IActionResult> GetId(int id)
        {
            return Ok(await _keySerivices.GetIdAsync(id));
        }

        [HttpGet]
        [Route("get_name/{keyname}")]
        [AllowAnonymous]

        public async Task<IActionResult> GetName(string keyname)
        {
            return Ok(await _keySerivices.GetNameAsync(keyname));
        }

    
        [HttpPost]
        [Route("edit")]
        [Authorize(Roles = "Admin,SuperMod")]
        public async Task<IActionResult> EditKey([FromBody]Key key)
        {
            return Ok(await _keySerivices.EditAsync(key));
        }
        [HttpPost]
        [Route("add")]
        [Authorize(Roles = "Admin,SuperMod")]

        public async Task<IActionResult> AddKey([FromBody] Key key)
        {
            return Ok(await _keySerivices.AddAsync(key));
        }
        [HttpGet]
        [Route("delete/{keyid}")]
        [Authorize(Roles = "Admin,SuperMod")]

        public async Task<IActionResult> AddKey(int keyid)
        {
            return Ok(await _keySerivices.DeleteAsync(keyid));
        }
    }
}
