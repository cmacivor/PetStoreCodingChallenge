using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessTier
{
    public class OrderDetail : IOrderDetail
    {
       public int OrderDetailId { get; set; }

       public int OrderId { get; set; }

       public string ProductId { get; set; }

       public int Quantity { get; set; }

       public string ProductCategory { get; set; }

       public string ProductName { get; set; }

       public decimal ProductPrice { get; set; }
 
       public decimal SubTotal { get => ProductPrice * Quantity; }
    }
}
