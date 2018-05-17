using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CodingChallenge.Models;
using System.Net;
using System.Net.Http;


namespace CodingChallenge.Infrastructure.HttpClient
{
    public class InventoryRetriever
    {
        //make this static so there's only one instance
        private static System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();

        //Not sure if this is a good way of doing it or not
        public static async Task<List<ProductViewModel>> GetInventoryAsync()
        {
            List<ProductViewModel> productViewModel = null;

            //got info on the HttpCompletionOption from here:
            //https://stackoverflow.com/questions/10343632/httpclient-getasync-never-returns-when-using-await-async
            //http://blog.stephencleary.com/2012/02/async-and-await.html
            //TODO: put this URI into an app setting
            HttpResponseMessage responseMessage = await client.GetAsync("https://petstoreapp.azurewebsites.net/api/products", HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);
            responseMessage.EnsureSuccessStatusCode();
            if (responseMessage.IsSuccessStatusCode)
            {
                productViewModel = await responseMessage.Content.ReadAsAsync<List<ProductViewModel>>();
            }
            return productViewModel;
        }
    }
}