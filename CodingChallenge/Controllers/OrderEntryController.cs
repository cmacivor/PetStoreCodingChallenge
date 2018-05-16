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

namespace CodingChallenge.Controllers
{
    public class OrderEntryController : ApiController
    {

        // POST api/values
        public async void Post([FromBody]OrderEntryViewModel order)
        {
            //do error handling here- fail early

            var inventory = await GetInventory();

           var orderDTO = MapInventoryToBusinessObjects(order, inventory);

            orderDTO.CalculateTotalCost();
        }

        private static Order MapInventoryToBusinessObjects(OrderEntryViewModel order, List<ProductViewModel> inventory)
        {
            //mapping this manually but we could maybe use AutoMapper for this
            var orderDetails = new List<OrderDetail>();
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
                CustomerId = order.customerId,
                OrderItems = orderDetails
            };

            return orderDTO;
        }

        private static async Task<List<ProductViewModel>> GetInventory()
        {
            //put put in the cache if it's not already there
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
