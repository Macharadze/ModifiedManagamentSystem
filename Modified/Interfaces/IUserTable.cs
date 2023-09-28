using Modified.Dto;
using Modified.Models;

namespace Modified.Interfaces
{
    public interface IUserTable
    {
        Task<ServiceResponse<UserProfile>> AddUserProfile(int userId, UserProfile profile);
        Task<ServiceResponse<ProfileDtoWithId>> UpdateUserProfile(int userProfileId, UserProfileDto profile);
        Task<ServiceResponse<List<ProfileDtoWithId>>> DeleteUserProfile(int userProfileId,int userId);
        Task<ServiceResponse<ProfileDtoWithId>> GetUserProfilesbyOwnerId(int userId);




    }
}
