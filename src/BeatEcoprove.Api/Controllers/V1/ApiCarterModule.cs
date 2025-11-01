using System.Diagnostics.CodeAnalysis;

using Carter;

namespace BeatEcoprove.Api.Controllers.V1;

public abstract class ApiCarterModule : CarterModule
{
    protected static RouteGroupBuilder CreateVersionedGroup(
        IEndpointRouteBuilder app,
        [StringSyntax("Route")] string path)
    {
        var versionedGroup = app.MapGroup($"v{{version:apiVersion}}")
            .WithApiVersionSet(app.NewApiVersionSet()
                .HasApiVersion(ApiVersions.Current)
                .Build());

        return versionedGroup.MapGroup(path);
    }
}