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
    // GET /vendors
    [HttpGet]
    public async Task<ActionResult<VendorSummary>> GetVendors([FromServices] IHandleVendorData vendorApi, CancellationToken token, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {

       IReadOnlyList<VendorSummaryModel>? summary = 
            await vendorApi.GetVendorsAsync(page, pageSize);
        var response = new VendorSummary
        {
            Vendors = summary ?? [],
            Page = page,
            PageSize = pageSize
        };
        return Ok(response);
    }

    // GET /vendors
  
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<VendorDetailsModel>> GetVendorById(Guid id, [FromServices] IDocumentSession session, CancellationToken token)
    {

        var response = await session.Query<VendorEntity>()
            .Select(v => new VendorDetailsModel
            {
                Id = v.Id,
                Name = v.Name,
                Url = v.Url,
                PointOfContact = v.PointOfContact
            })
            .SingleOrDefaultAsync(v => v.Id == id, token);

        return response switch
        {
            null => NotFound(),
            _ => Ok(response)
        };
    }

}
