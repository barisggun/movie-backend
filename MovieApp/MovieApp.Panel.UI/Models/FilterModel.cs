﻿using MovieApp.EntityLayer.Entities;

namespace MovieApp.Panel.UI.Models
{
    public class FilterModel
    {
        public List<int>? Years { get; set; }
        public List<int>? SelectedYears { get; set; }
        public List<int>? Categories { get; set; }
        public List<int>? SelectedCategories { get; set; }
        public List<int>? Ratings { get; set; }
        public List<int>? SelectedRatings { get; set; }
        public bool IsHighToLow { get; set; }
        public List<string>? CategoryNames { get; set; }
        public string? Sorting { get; set; }
    }

}
