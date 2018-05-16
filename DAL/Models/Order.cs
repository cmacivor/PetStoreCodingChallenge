using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Order 
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public decimal TotalCost { get; set; }


        public virtual Customer Customer { get; set; }

        public virtual List<OrderDetail> OrderDetails { get; set; }
    }
}
