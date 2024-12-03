﻿using System.ComponentModel.DataAnnotations;

namespace Ecomerce_Web.ViewModels
{
    public class LoginVM
    {
        [Display(Name = "Tên đăng nhập")]
        [Required(ErrorMessage ="*")]
        [MaxLength(20,ErrorMessage = "Tối đa 20 ký tự")]
        public string Username { get; set; }

        [Display(Name = "Mật khẩu")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
