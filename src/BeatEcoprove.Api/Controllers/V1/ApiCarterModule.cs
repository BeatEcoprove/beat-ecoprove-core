using Carter;

namespace BeatEcoprove.Api.Controllers.V1;

public abstract class ApiCarterModule: CarterModule
{
    protected RouteGroupBuilder CreateVersionedGroup(
        IEndpointRouteBuilder app, 
        string path)
    {
        return app.MapGroup($"/v{{version:apiVersion}}/{path.TrimStart('/')}")
            .WithApiVersionSet(app.NewApiVersionSet()
                .HasApiVersion(ApiVersions.Current)
                .Build());
    }
}