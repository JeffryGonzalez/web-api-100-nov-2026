using System;
using System.Collections.Generic;
using System.Text;

namespace Software.Tests.Vendors;

public class PostsAreValidated : SystemTestContext
{
    public PostsAreValidated(AuthenticatedSystemTestFixture fixture) : base(fixture)
    {
    }

    [Fact]
    public async Task InvalidVendorReturns400()
    {
        var willfullyWrongVendor = new { Tacos = "Delicious" };

        await Host.Scenario(api =>
        {
            api.Post.Json(willfullyWrongVendor).ToUrl("/vendors");
            api.StatusCodeShouldBe(400);
        });
    }
}
