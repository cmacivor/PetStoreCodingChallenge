using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IOrderDetail
    {
        int OrderDetailId { get; set; }

        int OrderId { get; set; }

        string ProductId { get; set; }

        int Quantity { get; set; }

        string ProductCategory { get; set; }

        string ProductName { get; set; }

        decimal ProductPrice { get; set; }

        decimal SubTotal { get; }
    }
}
