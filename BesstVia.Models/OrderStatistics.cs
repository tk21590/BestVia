using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BestVia.Models
{
    public class OrderStatistics
    {
        //Tổng đơn
        public int total_order { get; set; }
        //Tổng tiền đặt đơn thành công
        public long total_order_success_price { get; set; }
        //Tổng tiền đặt đơn chưa thành công
        public long total_order_not_success_price { get; set; }
        //Tổng đơn thành công
        public int count_order_success { get; set; }
        //Tổng tiền đặt đơn chưa thành công
        public int count_order_not_success { get; set; }
    }
}
