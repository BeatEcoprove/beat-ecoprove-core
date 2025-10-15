using BeatEcoprove.Contracts.Common;

using Microsoft.AspNetCore.Http;

namespace BeatEcoprove.Contracts.Profile;

public record CreateNestedProfileRequest
(
    string Name,
    DateOnly BornDate,
    string Gender,
    string UserName,
    IFormFile? Avatar
) : IPictureRequest
{
    public Stream PictureStream => Avatar is null ? Stream.Null : Avatar.OpenReadStream();
}