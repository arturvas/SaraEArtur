using System.Text.Json.Serialization;

namespace Wedding.API.Core.DTOs;

public class WebhookPayloadDto
{
    [JsonPropertyName("type")]
    public string? Type { get; set; }
    [JsonPropertyName("data")]   
    public WebhookDataDto? Data { get; set; }
}
