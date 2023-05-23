using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestVia.Models
{
    public class Via
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UID { get; set; }
        public string Userid { get; set; }//Chủ sở hữu
        public string Username { get; set; }//Chủ sở hữu
        public string User { get; set; }
        public string Password { get; set; }
        public string TwoFa { get; set; }
        public string Mail { get; set; }
        public string PasswordMail { get; set; }
        public int OrderId { get; set; } //Thuộc đơn hàng nào
        public int ProductId { get; set; } //Thuộc product nào
        public Product Product { get; set; }
        public bool IsSold { get; set; } //Đã bán hay chưa
    }
}
