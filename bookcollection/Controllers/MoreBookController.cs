using Azure;
using bookcollection.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static System.Reflection.Metadata.BlobBuilder;

namespace bookcollection.Controllers
{
    public class MoreBookController : Controller
    {
        private readonly string apiKey = "AIzaSyAptgA8XirEQGG_mq-2eCbqVqBvAOgx4i8";
        public async Task<IActionResult> Index(string query)
        {
            if(query is null)
            {
                query = "bestsellers";
                string url = $"https://www.googleapis.com/books/v1/volumes?q={query}&key={apiKey}";

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync(url);
                    var books = JsonConvert.DeserializeObject<GoogleBooksResponse>(response);
                    return View(books);
                }
            }
            else
            {
                string url = $"https://www.googleapis.com/books/v1/volumes?q={query}&key={apiKey}";
                using (HttpClient client = new HttpClient())
                {
                    var response = await client.GetStringAsync(url);
                    var books = JsonConvert.DeserializeObject<GoogleBooksResponse>(response);
                    return View(books);
                }
            }
            
        }
    }

}
