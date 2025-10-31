using BeatEcoprove.Api.Extensions;
using BeatEcoprove.Application.FeedBacks.Commands.SubmitFeedBack;
using BeatEcoprove.Application.Shared.Multilanguage;
using BeatEcoprove.Contracts.FeedBacks;

using MapsterMapper;

using MediatR;

namespace BeatEcoprove.Api.Controllers.V1;

public class FeedbackController : ApiCarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var feedback = CreateVersionedGroup(app, "extensions/feedback")
            .RequireAuthorization();

        feedback.MapPost(string.Empty, async (
            ISender sender,
            IMapper mapper,
            ILanguageCulture localizer,
            HttpContext context,
            FeedBackRequest request,
            CancellationToken cancellationToken
        ) =>
        {
            var profileId = context.User.GetProfileId();

            var result = await sender.Send(
                new SubmitFeedBackCommand(
                    profileId,
                    request.Title,
                    request.Description),
                cancellationToken);

            return result.Match(
                response => Results.Ok(
                    mapper.Map<FeedBackResponse>(response)),
                errors => errors.ToProblemDetails(localizer)
            );
        });
    }
}