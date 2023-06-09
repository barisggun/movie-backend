using MovieApp.EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.EntityLayer.Entities.ConnectionClasses
{
    public class DirectorMovie
    {
        public int DirectorId { get; set; }
        public Director Director { get; set; }


        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        
       
    }
}
