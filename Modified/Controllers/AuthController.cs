using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modified.Dto;
using Modified.Interfaces;
using Modified.Models;

namespace Modified.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth auth;
        public AuthController(IAuth auth)
        {
            this.auth = auth;
        }
        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto user)
        {
            var response = await auth.Register(
                new User {  Username = user.Username},user.Password,user.Email);
            if (!response.Succes)
                return BadRequest(response);
            return Ok(response);

        }
        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<int>>> Login(UserLogin request)
        {
            var response =  auth.Login(request.Email, request.Password);
            if (response == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
      
    }
}
