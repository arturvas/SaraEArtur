using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Http;
using Wedding.API.Core.Models;

namespace Wedding.Functions;

public class RegisterGiftBuyerFunction(ILogger<RegisterGiftBuyerFunction> logger)
{
    [Function("RegisterGiftBuyerFunction")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "post")]
        HttpRequestData req)
    {
        logger.LogInformation("RegisterGiftBuyerFunction triggered.");
        
        try
        {
            // le o corpo json enviado
            var body = await new StreamReader(req.Body).ReadToEndAsync();

            if (string.IsNullOrWhiteSpace(body))
            {
                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badResponse.WriteStringAsync("Request body cannot be empty.");
                return badResponse;
            }
            
            var giftOrder = JsonSerializer.Deserialize<GiftOrder>(body, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });

            if (giftOrder == null)
            {
                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badResponse.WriteStringAsync("Request body cannot be empty.");
                return badResponse;
            }
            
            logger.LogInformation($"Gift: '{giftOrder.Title}', Amount: '{giftOrder.Amount}, Buyer: {giftOrder.PayerFullName}");
            
            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteStringAsync($"Gift '{giftOrder.Title}' registered successfully for {giftOrder.PayerFullName}");
            return response;
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error occurred.");
            
            var errorResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            await errorResponse.WriteStringAsync($"An error occurred. Error: {e.Message}");
            return errorResponse;
        }
    }
}