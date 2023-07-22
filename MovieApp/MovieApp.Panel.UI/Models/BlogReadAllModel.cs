namespace MovieApp.Panel.UI.Models
{
    public class BlogReadAllModel
    {
        public int UserID { get; set; }
        //public int MovieID { get; set; }
        public int BlogID { get; set; }
        public string Slug { get; set; }
        public string MovieSlug { get; set; }
        public DateTime BlogDate { get; set; }
        public string BlogContent { get; set; }
        public string BlogTitle { get; set; }
        public string UserRole { get; set; }
        public string NameSurname { get; set; }
        public string BlogImage { get; set; }
        public string ProfilePicture { get; set; }
        public int MovieId { get; set; }
        public string MovieName { get; set; }
    }
}
