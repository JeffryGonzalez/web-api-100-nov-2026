# Notes on the Software Center API Design

## What we are storing - our persistence model (Entity)

The thing in the database ("Entity")

-  postgres drive npgsql, marten

Entity

Keep your entities private - never let entities
leave the boundary of the API. There are big reasons for this.


```json
{
    "id": "{id}",
    "name": "Anemone",
    "url": "https://site.com",
    "createdAt": "{iso 8601 string}",
    "pointOfContact" : {
        "name": "Carl",
        "email": "carl@site.com"
    }
}
```

## Models


### GET summary
This is what you get if you go to `GET /vendors`
```csharp
public record VendorSummaryModel
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Url { get; init; }
}

public record VendorSummary
{
    public required IReadOnlyList<VendorSummaryModel> Vendors { get; init; }
    public required int Page { get; init; }
    public required int PageSize { get; init; }
}
```

Response:

```json
{
    "vendors": [
        {
            "id": "{guid}",
            "name": "Microsoft",
            "url": "https://www.microsoft.com",
            "rating" "not supported"
        }
    ],
    "page": 1,
    "pageSize": 20
}
```

Versioning:
- you can add stuff to a response.
- you can't remove things that are "required"
- you can't change the meaning of existing things.