namespace MovieApp.Panel.UI.Models
{
    public class ProfileWatchListModel
    {
        public int MovieId { get; set; }
        public List<int> WatchListId { get; set; }
        public string MoviePoster { get; set; }
    }
}
