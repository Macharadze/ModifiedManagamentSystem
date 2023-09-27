using Modified.Data;
using Modified.Dto;
using Modified.Interfaces;
using Modified.Models;

namespace Modified.Repository
{
    public class UserProfileRepository : IUserTable
    {
        private readonly MyDB myDB;

        public UserProfileRepository(MyDB db)
        {
            myDB = db;
        }

        public async Task<ServiceResponse<UserProfile>> AddUserProfile(int userId, UserProfile profile)
        {
            var response = new ServiceResponse<UserProfile>();
            var target =  myDB.Users.FirstOrDefault(i => i.Id == userId);

            if (target == null)
            {
                response.Succes = false;
                response.Message = "does not exists";
                return response;
            }
          profile.User = target;
          target.Profile = profile;
           

            response.Data = profile;
             myDB.UserProfiles.Add(profile);
            myDB.Users.Update(target);
            await myDB.SaveChangesAsync();
            return response;

        }
      

        public async Task<ServiceResponse<UserProfileDto>> DeleteUserProfile(int userProfileId, int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<List<UserProfileDto>>> GetUserProfilesbyOwnerId(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<ServiceResponse<UserProfileDto>> UpdateUserProfile(int userProfileId, UserProfileDto profile)
        {
            throw new NotImplementedException();
        }
        
    }
}
