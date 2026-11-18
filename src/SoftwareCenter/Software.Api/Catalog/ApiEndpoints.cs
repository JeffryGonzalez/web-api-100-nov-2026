using JasperFx.CodeGeneration.Frames;
using JasperFx.CommandLine.Descriptions;
using Software.Api.Catalog.Operations;

namespace Software.Api.Catalog;

public static class ApiEndpoints
{

    //public static IEndpointRouteBuilder MapCatalog(this IEndpointRouteBuilder routes)
    //{
    //    routes.MapGet("catalog", () => "Catalog Goes Here");
    //    return routes;
    //}

    extension(IServiceCollection services)
    {
        public IServiceCollection AddCatalog()
        {
            return services;
        }
    }


    extension(IEndpointRouteBuilder routes)
    {
        public IEndpointRouteBuilder MapCatalog()
        {
            var group = routes.MapGroup("/catalog").WithTags("Catalog", "Software");
            group.MapGet("{id:guid}", CatalogQueries.GetByIdAsync);
            group.MapGet("", CatalogQueries.GetAllAsync).WithDescription("Get all the catalog items!");
            group.MapPost("", AddCatalogItem.CreateAsync).WithDescription("Add a new catalog item!");

            return routes;
        }
    }
}
