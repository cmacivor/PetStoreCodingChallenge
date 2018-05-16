using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingChallenge.Models.OrderEntry.Get
{
    public class CustomerViewModel : ICustomer
    {
        public int CustomerId { get; set; }

        public List<OrderViewModel> Orders { get; set; }
    }
}