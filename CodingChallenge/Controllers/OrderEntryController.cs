using BusinessTier;
using CodingChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CodingChallenge.Infrastructure.HttpClient;
using CodingChallenge.Infrastructure.Caching;
using Common;
using DAL;
using CodingChallenge.Models.OrderEntry.Get;

namespace CodingChallenge.Controllers
{
    public class OrderEntryController : ApiController
    {
        private readonly IPetStoreRepository _repository;

        public OrderEntryController(IPetStoreRepository petStoreRepository)
        {
            _repository = petStoreRepository;
        }


        // POST api/values
        public async void Post([FromBody]OrderEntryViewModel order)
        {
           //TODO error handling here- fail early

           var inventory = await GetInventory();

           var orderDTO = MapInventoryToBusinessObjects(order, inventory);

           orderDTO.CalculateTotalCost();

           _repository.Save(orderDTO);
        }
            
        public IHttpActionResult Get(int id)
        {
            var ordersByCustomerId = _repository.GetCustomerById(id);

            var orderVMs = new List<OrderViewModel>();

            foreach (var order in ordersByCustomerId.Orders)
            {
                var orderDetails = new List<IOrderDetail>();
                foreach (var detail in order.OrderDetails)
                {
                    var orderDetailVM = new OrderDetailViewModel
                    {
                         OrderDetailId = detail.OrderDetailId,
                         OrderId = detail.OrderId,
                         ProductCategory = detail.ProductCategory,
                         ProductId = detail.ProductId,
                         ProductName = detail.ProductName,
                         ProductPrice = detail.ProductPrice,
                         Quantity = detail.Quantity
                    };

                    orderDetails.Add(orderDetailVM);
                }

                var orderVM = new OrderViewModel
                {
                     OrderId = order.OrderId,
                     TotalCost = order.TotalCost,
                     OrderDetails = orderDetails
                };

                orderVMs.Add(orderVM);
            }

            var customerVM = new CustomerViewModel
            {
                CustomerId = ordersByCustomerId.CustomerId,
                Orders = orderVMs
            };

            return Ok(new { results = customerVM });
        }

        private static Order MapInventoryToBusinessObjects(OrderEntryViewModel order, List<ProductViewModel> inventory)
        {
            //mapping this manually but we could maybe use AutoMapper for this
            var orderDetails = new List<IOrderDetail>();
            foreach (var detail in order.items)
            {
                var productFromInventory = inventory.FirstOrDefault(x => x.Id == detail.productId);
                orderDetails.Add(new OrderDetail
                {
                    ProductId = detail.productId,
                    Quantity = detail.quantity,
                    ProductName = productFromInventory.Name,
                    ProductCategory = productFromInventory.Category,
                    ProductPrice = productFromInventory.Price
                });
            }

            var orderDTO = new Order
            {
                CustomerId = Convert.ToInt32(order.customerId),
                OrderDetails = orderDetails
            };
          
            return orderDTO;
        }

        private static async Task<List<ProductViewModel>> GetInventory()
        {
            //put inventory in the cache if it's not already there- this way we avoid having to make an API call for every product
            List<ProductViewModel> inventory;
            var cache = (List<ProductViewModel>)MemoryCacher.GetValue("Inventory");
            if (cache == null)
            {
                //Call the service to get the whole inventory
                inventory = await InventoryRetriever.GetInventoryAsync();
                MemoryCacher.Add("Inventory", inventory, DateTimeOffset.UtcNow.AddMinutes(15));
            }
            else
            {
                inventory = cache;
            }

            return inventory;
        }
    }
}
