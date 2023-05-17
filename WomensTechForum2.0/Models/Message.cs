using System.Text.Json.Serialization;

namespace WomensTechForum2._0.Models
{
    public class Message
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("textMessage")]
        public string? TextMessage { get; set; }

        [JsonPropertyName("dateTime")]
        public DateTime? DateTime { get; set; }

        [JsonPropertyName("senderId")]
        public string? SenderId { get; set; }

        [JsonPropertyName("receiverId")]
        public string? ReceiverId { get; set; }

        [JsonPropertyName("isRead")]
        public bool IsRead { get; set; }
    }
}
