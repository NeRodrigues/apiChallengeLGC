using System.Text.Json.Serialization;

public class PayloadBody
{
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    [JsonPropertyName("body")]
    public string? Body { get; set; }
    [JsonPropertyName("userId")]
    public int UserId { get; set; }
    [JsonPropertyName("id")]
    public int Id { get; set; }
}
