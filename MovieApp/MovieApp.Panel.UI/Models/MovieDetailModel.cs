namespace MovieApp.Panel.UI.Models
{
    public class MovieDetailModel
    {
        public int MovieId { get; set; }
        public List<string> DirectorNames { get; set; }
        public List<string> ActorNames { get; set; }
        public string MovieTitle { get; set; }
        public string MoviePoster { get; set; }
        public string MovieDetailPoster { get; set; }
        public string MovieDescription { get; set; }
    }
}
