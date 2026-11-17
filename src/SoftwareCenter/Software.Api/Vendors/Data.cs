namespace Software.Api.Vendors;


// Your database code goes here, depending on your selected approach
public class VendorEntity
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Url { get; init; }
    public required VendorPointOfContact PointOfContact { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
}