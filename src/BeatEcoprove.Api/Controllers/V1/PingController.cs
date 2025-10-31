namespace BeatEcoprove.Api.Controllers.V1;

public class PingController : ApiCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var ping = CreateVersionedGroup(app, "health");

        ping.MapGet(string.Empty, () => 
            Results.Ok(new { Message = "The server is online" }));
    }
}