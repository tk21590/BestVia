using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestVia.Models
{
    public class Key
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserId { get; set; } //Người sở hữu
        public string UserName { get; set; }//Người  sở hữu
        public string KeyName { get; set; }//KEY NAME
        public string KeyId { get; set; }//KEY NAME
        public int DateCreated { get; set; }//Ngày tạo
        public int DateExpired { get; set; }//Ngày hết hạn
        public bool IsAvaiable { get; set; }//Còn hiệu lực 
        public int OrderId { get; set; }//Thuộc đơn hàng nào
        public Order Order { get; set; }//Thuộc đơn hàng nào
        public int ProductId { get; set; }//Thuộc sản phẩm nào
    }
}
