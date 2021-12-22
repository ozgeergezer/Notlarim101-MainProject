using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Notlarim101.Entity.ValueObject
{
    public class RegisterViewModel
    {
        [DisplayName("Adi"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(30, ErrorMessage = "{0} max. {1} karakter olmalı")]
        public string Name { get; set; }
        [DisplayName("Soyadi"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(30, ErrorMessage = "{0} max. {1} karakter olmalı")]
        public string SurName { get; set; }
        [DisplayName("Kullanici Adi"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(30, ErrorMessage = "{0} max. {1} karakter olmalı")]
        public string UserName { get; set; }
        [DisplayName("Email"), Required(ErrorMessage = "{0} alanı boş geçilemez."), StringLength(100, ErrorMessage = "{0} alanı için geçerli {1} karakter olmali.")]
        public string EMail { get; set; }
        [DisplayName("Şifre"), Required(ErrorMessage = "{0} alanı boş geçilemez."), DataType(DataType.Password), StringLength(30, ErrorMessage = "{0} max. {1} karakter olmalı")]
        public string Password { get; set; }
        [DisplayName("Şifre Tekrar"), Required(ErrorMessage = "{0} alanı boş geçilemez."), DataType(DataType.Password), StringLength(30, ErrorMessage = "{0} max. {1} karakter olmalı"),Compare("Password",ErrorMessage ="{0} ile {1} uyusmuyor ")]
        public string RePassword { get; set; }
    }
}