using Microsoft.AspNetCore.Mvc;

namespace VideoGames.Api.Games;

public class GamesController : ControllerBase
{
    [HttpGet("/games")]
    public async Task<ActionResult> GetAllGamesAsync(CancellationToken token)
    {
        var fakeResponse = new List<GameListItem>()
        {
            new GameListItem(Guid.NewGuid(), "Destiny 2", ["XBox", "Playstation", "PC"], DateTimeOffset.UtcNow),
            new GameListItem(Guid.NewGuid(), "Stray", ["XBox"], DateTimeOffset.UtcNow.AddMonths(-3))
        };

        // TODO: go to the database, get the list of games, send it to the calling client
        return Ok(fakeResponse);
    }

    [HttpPost("/games")]
    public async Task<ActionResult> AddAGameAsync([FromBody] GameCreateItem request, CancellationToken token)
    {
        
        return Ok(request);
    }
}
