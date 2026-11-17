using Marten;
using Microsoft.AspNetCore.Mvc;

namespace VideoGames.Api.Games;

public class GamesController(IDocumentSession session) : ControllerBase
{

    //private IDocumentSession session;

    //public GamesController(IDocumentSession session)
    //{
    //    this.session = session;
    //}
    [HttpGet("/games")]
    public async Task<ActionResult> GetAllGamesAsync(CancellationToken token)
    {
        var games = await session.Query<GameEntity>().Select(g => new GameDetailsItem(g.Id, g.Title, g.Platforms)).ToListAsync();

        // TODO: go to the database, get the list of games, send it to the calling client
        return Ok(games);
    }

    [HttpPost("/games")]
    public async Task<ActionResult> AddAGameAsync([FromBody] GameCreateItem request, CancellationToken token)
    {
        var entityToSave = new GameEntity
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Platforms = request.Platforms
        };

        // YMMV - depending on what database approach you are using.
        session.Store(entityToSave);
        await session.SaveChangesAsync();

        var response = new GameDetailsItem(entityToSave.Id, entityToSave.Title, entityToSave.Platforms);
        return Ok(response);
    }
}
