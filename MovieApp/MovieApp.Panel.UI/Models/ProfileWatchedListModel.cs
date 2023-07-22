namespace MovieApp.Panel.UI.Models
{
    public class ProfileWatchedListModel
    {
        public int MovieId { get; set; }
        public List<int> WatchedListId { get; set; }
        public string MoviePoster { get; set; }
        public string MovieSlug { get; set; }
    }
}
