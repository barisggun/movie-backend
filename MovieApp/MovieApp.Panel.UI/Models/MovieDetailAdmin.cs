namespace MovieApp.Panel.UI.Models
{
    public class MovieDetailAdmin
    {
        public int MovieId { get; set; }
        public List<string> DirectorNames { get; set; }
        public List<string> ActorNames { get; set; }
        public List<string> CategoryNames { get; set; }
        public string MovieTitle { get; set; }
        public string MoviePoster { get; set; }
        public string MovieDetailPoster { get; set; }
        public string MovieDescription { get; set; }
        public DateTime ReleaseDate { get; set; }
        public float? AverageRating { get; set; }
        
    }
}
