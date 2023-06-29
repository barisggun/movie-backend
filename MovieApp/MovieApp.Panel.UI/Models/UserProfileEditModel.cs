namespace MovieApp.Panel.UI.Models
{
    public class UserProfileEditModel
    {
        public int UserID { get; set; }
        public string? NameSurname { get; set; }
        public string? About { get; set; }
        public string? OldPassword { get; set; }
        public string? NewPassword { get; set; }
        public string? EMail { get; set; }
        public string? ImageUrl { get; set; }
        public string? ProfilePictureUrl { get; set; }
    }
}
