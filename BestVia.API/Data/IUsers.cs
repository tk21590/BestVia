using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BestVia.API.Data
{
    public class IUsers : IdentityUser
    {
        public int DateCreated { get; set; } //Ngày tạo
        public int DateLastLogin { get; set; } //Lần login cuối
        public long Balance { get; set; } //Tổng tiền hiện tại
        public long Balance_Total { get; set; } //Tổng tiền nạp
        public long Balance_Used { get; set; } //Tổng tiền sử dụng
        public int SubId { get; set; } //Số thứ tự
        public string RoleName { get; set; }
        public string RefCode { get; set; } //Mã giới thiệu , có thể có hoặc không
        public string RefAdd { get; set; } //Mã giới thiệu , có thể có hoặc không
        public string ApiKey { get; set; }//Apikey user dùng sử dụng API riêng
        public bool Block { get; set; }//User có bị khóa hay không
    }
}
