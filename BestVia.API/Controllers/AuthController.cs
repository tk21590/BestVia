using BestVia.API.Data;
using BestVia.API.Services;
using BestVia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BestVia.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthServicesAPI _authServices;
        public AuthController(IAuthServicesAPI authServices)
        {
            _authServices = authServices;

        }
        [HttpGet]
        [Route("/")]
        public IActionResult GET()
        {
            return Ok("Welcome to API BestVia");
        }
        #region POST METHODS
        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody]LoginModel login_model)
        {
            var respone = await _authServices.LoginUserAsync(login_model);
            return Ok(respone);
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterModel reg_model)
        {
            var respone = await _authServices.RegisterUserAsync(reg_model);
            return Ok(respone);
        }

        #endregion

    }
}
