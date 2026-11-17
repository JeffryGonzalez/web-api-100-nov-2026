using Marten;
using Microsoft.AspNetCore.Mvc;


namespace Software.Api.Vendors;

// Api Controller says automatically validate any incoming request based on the validation rules of that model.

[ApiController]
[Route("vendors")] // /vendors resources
public class VendorsController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<VendorDetailsModel>> CreateVendor([FromBody] VendorCreateModel model, [FromServices] TimeProvider clock, [FromServices] IDocumentSession session)
    {

        var vendorEntity = new VendorEntity
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Url = model.Url,
            PointOfContact = model.PointOfContact,
            CreatedAt = clock.GetUtcNow(),
            CreatedBy = User.Identity?.Name ?? "Unknown"
        };

        session.Store(vendorEntity);
        await session.SaveChangesAsync();

        var newVendor = new VendorDetailsModel
        {
            Id = vendorEntity.Id,
            Name = vendorEntity.Name,
            Url = vendorEntity.Url,
            PointOfContact = vendorEntity.PointOfContact
        };

        return CreatedAtAction(nameof(GetVendorById), new { id = newVendor.Id }, newVendor);
    }
    // GET /vendors/some-big-old-id
    [HttpGet]
    public async Task<ActionResult<VendorSummary>> GetVendors([FromServices] IDocumentSession session, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {

        var vendors = await session.Query<VendorEntity>()
            .OrderBy(v => v.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(v => new VendorSummaryModel
            {
                Id = v.Id,
                Name = v.Name,
                Url = v.Url
            })
            .ToListAsync();

        var summary = new VendorSummary
        {
            Vendors = vendors,
            Page = page,
            PageSize = pageSize
        };
        return Ok(summary);
    }

    // GET /vendors
    [HttpGet]
    public async Task<ActionResult<VendorSummary>> GetVendors([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        // Simulate retrieving a list of vendors with pagination
        var vendors = new List<VendorSummaryModel>();
        for (int i = 0; i < pageSize; i++)
        {
            vendors.Add(new VendorSummaryModel
            {
                Id = Guid.NewGuid(),
                Name = $"Vendor {((page - 1) * pageSize) + i + 1}",
                Url = $"https://example.com/vendor{((page - 1) * pageSize) + i + 1}"
            });
        }
        var summary = new VendorSummary
        {
            Vendors = vendors,
            Page = page,
            PageSize = pageSize
        };
        // In a real application, you would retrieve the vendors from a database here
        return Ok(summary);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<VendorDetailsModel>> GetVendorById(Guid id, [FromServices] IDocumentSession session)
    {

        var response = await session.Query<VendorEntity>()
            .Select(v => new VendorDetailsModel
            {
                Id = v.Id,
                Name = v.Name,
                Url = v.Url,
                PointOfContact = v.PointOfContact
            })
            .SingleOrDefaultAsync(v => v.Id == id);

        return response switch
        {
            null => NotFound(),
            _ => Ok(response)
        };
    }

}
