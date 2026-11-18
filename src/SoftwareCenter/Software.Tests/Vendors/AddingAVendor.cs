
using Alba;
using Marten;
using Microsoft.Extensions.DependencyInjection;
using Software.Api.Vendors;

namespace Software.Tests.Vendors;

public class AddingAVendor : SystemTestContext
{
    AuthenticatedSystemTestFixture Fixture { get; }
    public AddingAVendor(AuthenticatedSystemTestFixture fixture) : base(fixture)
    {
        Fixture = fixture;
    }   


    [Fact]
    public async Task PostingANewVendorHappyPath()
    {
        var response = await Host.Scenario(api =>
        {
            api.Post.Json(new
            {
                name = "Microsoft",
                url = "https://www.microsoft.com",
                pointOfContact = new
                {
                    name = "Satya N",
                    email = "satya@microsoft.com"
                }
            }).ToUrl("/vendors");
            api.StatusCodeShouldBe(201);
            api.JsonPath("$.name").ShouldBe("Microsoft");
            api.JsonPath("$.url").ShouldBe("https://www.microsoft.com");
            api.JsonPath("$.pointOfContact.name").ShouldBe("Satya N");
            api.JsonPath("$.id").ShouldExist();
        });

        var createdId = response.ReadAsJson<GuidIdentifiedResponse>().Id;
       
        var location = response.Context.Response.Headers.Location.First();
        Assert.NotNull(location);
        var path = new Uri(location).LocalPath;

        await Host.Scenario(api =>
        {
            api.Get.Url(path);
            api.StatusCodeShouldBeOk();
            api.JsonPath("$.name").ShouldBe("Microsoft");
            api.JsonPath("$.url").ShouldBe("https://www.microsoft.com");
            api.JsonPath("$.pointOfContact.name").ShouldBe("Satya N");
            api.JsonPath("$.id").ShouldExist();

        });

        using var scope = Host.Services.CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<IDocumentSession>();

        var createdDoc = await db.LoadAsync<VendorEntity>(createdId, TestContext.Current.CancellationToken);
        Assert.NotNull(createdDoc);
        Assert.Equal("Microsoft", createdDoc.Name);
        Assert.Equal(Fixture.Clock.GetUtcNow(), createdDoc.CreatedAt);
    }
}

public record GuidIdentifiedResponse(Guid Id);