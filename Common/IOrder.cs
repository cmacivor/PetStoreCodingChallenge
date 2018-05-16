﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IOrder
    {
        int OrderId { get; set; }

        string CustomerId { get; set; }

        decimal TotalCost { get; set; }
    }
}
