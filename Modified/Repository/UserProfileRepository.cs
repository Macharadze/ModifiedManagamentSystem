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
      

        public async Task<ServiceResponse<List<ProfileDtoWithId>>> DeleteUserProfile(int userProfileId, int userId)
        {
            var response = new ServiceResponse<List<ProfileDtoWithId>>();
            var owner = myDB.Users.FirstOrDefault(i=>i.Id==userId);
            
            if ( owner == null )
            {
                response.Succes = false;
                response.Message = "does not exists1";
                return response;
            }
           
         
            myDB.UserProfiles.Remove(myDB.UserProfiles.First(i=>i.UserProfileId==userProfileId));
            await myDB.SaveChangesAsync();
            response.Data = MapToListOfUserProfile(myDB.UserProfiles.OrderBy(i=>i.UserProfileId).ToList());
            return response;
        }

        public async Task<ServiceResponse<ProfileDtoWithId>> GetUserProfilesbyOwnerId(int userId)
        {
            var response = new ServiceResponse<ProfileDtoWithId>();
            var target = myDB.UserProfiles.Where(i => i.UserId == userId).FirstOrDefault();

            if (target == null)
            {
                response.Succes = false;
                response.Message = "does not exists";
                return response;
            }
            response.Data = new ProfileDtoWithId()
            {
                UserId = userId,
                LastName = target.LastName,
                FirstName = target.FirstName,
                number = target.number,
                UserProfileId = target.UserProfileId,
            };
            return await Task.FromResult(response);
        }

        public async Task<ServiceResponse<ProfileDtoWithId>> UpdateUserProfile(int userProfileId, UserProfileDto profile)
        {
            var response = new ServiceResponse<ProfileDtoWithId>();
            var target = myDB.UserProfiles.FirstOrDefault(i=>i.UserId==userProfileId);

            if (target == null)
            {
                response.Succes = false;
                response.Message = "does not exists";
                return response;
            }
            var i = new ProfileDtoWithId()
            {
              FirstName = profile.FirstName,
              LastName = profile.LastName,
              number = profile.number,
              UserProfileId = userProfileId,
              UserId = target.UserId
              
            }
            ;
            target.LastName = profile.LastName;
            target.FirstName = profile.FirstName;
            target.number = profile.number;
            response.Data = i;
            myDB.UserProfiles.Update(target);
            myDB.SaveChanges();
            return response;
        }
        
    private List<ProfileDtoWithId> MapToListOfUserProfile (List<UserProfile> userProfiles)
        {
            var list = new List<ProfileDtoWithId>();
            foreach (var item in list)
            {
                var helper = new ProfileDtoWithId()
                {
                    LastName = item.LastName,
                    FirstName = item.FirstName,
                    number = item.number,
                    UserId = item.UserId,
                    UserProfileId = item.UserProfileId,
                    
                };
                list.Add(helper);
            }
            return list;
        }
    }
}
