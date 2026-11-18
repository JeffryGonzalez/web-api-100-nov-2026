using Alba;


namespace Software.Tests.Vendors;

[Trait("Type", "SystemTest")]
public class GettingVendorsSystemTest : SystemTestContext
{
    public GettingVendorsSystemTest(AuthenticatedSystemTestFixture fixture): base(fixture) { }

    [Fact]
    public async Task CanGetTheVendorsAndGetA200Ok()
    {
      
        await Host.Scenario(api =>
        {
            api.Get.Url("/vendors");
            api.StatusCodeShouldBeOk();

        });
      

    }
}
