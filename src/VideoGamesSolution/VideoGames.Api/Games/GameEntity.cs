namespace VideoGames.Api.Games;

public class GameEntity
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string[] Platforms { get; set; }
}