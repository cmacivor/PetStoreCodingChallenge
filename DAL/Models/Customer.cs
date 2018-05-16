﻿using Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Models
{
    public class Customer : ICustomer
    {
        //[DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CustomerId { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}
