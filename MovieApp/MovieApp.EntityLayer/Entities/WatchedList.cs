﻿using MovieApp.EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.EntityLayer.Entities
{
    public class WatchedList: BaseEntity
    {
        public int MovieId { get; set; }
        public Movie Movie { get; set; }

        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}
