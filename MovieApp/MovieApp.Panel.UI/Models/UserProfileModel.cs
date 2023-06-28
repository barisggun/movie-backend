namespace MovieApp.Panel.UI.Models
{
    public class UserProfileModel
    {
        public int UserID { get; set; }
        public int MovieID { get; set; }
        public int CommentID { get; set; }
        public DateTime CommentDate { get; set; }
        public string CommentContent { get; set; }
        //public string Poster { get; set; }
        public string UserRole { get; set; }
        public string UserName { get; set; }
        public string NameSurname { get; set; }
        public string About { get; set; }
        public string ImageUrl { get; set; }
        public string ProfileImageUrl { get; set; }
    }
}
