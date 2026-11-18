using Marten;
using Microsoft.AspNetCore.Http.HttpResults;
using Software.Api.Catalog.Entities;

namespace Software.Api.Catalog.Operations;

public static class CatalogQueries
{

    public static async Task<Results<Ok<CatalogItemModel>, NotFound<string>>> GetByIdAsync(IDocumentSession session, Guid id) {

        var response = await session.Query<CatalogEntity>()
        .Where(c => c.Id == id)
        .Select(c => new CatalogItemModel { Id=c.Id, Name = c.Name})
        .SingleOrDefaultAsync();
        if(response is null) {
            return TypedResults.NotFound("No Catalog Item With That Id");
        } 
        return TypedResults.Ok(response);
    }

    public static async Task<Ok<IReadOnlyList<CatalogItemModel>>> GetAllAsync(IDocumentSession session,CancellationToken token)
    {
        var response = await session.Query<CatalogEntity>()
            .Select(i => new CatalogItemModel { Id = i.Id, Name = i.Name })
            .ToListAsync(token);

        return TypedResults.Ok(response);
    }
}
