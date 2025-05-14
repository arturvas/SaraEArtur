namespace SaraEArtur.API.Routes;

public static class GiftsRoute
{
    public static void GiftsRoutes(this WebApplication app)
    {
        app.MapGet("gifts", () => new { message = "Hello World!" });
    }
}
