using MovieApp.EntityLayer.Entities;

namespace MovieApp.Panel.UI.Models
{
    public class FilterModel
    {
        public List<int>? Years { get; set; }
        public List<Category>? Categories { get; set; }
        public List<int>? Ratings { get; set; }
        public bool IsHighToLow { get; set; }
    }

}
