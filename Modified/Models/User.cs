using System.ComponentModel.DataAnnotations;

namespace Modified.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; } = new byte[0];
        public byte[] PasswordSalt { get; set; } = new byte[0];
        public string Email { get; set; } 
        public bool isActive { get; set; } = false;
        public UserProfile? Profile { get; set; }
    }
}
