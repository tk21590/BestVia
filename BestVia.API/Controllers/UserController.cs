using BestVia.API.Data;
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

    public class UserController : ControllerBase
    {
        private readonly IUserServicesAPI _userServices;
        public UserController(IUserServicesAPI userServices)
        {
            _userServices = userServices;

        }

        [HttpGet]
        [Route("get_list")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _userServices.ListAsync());
        }

        [HttpPost]
        [Route("edit")]
        [Authorize(Roles ="Admin,SuperMod")]

        public async Task<IActionResult> EditUser([FromBody]UserView user)
        {
            return Ok(await _userServices.EditAsync(user));
        }

        [HttpGet]
        [Route("get_id/{userid}")]
        public async Task<IActionResult> GetUserId(string userid)
        {
            return Ok(await _userServices.GetIdAsync(userid));
        }
        [HttpGet]
        [Route("get_name/{username}")]
        public async Task<IActionResult> GetUserName(string username)
        {
            return Ok(await _userServices.GetNameAsync(username));
        }

        [HttpGet]
        [Route("search_name/{username}")]
        public async Task<IActionResult> SearchListAsync(string username)
        {
            return Ok(await _userServices.SearchListAsync(username));
        }
    }
}
