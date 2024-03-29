﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MovieApp.Panel.UI.Models
{
    public class UserSignUpViewModel
    {
        [Display(Name = "Ad Soyad")]
        [Required(ErrorMessage ="Lütfen ad soyad giriniz")]
        public string NameSurname { get; set; }

        [Display(Name="Şifre")]
        [Required(ErrorMessage = "Lütfen şifre giriniz")]
        public string Password { get; set; }

        [Display(Name ="Şifre Tekrar")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        public string  ConfirmPassword { get; set; }

        [Display(Name = "Mail")]
        [Required(ErrorMessage = "Lütfen mail adresi giriniz")]
        public string Mail { get; set; }

        [Display(Name = "Kullanıcı Adı")]
        [Required(ErrorMessage = "Lütfen kullanıcı adınızı giriniz")]
        public string UserName { get; set; }
        [Display(Name = "Profil Fotoğrafı")]
        public string? ProfilePictureUrl { get; set; }

        [Display(Name = "Kapak Fotoğrafı")]
        public string? ImageUrl { get; set; }

        [Display(Name = "Kullanım Koşulları")]
        public bool TermsOfUse { get; set; }
    }
}
