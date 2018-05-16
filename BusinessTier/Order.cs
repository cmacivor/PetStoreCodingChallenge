using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTier
{
    public class Order : IOrder
    {
        public int OrderId { get; set; }

        public string CustomerId { get; set; }

        public decimal TotalCost { get; set; }

        public List<OrderDetail> OrderItems { get; set; }
    }
}
