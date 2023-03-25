using System.Text.Json.Serialization;

namespace StanzaBonanza.Dtos.PoemDto
{
    public class PoemDto
    {
        [JsonPropertyName("authorId")]
        public int AuthorId { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("body")]
        public string Body { get; set; }
    }
}
