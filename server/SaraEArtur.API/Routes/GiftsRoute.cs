using Microsoft.EntityFrameworkCore;
using SaraEArtur.API.Data;
using SaraEArtur.API.Models;

namespace SaraEArtur.API.Routes;

public static class GiftsRoute
{
    public static void GiftsRoutes(this WebApplication app)
    {
        var route = app.MapGroup("gifts");

        route.MapPost("",
            async (GiftRequest req, WeddingContext context) =>
        {
            var gift = new GiftModel(req.name);
            await context.AddAsync(gift);
            await context.SaveChangesAsync();
            
            return Results.Created($"/gifts/{gift.Id}", gift);
        });
        
        route.MapGet("", 
            async (WeddingContext context) =>
        {
            var gifts = await context.Gifts.ToListAsync();
            return Results.Ok(gifts);
        });

        route.MapPut("{id:guid}",
            async (Guid id, GiftRequest req, WeddingContext context) =>
        {
            var gift = await context.Gifts.FirstOrDefaultAsync(g => g.Id == id);

            if (gift == null)
                return Results.NotFound();
            
            
            gift.ChangeName(req.name);
            await context.SaveChangesAsync();
            
            return Results.Ok(gift);
        });

        route.MapDelete("{id:guid}",
            async (Guid id, WeddingContext context) =>
        {
            var gift = await context.Gifts.FirstOrDefaultAsync(g => g.Id == id);
            
            if (gift == null)
                return Results.NotFound();
            
            gift.IsAvailable = false;
            gift.SetInactive();
            await context.SaveChangesAsync();
            return Results.Ok(gift);
        });
    }
}
