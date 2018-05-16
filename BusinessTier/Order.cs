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

        public int CustomerId { get; set; }

        public decimal TotalCost { get; set; }

        public List<IOrderDetail> OrderDetails { get; set; }

        public void CalculateTotalCost()
        {
            TotalCost = OrderDetails.Select(x => x.SubTotal).Sum();
        }
    }
}
