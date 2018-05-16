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


            //mapping this manually but we could use AutoMapper for this
            var orderDetails = new List<OrderDetail>();
            foreach (var detail in order.items)
            {
                orderDetails.Add(new OrderDetail
                {
                    ProductId = detail.productId,
                    Quantity = detail.quantity
                });
            }

            var orderDTO = new Order
            {
               CustomerId = order.customerId,
               OrderItems = orderDetails
            };
        }
    }
}
