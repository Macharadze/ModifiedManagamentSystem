using Modified.Dto;
using Modified.Models;

namespace Modified.Interfaces
{
    public interface IUserTable
    {
        Task<ServiceResponse<UserProfile>> AddUserProfile(int userId, UserProfile profile);
        Task<ServiceResponse<UserProfileDto>> UpdateUserProfile(int userProfileId, UserProfileDto profile);
        Task<ServiceResponse<UserProfileDto>> DeleteUserProfile(int userProfileId,int userId);
        Task<ServiceResponse<List<UserProfileDto>>> GetUserProfilesbyOwnerId(int userId);




    }
}
