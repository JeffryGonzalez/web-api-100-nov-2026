
using Alba;
using Marten;
using Microsoft.Extensions.DependencyInjection;
using Software.Api.Vendors;
using System.Security.Claims;

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


        //var response = await Host.Scenario(api =>
        //{
        //    api.WithClaim(new System.Security.Claims.Claim(ClaimTypes.Role, "SoftwareCenter"));
        //    api.WithClaim(new System.Security.Claims.Claim(ClaimTypes.Role, "Manager"));
        //    api.Post.Json(new VendorCreateModel { CompanyName = "Microsoft",
        //        Url = "https://www.microsoft.com",
        //        PointOfContact = new VendorPointOfContact
        //        {
        //            Name = "Satya N",
        //            Email = "satya@microsoft.com"
        //        }
        //    }).ToUrl("/vendors");

        //    api.StatusCodeShouldBe(201);
        //    //api.JsonPath("$.name").ShouldBe("Microsoft");
        //    //api.JsonPath("$.url").ShouldBe("https://www.microsoft.com");
        //    //api.JsonPath("$.pointOfContact.name").ShouldBe("Satya N");
        //    //api.JsonPath("$.id").ShouldExist();
        //});

        //var thingy = response.ReadAsJson<VendorDetailsModel>();

        //Assert.NotNull(thingy);
        //Assert.Equal("Microsoft", thingy.Name);

        var response = await Host.Scenario(api =>
        {
            api.WithClaim(new System.Security.Claims.Claim(ClaimTypes.Role, "SoftwareCenter"));
            api.WithClaim(new System.Security.Claims.Claim(ClaimTypes.Role, "Manager"));
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

        // This is the "gray box" thing - 
        // we can do things like check the database, we can look at messages on a broker, all that stuff
        // these are things that are almost impossible (or very costly ) in system integration tests. 

        using var scope = Host.Services.CreateScope();
        using var db = scope.ServiceProvider.GetRequiredService<IDocumentSession>();

        var createdDoc = await db.Query<VendorEntity>().FirstOrDefaultAsync(x => x.Id == createdId, TestContext.Current.CancellationToken);
        Assert.NotNull(createdDoc);
        Assert.Equal("Microsoft", createdDoc.Name);
        Assert.Equal(Fixture.Clock.GetUtcNow(), createdDoc.CreatedAt);
        //Fixture.Clock.Advance(TimeSpan.FromDays(365 * 10));
    }
}

public record GuidIdentifiedResponse(Guid Id);