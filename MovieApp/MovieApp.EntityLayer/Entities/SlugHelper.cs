using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MovieApp.EntityLayer.Entities
{
    public static class SlugHelper
    {
        public static string GenerateSlug(string input)
        {
            string slug = input
                .ToLower()
                .Replace(" ", "-")
                .Replace("ç", "c")
                .Replace("ğ", "g")
                .Replace("ı", "i")
                .Replace("ö", "o")
                .Replace("ş", "s")
                .Replace("ü", "u");

            slug = Regex.Replace(slug, "[^a-z0-9\\-]", "");

            slug = Regex.Replace(slug, "-{2,}", "-");

            slug = slug.Trim('-');

            return slug;
        }
    }
}
