namespace Platforms.API.Models;

public class Platform
{
    public required string Name { get; init; }
    public List<string> Locations { get; init; } = [];
}