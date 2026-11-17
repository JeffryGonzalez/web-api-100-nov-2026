using Alba;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Software.Api.Vendors;
using Testcontainers.PostgreSql;

namespace Software.Tests.Vendors;

[Trait("Type", "SystemTest")]
public class GettingVendors
{
    [Fact]
    public async Task CanGetTheVendorsAndGetA200Ok()
    {
        PostgreSqlContainer _pgContainer = new PostgreSqlBuilder("postgres:18.3").Build();
        await _pgContainer.StartAsync(TestContext.Current.CancellationToken);
        await using var host = await AlbaHost.For<Program>(config=>
        {
            config.ConfigureTestServices(sp =>
            {
                var fakeApi = Substitute.For<IHandleVendorData>();
                fakeApi.GetVendorsAsync(Arg.Any<int>(), Arg.Any<int>())
                .Returns(new List<VendorSummaryModel>()); // for now
                sp.AddScoped<IHandleVendorData>(_ => fakeApi);
            });
        })
            .WithConfiguration("ConnectionStrings:software-db", _pgContainer.GetConnectionString());


        // post a vendor to the api


        // get that vendor, or make sure it is in the list.
        await host.Scenario(api =>
        {
            api.Get.Url("/vendors");
            api.StatusCodeShouldBeOk();

        });
      


    }
}
