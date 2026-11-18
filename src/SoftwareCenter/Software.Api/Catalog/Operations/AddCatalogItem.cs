using Marten;
using Microsoft.AspNetCore.Http.HttpResults;
using Software.Api.Catalog.Entities;
using System.ComponentModel.DataAnnotations;

namespace Software.Api.Catalog.Operations;


public record CatalogCreateItem
{
    [Required]
    public required string Name { get; init; } = string.Empty;
}
public static class AddCatalogItem
{

    public static async Task<Created<CatalogItemModel>> CreateAsync(IDocumentSession session, CatalogCreateItem request, CancellationToken token)
    {
        var entity = new CatalogEntity
        {
            Id = Guid.NewGuid(),
            Name = request.Name
        };
         session.Store(entity);
        await session.SaveChangesAsync(token);
        var response = new CatalogItemModel
        {
            Id = entity.Id,
            Name = entity.Name
        };
        return TypedResults.Created($"/catalog/{entity.Id}", response);
    }

}
