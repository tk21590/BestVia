using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestVia.Models
{
    public class History
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } //Id
        public string Userid { get; set; } //Thuộc sở hữu của tài khoản nào
        public string Username { get; set; } //Thuộc sở hữu của tài khoản nào

        [ForeignKey("HistoryTypeId")]
        public int HistoryTypeId { get; set; }
        public HistoryType HistoryType { get; set; }
        public string Header { get; set; } //Thông tin phụ
        public string Content { get; set; } //Thông tin chính
        public long Value { get; set; } //Số tiền
        public int OrderId { get; set; } //Đơn đặt hàng
        public int DateCreated { get; set; } //Thời gian tạo

    }
}
