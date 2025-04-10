namespace Platforms.API.Services.Interfaces;

public interface IPlatformService
{
    Task<bool> LoadDataAsync(string filePath);
    HashSet<string> FindByLocation(string location);
}