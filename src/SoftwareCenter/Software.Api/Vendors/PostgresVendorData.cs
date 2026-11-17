using Marten;
using static System.Collections.Specialized.BitVector32;

namespace Software.Api.Vendors;

public class PostgresVendorData(IDocumentSession session) : IHandleVendorData
{
    public async Task<IReadOnlyList<VendorSummaryModel>?> GetVendorsAsync(int page,
        int pageSize, CancellationToken ct)
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
     .ToListAsync(ct);

        return vendors;
    }
}
