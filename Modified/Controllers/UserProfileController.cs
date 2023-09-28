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

        [HttpPut("UserProfile/{userProfileId}")]
        public async Task<ActionResult<ServiceResponse<ProfileDtoWithId>>> UpdateUserProfile(int userProfileId, UserProfileDto profile)
        {
            var response =user.UpdateUserProfile(userProfileId, profile);

            return Ok(response);
        }

        [HttpDelete("UserProfile/{userProfileId}")]
       public async Task<ActionResult<ServiceResponse<List<ProfileDtoWithId>>>> DeleteUserProfile(int userProfileId, int userId)
        {
            return Ok(user.DeleteUserProfile(userProfileId, userId));
        }
        [HttpGet("User/UserProfile/{userId}")]
     public async   Task<ActionResult<ServiceResponse<List<ProfileDtoWithId>>>> GetUserProfilesbyOwnerId(int userId)
        {
            return Ok(user.GetUserProfilesbyOwnerId(userId));
        }
    }
}
