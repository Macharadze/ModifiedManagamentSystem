using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modified.Dto;
using Modified.Interfaces;
using Modified.Models;

namespace Modified.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser user;
        public UserController(IUser user)
        {
            this.user = user;
        }

        [HttpGet("GetAllUser")]
        public async Task<ActionResult<ServiceResponse<UserDto>>> GetAllUser()
        {
            var response =await user.GetUsers();
            
        if(response == null)
                return BadRequest(response);

        return Ok(response);
        }
        
        [HttpGet("GetUserById")]
        public async Task<ActionResult<ServiceResponse<User>>> GetUser(int id)
        {
            var response = await user.GetUser(id);
            if(response == null)
                return BadRequest(response);

            return Ok(response);
        }
       
    }
}
