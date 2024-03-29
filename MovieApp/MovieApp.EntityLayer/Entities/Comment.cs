﻿using MovieApp.EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.EntityLayer.Entities
{
    public class Comment:BaseEntity
    {
        public int CommentUserNameId { get; set; }  
        public string CommentUserName { get; set; }
        public string CommentContent { get; set; }
        public string? ProfilePictureUrl { get; set; }
        public DateTime CommentDate { get; set; }
        public bool CommentStatus { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public string MovieSlug { get; set; }
    }
}
