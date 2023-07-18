using MovieApp.EntityLayer.Entities;
using MovieApp.EntityLayer.Entities.ConnectionClasses;

namespace MovieApp.Panel.UI.Models
{
    public class MovieDetailModel
    {
        public int MovieId { get; set; }
        public List<string> DirectorNames { get; set; }
        public List<Actor> Actors { get; set; }
        public List<string> CategoryNames { get; set; }
        public string MovieTitle { get; set; }
        public string MoviePoster { get; set; }
        public string MovieDetailPoster { get; set; }
        public string MovieDescription { get; set; }
        public DateTime ReleaseDate { get; set; }
        public float? AverageRating { get; set; }
        public int UserRating { get; set; }
        public bool IsMovieAdded { get; set; }
        public bool IsMovieAddedToWatchedList { get; set; }
        public string MovieGifUrl { get; set; }
        public string? TrailerUrl { get; set; }
    }
}
