using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingChallenge.Models.OrderEntry.Get
{
    public class OrderViewModel : IOrder
    {
        public int OrderId { get; set; }

        public int CustomerId { get; set; }

        public decimal TotalCost { get; set; }

        public List<IOrderDetail> OrderDetails { get; set; }
    }
}