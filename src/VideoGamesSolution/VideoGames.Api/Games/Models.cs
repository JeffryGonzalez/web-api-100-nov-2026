namespace VideoGames.Api.Games;

public record GameListItem(Guid Id, string Title, string[] Platforms, DateTimeOffset Added);

public record GameCreateItem
{
    public required string Title { get; set; }
    public required string[] Platforms { get; set; }
}

public record GameDetailsItem(Guid Id, string Title, string[] Platforms);