using MovieApp.EntityLayer.Entities;

namespace MovieApp.Panel.UI.Models
{
    public class MovieListModel
    {
        public FilterModel Filter { get; set; }
        public List<Movie> Movies { get; set; }

        public MovieListModel()
        {
            Filter = new FilterModel();
            Movies = new List<Movie>(); // Movies özelliğini başlat
        }
    }
}
