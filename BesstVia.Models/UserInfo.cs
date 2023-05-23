using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestVia.Models
{
    public class UserInfo
    {
        public string apikey { get; set; }
        public long balance { get; set; }
        public long balance_used { get; set; }
        public long balance_total { get; set; }
        public string username { get; set; }
        public string refcode { get; set; }
        public bool block { get; set; }
        public long total_income { get; set; }
        public long total_ref { get; set; }
    }
}
