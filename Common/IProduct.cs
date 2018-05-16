using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IProduct
    {
        string Id { get; set; }

        string Category { get; set; }

        string Name { get; set; }

        decimal Price { get; set; }
    }
}
