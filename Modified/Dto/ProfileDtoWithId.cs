namespace Modified.Dto
{
    public class ProfileDtoWithId
    {
        public int UserProfileId { get; set; }
        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public string number { get; set; } = string.Empty;
        public int UserId { get; set; } 
    }
}
