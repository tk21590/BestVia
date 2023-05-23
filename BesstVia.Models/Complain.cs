using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestVia.Models
{
    public class Complain
    {
        public int orderid { get; set; }
        public string reason { get; set; }
        public string userid { get; set; }
        public string username { get; set; }
    }
}
