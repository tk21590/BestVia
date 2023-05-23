using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestVia.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Dữ liệu không được để trống")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Username phải có độ dài ít nhất 5 kí tự")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Dữ liệu không được để trống")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Password phải có độ dài ít nhất 5 kí tự")]
        public string Password { get; set; }
        //[Required(ErrorMessage = "Chưa giải captcha")]

        //public string Token { get; set; }
    }
}
