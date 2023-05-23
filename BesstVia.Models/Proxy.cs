
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace BestVia.Models
{
    public class Proxy
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; } //Thuộc product nào
        public Product Product { get; set; }
        public string Userid { get; set; }//Chủ sở hữu
        public string Username { get; set; }//Chủ sở hữu
        public int OrderId { get; set; } //Thuộc đơn hàng nào
        public bool IsSold { get; set; } //Đã bán hay chưa


    }
}
