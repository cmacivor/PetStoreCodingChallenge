using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public interface IPetStoreRepository
    {
        void Save(Common.IOrder order);

        Models.Customer GetCustomerById(int customerId);
    }
}
