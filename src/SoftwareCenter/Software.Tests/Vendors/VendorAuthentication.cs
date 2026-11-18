

namespace Software.Tests.Vendors;

public class VendorAuthentication(UnauthenticatedSystemTestFixture fixture) : IClassFixture<UnauthenticatedSystemTestFixture>
{
    [Theory]
    [InlineData("/vendors")]
    [InlineData("/vendors/e327ff7f-1603-4200-9413-a3c112f6cf8b")]
    public async Task CannotGet(string resource)
    {
        await fixture.Host.Scenario(api =>
        {
            api.Get.Url(resource);
            api.StatusCodeShouldBe(401);
        });

    }

    [Fact]
    public async Task CannotPost()
    {
        await fixture.Host.Scenario(api =>
        {
            api.Post.Json(new { }).ToUrl("/vendors");
            api.StatusCodeShouldBe(401);
        });
    }
}
