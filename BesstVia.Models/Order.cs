using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestVia.Models
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string UserOrderId { get; set; } //User đặt hàng
        public string UserOrderName { get; set; }//Tên
        public long TotalPrice { get; set; } //Tổng giá
        public long TotalPriceIncome { get; set; } //Tổng Hoa hồng
        public long TotalPriceIncomeMod { get; set; } //Tổng Hoa hồng
        public int DateOrder { get; set; }//Ngày đặt
        public int Quantity { get; set; }//Số lượng đặt
        public string Address { get; set; }//Địa chỉ
        public string User { get; set; }//Tên
        public string BillOrder { get; set; }//Mã vận đơn
        public string Phone { get; set; }//SĐT
        public int StatusId { get; set; }
        public Status Status { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public bool isComplain { get; set; } //Khiếu nại
        public string Reason { get; set; } //Lý do khiếu nại 

    }
}
