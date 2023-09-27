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
    public class UserProfileController : ControllerBase
    {
        private readonly IUserTable user;
        public UserProfileController(IUserTable user)
        {
            this.user = user;
        }
        [HttpPost("addUserProfile")]
        public async Task<ActionResult<ServiceResponse<UserProfile>>> AddProfile(int userId, UserProfileDto profile)
        {
            var response = user.AddUserProfile(userId, new UserProfile { FirstName = profile.FirstName, LastName = profile.LastName, number = profile.number, UserId = userId });
            if (response == null)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
      

    }
}
