namespace BeatEcoprove.Application.Shared.Helpers;

public interface IAuthorization
{
    Guid ProfileId { get; }
    Guid AuthId { get; }
}