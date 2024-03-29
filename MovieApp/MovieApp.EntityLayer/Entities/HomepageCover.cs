﻿using MovieApp.EntityLayer.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApp.EntityLayer.Entities
{
    public class HomepageCover:BaseEntity
    {

        [Required(ErrorMessage = "Anasayfa header fotoğrafı alanı boş geçilemez.")]
        [DisplayName("Anasayfa header fotoğrafı")]
        public string? ImageUrl { get; set; }


        [Required(ErrorMessage = "Anasayfa header yazısı alanı boş geçilemez.")]
        [DisplayName("Anasayfa header yazısı")]
        [MaxLength(80, ErrorMessage = "Anasayfa header yazısı maksimum 60 karakter olabilir")]
        [MinLength(5, ErrorMessage = "Anasayfa header yazısı minimum 5 karakter olabilir")]
        public string? Title { get; set; }

        [Required(ErrorMessage = "Anasayfa alt header yazısı alanı boş geçilemez.")]
        [DisplayName("Anasayfa alt header yazısı")]
        [MaxLength(80, ErrorMessage = "Anasayfa alt header yazısı maksimum 60 karakter olabilir")]
        [MinLength(5, ErrorMessage = "Anasayfa alt header yazısı minimum 5 karakter olabilir")]
        public string? BottomTitle { get; set; }
    }
}
