using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodingChallenge.Models
{
    public class ProductViewModel : IProduct
    {
        public string Id { get; set; }

        public string Category { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }
    }
}