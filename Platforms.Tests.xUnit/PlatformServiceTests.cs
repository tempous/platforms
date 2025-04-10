using Platforms.API.Services;

namespace Platforms.Tests.xUnit;

public class PlatformServiceTests : IAsyncLifetime
{
    private readonly PlatformService _platformService = new();
    private const string FilePath = "ads.txt";

    private const string FileContent =
        "Яндекс.Директ:/ru\n" +
        "Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik\n" +
        "Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl\n" +
        "Крутая реклама:/ru/svrd";

    public async Task InitializeAsync()
    {
        await File.WriteAllTextAsync(FilePath, FileContent);
        await _platformService.LoadDataAsync(FilePath);
    }

    public Task DisposeAsync()
    {
        File.Delete(FilePath);
        return Task.CompletedTask;
    }

    [Theory]
    [InlineData("/ru/svrd/revda", new[] { "Яндекс.Директ", "Ревдинский рабочий", "Крутая реклама" })]
    [InlineData("/ru/svrd", new[] { "Яндекс.Директ", "Крутая реклама" })]
    [InlineData("/ru/msk", new[] { "Яндекс.Директ", "Газета уральских москвичей" })]
    [InlineData("/ru", new[] { "Яндекс.Директ" })]
    public Task FindPlatformsByLocation_ShouldReturnCorrectPlatforms(string location, string[] expectedPlatforms)
    {
        var actualPlatforms = _platformService.FindByLocation(location);

        Assert.Equal(expectedPlatforms.Length, actualPlatforms.Count);
        foreach (var platform in expectedPlatforms)
            Assert.Contains(platform, actualPlatforms);

        return Task.CompletedTask;
    }
}