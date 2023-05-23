using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestVia.Models
{
    public class Category
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }// "Shopee", "Facebook", "Lazada", "Gmail", "Telegram", "Toss", "Bnb", "Finance", "Zalo", "Tiki"
        [Required]

        public int Price { get; set; } //giá sim
        [Required]

        public bool isAvaiable { get; set; } //Tồn tại hay không

    }
}
