using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using WomensTechForum2._0.Models;
using static System.Net.WebRequestMethods;

namespace WomensTechForum2._0.Pages
{
    public class IndexModel : PageModel
    {
        public List<Quote> Quotes { get; set; }
        public Quote RandomQuote { get; set; }

        public List<NewsAPI.Models.Article> News { get; set; }

        public async Task OnGetAsync()
        {
            Quotes = await DAL.QuoteManager.GetAllQuotes();
            News = await DAL.NewsManager.GetNews();

            Random rnd = new Random();
            var randomIndex = rnd.Next(0, Quotes.Count);
            RandomQuote = Quotes[randomIndex];

        }
    }
}