using Microsoft.AspNetCore.Mvc;


namespace Software.Api.Vendors;

// Api Controller says automatically validate any incoming request based on the validation rules of that model.

[ApiController]
[Route("vendors")] // /vendors resources
public class VendorsController : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult<VendorDetailsModel>> CreateVendor([FromBody] VendorCreateModel model)
    {
        // Simulate creating a vendor and returning the details
        var newVendor = new VendorDetailsModel
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Url = model.Url,
            PointOfContact = model.PointOfContact
        };
        // In a real application, you would save the new vendor to a database here
        return CreatedAtAction(nameof(GetVendorById), new { id = newVendor.Id }, newVendor);
    }
    // GET /vendors/some-big-old-id
    [HttpGet("{vendorId:guid}")]
    public async Task<ActionResult<VendorDetailsModel>> GetVendorById(Guid vendorId)
    {
        // Simulate retrieving a vendor by ID
        var vendor = new VendorDetailsModel
        {
            Id = vendorId,
            Name = "Sample Vendor",
            Url = "https://example.com",
            PointOfContact = new VendorPointOfContact
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Phone = "123-456-7890"
            }
        };
        // In a real application, you would retrieve the vendor from a database here
        return Ok(vendor);
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

}
