﻿using MovieApp.EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.EntityLayer.Entities
{
    public class Actor : BaseEntity
    {
        public string ActorName { get; set; }
        public List<Movie> Movies { get; set; }
    }
}