using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestVia.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Userid { get; set; } //Thuộc user
        public string Username{ get; set; } //Thuộc user
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
        public long Price { get; set; }
        public long PriceIncome { get; set; } //Tiền hoa hồng (cài đặt)
        public long PriceIncomeMod { get; set; } //Tiền hoa hồng (cài đặt) cho mod
        public int Amount { get; set; }
        public int Combo { get; set; }
        public int Sold { get; set; } //Đã bán 
        public int Stock { get; set; } //Còn lại 
        public string Setting { get; set; } //Cấu hình
        public string Date { get; set; } //Ngày tạo
        public string IP { get; set; } //Loại ip
        public string Model { get; set; } //Loại đơn hàng
        public string Ram { get; set; }
        public string CPU { get; set; }
        public string Friend { get; set; } //Số bạn
        public string PathImage { get; set; } //Link ảnh
        public string UrlDownload { get; set; } //Link download tool
        public bool TwoFA { get; set; } // Có 2Fa
        public bool Backup { get; set; } //Có backup
        public bool Change { get; set; } //Change info
        public bool IsAvaiable { get; set; } //Hiển thị
        public int Guarantee { get; set; } //Bảo hành
        public int DateCreated { get; set; } //Ngày tạo
        public int DateUpdated { get; set; } //Ngày cập nhật.
        [NotMapped]
        public List<Via> list_via { get; set; }
        [NotMapped]
        public List<Proxy> list_proxy{ get; set; }
    }
}
