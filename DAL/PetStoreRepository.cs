using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PetStoreRepository : IPetStoreRepository
    {
        public void Save(Common.IOrder order)
        {
            using (var context = new PetStoreDbContext())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        //check if customer exists, if not, add them
                        Customer customer;
                        customer = context.Customers.FirstOrDefault(x => x.CustomerId == order.CustomerId);
                        if (customer == null)
                        {
                            var newCustomer = new Customer
                            {
                                CustomerId = order.CustomerId 
                            };

                            context.Customers.Add(newCustomer);
                            context.SaveChanges();

                            SaveOrder(order, context, newCustomer);
                        }
                        else //customer already in the database
                        {
                            SaveOrder(order, context, customer);
                        }
                        dbContextTransaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        //Todo: implement some error handling here
                        dbContextTransaction.Rollback();
                    }
                }
            }
        }

        private static void SaveOrder(Common.IOrder order, PetStoreDbContext context, Customer customer)
        {
            int customerId = (customer == null) ? order.CustomerId : customer.CustomerId;

            var newOrder = new Order
            {
                CustomerId = customerId, //customer.CustomerId,
                TotalCost = order.TotalCost
            };

            context.Orders.Add(newOrder);

            context.SaveChanges();

            //now save the order details
            foreach (var detail in order.OrderDetails)
            {
                var orderDetail = new OrderDetail
                {
                    OrderId = newOrder.OrderId,
                    ProductId = detail.ProductId,
                    Quantity = detail.Quantity,
                    ProductCategory = detail.ProductCategory,
                    ProductName = detail.ProductName,
                    ProductPrice = detail.ProductPrice
                };

                context.OrderDetails.Add(orderDetail);
            }

            context.SaveChanges();
        }
    }
}
