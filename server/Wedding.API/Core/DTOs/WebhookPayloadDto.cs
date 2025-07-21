namespace Wedding.API.Core.DTOs;

public class WebhookPayloadDto
{
    public string? Type { get; set; }
    public WebhookDataDto? Data { get; set; }
}
