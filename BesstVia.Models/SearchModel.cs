using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestVia.Models
{
    public class SearchModel
    {
        public int from { get; set; }
        public int to { get; set; }
        public string userorderid { get; set; } //Của người nào
        public int historyid { get; set; }
        public string ref_add { get; set; } = "";
    }
}
