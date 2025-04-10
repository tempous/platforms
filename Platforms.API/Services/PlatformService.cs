using Platforms.API.Services.Interfaces;

namespace Platforms.API.Services;

public class PlatformService : IPlatformService
{
    private readonly Dictionary<string, HashSet<string>> _platforms = new();

    public async Task<bool> LoadDataAsync(string filePath)
    {
        _platforms.Clear();
        filePath = filePath.Trim(['"', ' ']);

        try
        {
            var lines = await File.ReadAllLinesAsync(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(':');
                if (parts.Length == 2)
                {
                    var name = parts[0].Trim();
                    var locations = parts[1].Split(',').Select(l => l.Trim()).ToList();
                    foreach (var location in locations)
                    {
                        if (!_platforms.ContainsKey(location))
                            _platforms[location] = [];

                        _platforms[location].Add(name);
                    }
                }
            }

            return true;
        }
        catch (FileNotFoundException)
        {
            return false;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public HashSet<string> FindByLocation(string location)
    {
        location = location.Trim();
        var platformsByLocation = new HashSet<string>();

        int lastIndexOfSlash;
        do
        {
            if (_platforms.TryGetValue(location, out var platforms))
                platformsByLocation.UnionWith(platforms);

            lastIndexOfSlash = location.LastIndexOf('/');
            location = location[..lastIndexOfSlash];
        } while (lastIndexOfSlash != 0);

        return platformsByLocation;
    }
}