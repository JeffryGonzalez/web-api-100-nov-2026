namespace Software.Api.Catalog.Entities;

public class CatalogEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public Guid VendorId { get; set; }
}
