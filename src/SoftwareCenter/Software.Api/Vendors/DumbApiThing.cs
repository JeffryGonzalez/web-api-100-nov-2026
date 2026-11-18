using Microsoft.AspNetCore.Mvc;

namespace Software.Api.Vendors;

public class DumbApiThing : ControllerBase
{

    private int Count = 0;

    [HttpGet("count")]
    public ActionResult GetCount()
    {
        Count++;
        return Ok(new { count = Count });
    }
}
