using BeatEcoprove.Application.Shared.Gaming;

namespace BeatEcoprove.Application.Shared.Interfaces.Services;

public interface IGamingService
{
    double GetLevelProgress(IGamingObject profile);
    void GainXp(IGamingObject profile, double xp);
    double GetNextLevelXp(IGamingObject profile);
}