using MovieApp.EntityLayer.Entities;

namespace MovieApp.Panel.UI.Models
{
    public class SearchResultsModel
    {
        public List<Movie> Movies { get; set; }
        public List<Blog> Blogs { get; set; }
        public List<AppUser> Users { get; set; }

    }
}
