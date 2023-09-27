using System.ComponentModel.DataAnnotations;

namespace Modified.Models
{
    public class UserProfile
    {
        [Key]
        public int UserProfileId { get; set; }
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string number { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }

    }
}
