using System;
using System.Collections.Generic;
using BusinessTier;
using Common;
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
            var orderItems = new List<IOrderDetail>();
            orderItems.Add(new OrderDetail
            {
                ProductId = "70508895",
                ProductName = "Scratching Post",
                Quantity = 1,
                ProductPrice = 32.95m
            });
            orderItems.Add(new OrderDetail
            {
                ProductId = "8ed0e6f7",
                ProductName = "Stroller",
                Quantity = 2,
                ProductPrice = 124.95m
            });

            var order = new BusinessTier.Order
            {
                CustomerId = 123,
                OrderDetails = orderItems
            };

            //Act
            order.CalculateTotalCost();

            //Assert
            Assert.AreEqual(282.85m, order.TotalCost);
        }
    }
}
