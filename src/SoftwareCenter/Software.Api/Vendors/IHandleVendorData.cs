namespace Software.Api.Vendors;

public interface IHandleVendorData
{
    Task<IReadOnlyList<VendorSummaryModel>?> GetVendorsAsync(int page, int pageSize, CancellationToken ct = default);
}