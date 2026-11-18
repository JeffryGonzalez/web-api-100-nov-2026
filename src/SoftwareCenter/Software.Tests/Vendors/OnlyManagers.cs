using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Software.Tests.Vendors;

public class OnlyManagers : SystemTestContext
{
    public OnlyManagers(AuthenticatedSystemTestFixture fixture) : base(fixture)
    {
    }
    [Fact]
    public async Task ManagersCanAddAVendor()
    {
        await Host.Scenario(api =>
        {
            api.WithClaim(new Claim(ClaimTypes.Role, "SoftwareCenter"));
            api.WithClaim(new Claim(ClaimTypes.Role, "Manager"));
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
        });
    }
    [Fact]
    public async Task NonManagersCannot()
    {
        await Host.Scenario(api =>
        {
       
           // api.WithClaim(new Claim(ClaimTypes.Role, "SoftwareCenter"));
           // api.WithClaim(new Claim(ClaimTypes.Role, "Manager"));
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
            api.StatusCodeShouldBe(403);
        });
    }
}
