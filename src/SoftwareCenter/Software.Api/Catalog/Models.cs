namespace Software.Api.Catalog;

public record CatalogItemModel
{
    public required Guid Id { get; init; }
    public required string Name { get; init; } = string.Empty;
}
