using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestVia.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "Dữ liệu không được để trống")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Username phải có độ dài ít nhất 5 kí tự")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Dữ liệu không được để trống")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Password phải có độ dài ít nhất 6 kí tự")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{6,32}$", ErrorMessage = "Password phải có viết hoa , viết thường và số , ít nhất 6 kí tự")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Dữ liệu không được để trống")]
        [DataType(DataType.Password, ErrorMessage = "Mật khẩu lặp lại không trùng khớp")]
        public string ConfirmPassword { get; set; }

        public string RefAdd { get; set; }
        //public string Token { get; set; }

    }
}
