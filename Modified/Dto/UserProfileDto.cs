using System.ComponentModel.DataAnnotations;

namespace Modified.Dto
{
    public class UserProfileDto
    {
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string number { get; set; } = string.Empty;
    }
}
