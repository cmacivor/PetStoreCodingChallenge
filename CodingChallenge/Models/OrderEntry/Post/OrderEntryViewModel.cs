using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingChallenge.Models
{
    public class OrderEntryViewModel
    {
        public string customerId { get; set; }

        public List<OrderItemViewModel> items { get; set; }
    }
}