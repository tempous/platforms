using Platforms.API.Services;

namespace Platforms.Tests.NUnit;

[TestFixture]
public class PlatformServiceTests
{
    private readonly PlatformService _platformService = new();
    private const string FilePath = "ads.txt";

    private const string FileContent =
        "Яндекс.Директ:/ru\n" +
        "Ревдинский рабочий:/ru/svrd/revda,/ru/svrd/pervik\n" +
        "Газета уральских москвичей:/ru/msk,/ru/permobl,/ru/chelobl\n" +
        "Крутая реклама:/ru/svrd";

    [SetUp]
    public async Task Setup()
    {
        await File.WriteAllTextAsync(FilePath, FileContent);
        await _platformService.LoadDataAsync(FilePath);
    }

    [TearDown]
    public void TearDown()
    {
        if (File.Exists(FilePath))
            File.Delete(FilePath);
    }

    [TestCase("/ru/svrd/revda", new[] { "Яндекс.Директ", "Крутая реклама", "Ревдинский рабочий" })]
    [TestCase("/ru/svrd", new[] { "Яндекс.Директ", "Крутая реклама" })]
    [TestCase("/ru/msk", new[] { "Яндекс.Директ", "Газета уральских москвичей" })]
    [TestCase("/ru", new[] { "Яндекс.Директ" })]
    public Task FindPlatformsByLocation_ShouldReturnCorrectPlatforms(string location, string[] expectedPlatforms)
    {
        var actualPlatforms = _platformService.FindByLocation(location);

        Assert.That(actualPlatforms, Is.EquivalentTo(expectedPlatforms));
        Assert.That(actualPlatforms, Has.Count.EqualTo(expectedPlatforms.Length));
        return Task.CompletedTask;
    }
}