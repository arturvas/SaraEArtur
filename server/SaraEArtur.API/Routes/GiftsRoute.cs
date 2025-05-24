using Microsoft.EntityFrameworkCore;
using SaraEArtur.API.Data;
using SaraEArtur.API.Models;

namespace SaraEArtur.API.Routes;

public static class GiftsRoute
{
    public static void GiftsRoutes(this WebApplication app)
    {
        app.MapGet("/api/gifts", () => new GiftsModel("Artur"));
        // app.MapGet("/api/gifts", async (WeddingContext db) =>
        //     await db.Gifts.ToListAsync());
    }
}
