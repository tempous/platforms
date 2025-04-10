using Platforms.API.Services.Interfaces;

namespace Platforms.API.Endpoints;

public static class PlatformEndpoints
{
    public static void MapPlatformEndpoints(this IEndpointRouteBuilder app)
    {
        var api = app.MapGroup("api/platforms").WithOpenApi();

        api.MapGet("load",
            async (IPlatformService platformService, string filePath = "ads.txt") => await platformService.LoadDataAsync(filePath)
                ? Results.Ok("Данные успешно загружены")
                : Results.Problem("Файл не найден или произошла ошибка при загрузке данных"));

        api.MapGet("find", (string location, IPlatformService platformService) =>
        {
            var platforms = platformService.FindByLocation(location);
            return platforms.Count != 0
                ? Results.Ok(platforms)
                : Results.NotFound("Для заданной локации рекламных площадок не найдено!");
        });
    }
}