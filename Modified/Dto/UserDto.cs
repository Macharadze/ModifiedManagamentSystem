namespace Modified.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public bool isActive { get; set; } = false;
    }
}
