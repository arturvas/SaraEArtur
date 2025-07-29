using System.Text.Json.Serialization;

namespace Wedding.API.Core.DTOs;

public class WebhookDataDto
{
    [JsonPropertyName("id")]
    public long Id { get; set; }
}
