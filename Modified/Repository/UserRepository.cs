using Azure;
using Modified.Data;
using Modified.Dto;
using Modified.Interfaces;
using Modified.Models;

namespace Modified.Repository
{
    public class UserRepository : IUser
    {
        private readonly MyDB myDB;
        public UserRepository(MyDB db)
        {
            myDB = db;
        }
        

        public async Task<ServiceResponse<List<UserDto>>> DeleteUser(int id)
        {
            var response =new ServiceResponse<List<UserDto>>();
            var target = myDB.Users.Where(i=>i.Id == id).FirstOrDefault();
            if (target == null)
            {
                response.Succes = false;
                response.Message = "the user does not exists";
                return response;
            }
             myDB.Users.Remove(target);
            await myDB.SaveChangesAsync();
            response.Data = MappToDtos(myDB.Users.OrderBy(i=>i.Id).ToList());
            return response;

        }

        public async Task<ServiceResponse<User>> GetUser(int id)
        {
            var response = new ServiceResponse<User>();

            var target =  myDB.Users.FirstOrDefault(i => i.Id == id);
            if (target == null)
            {
                response.Succes = false;
                response.Message = "the user does not exists";
                return  response;
            }
            response.Data = target;
            return await Task.FromResult(response);
        }

        public async Task<ServiceResponse<List<UserDto>>> GetUsers()
        {
            var response = new ServiceResponse<List<UserDto>>();
            var target =  myDB.Users.OrderBy(i => i.Id).ToList();
            if (target.Count==0)
            {
                response.Succes = false;
                response.Message = "the users does not exists";
                return response;
            }
            response.Data =MappToDtos(target);
            return await Task.FromResult(response);
        }


        public async Task<ServiceResponse<UserDto>> UpdateUser(int id,UserDto userDto)
        {
            var response = new ServiceResponse<UserDto>();
            var target =myDB.Users.Where(i => i.Id == id).FirstOrDefault();
            if (target == null)
            {
                response.Succes = false;
                response.Message = "the user does not exists";
                return await Task.FromResult(response);
            }
            target.Username = userDto.Username;
            target.Email = userDto.Email;
            response .Data = MapToDto(target);
            myDB.Users.Update(target);
            await myDB.SaveChangesAsync(); 
            return response;
        }

     
        private List<UserDto> MappToDtos(List<User> users) 
        {
             List<UserDto> result = new List<UserDto>();
            foreach (var i in users)
            {
                var userDto = new UserDto();
                userDto.Id= i.Id;
                userDto.Username= i.Username;
                userDto.isActive = i.isActive;
                userDto.Email = i.Email;
                result.Add(userDto);
            }
            return result;
        }
        private UserDto MapToDto(User user)
        {
            var result = new UserDto();
            result.Id = user.Id;
            result.Username = user.Username;
            result.isActive = user.isActive;
            result.Email = user.Email;
            return result;
        }

    }
}
