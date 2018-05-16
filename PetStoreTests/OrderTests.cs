using System;
using System.Collections.Generic;
using BusinessTier;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PetStoreTests
{
    [TestClass]
    public class OrderTests
    {
        [TestMethod]
        public void CalculateTotalCost_WhenCalled_SumsOrderItemPrices()
        {
            //Arrange
            var orderItems = new List<OrderDetail>();
            orderItems.Add(new OrderDetail
            {
                 ProductId = "70508895",
                 ProductName = "Scratching Post",
                 ProductPrice = 32.95m
            });
            orderItems.Add(new OrderDetail
            {
                ProductId = "8ed0e6f7",
                ProductName = "Stroller",
                ProductPrice = 124.95m
            });

            var order = new BusinessTier.Order
            {
                CustomerId = "123",
                OrderItems = orderItems
            };

            //Act
            order.CalculateTotalCost();


            //Assert
            Assert.AreEqual(157.90m, order.TotalCost);
        }
    }
}
