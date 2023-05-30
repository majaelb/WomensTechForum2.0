using Azure;
using NewsAPI;
using NewsAPI.Constants;
using System.Text.Json;
using WomensTechForum2._0.Models;

namespace WomensTechForum2._0.DAL
{
    public class NewsManager
    {
        public static async Task<List<NewsAPI.Models.Article>> GetNews()
        {
            List<NewsAPI.Models.Article> news = new();
            var newsAPIClient = new NewsApiClient("bbe6005d1e2d430290087505dabf9b4d");
            var articleResponse = newsAPIClient.GetEverything(new NewsAPI.Models.EverythingRequest
            {
                Q = "programming",
                Language = Languages.SV,
                PageSize = 6
            });
            if (articleResponse.Status == Statuses.Ok)
            {
                news = articleResponse.Articles.ToList();
                //string responseString = articleResponse.Articles.ToString();
                //news = JsonSerializer.Deserialize<List<News>>(responseString);
            }
            return news;
        }

        //private static Uri BaseAddress = new("https://localhost:44392/");

        //public static async Task<List<Message>> GetAllMessages()
        //{
        //    List<Message> messages = new();

        //    using (var client = new HttpClient())
        //    {
        //        client.BaseAddress = BaseAddress;
        //        HttpResponseMessage response = await client.GetAsync("api/Messages");
        //        //Get i apiet körs

        //        if (response.IsSuccessStatusCode)
        //        {
        //            string responseString = await response.Content.ReadAsStringAsync();
        //            messages = JsonSerializer.Deserialize<List<Message>>(responseString);
        //        }
        //    }

        //    return messages;
        //}


    }
}
