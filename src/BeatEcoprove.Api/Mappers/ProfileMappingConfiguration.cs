using BeatEcoprove.Application.Shared.Gaming;
using BeatEcoprove.Contracts.Profile;
using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;

using Mapster;

namespace BeatEcoprove.Api.Mappers;

public class ProfileMappingConfiguration : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<GamificationDTO, GamificationProfileResponse>()
            .MapWith((src) =>
                new GamificationProfileResponse(
                    src.profile.Id.Value,
                    src.profile.DisplayName.Value,
                    src.profile.SustainabilityPoints,
                    src.profile.EcoScore,
                    src.profile.EcoCoins,
                    src.profile.AvatarUrl,
                    src.profile.Phone.Value,
                    src.profile.Phone.Code,
                    src.profile.Level,
                    src.profile.XP,
                    0,
                    src.NextLevelUp
                )
            );


        config.NewConfig<Profile, ProfileResponse>()
            .MapWith((src) =>
                new ProfileResponse(
                    src.Id.Value,
                    src.DisplayName.Value,
                    src.Level,
                    src.XP,
                    0,
                    src.SustainabilityPoints,
                    src.EcoScore,
                    src.EcoCoins,
                    src.AvatarUrl,
                    src.Phone.Value,
                    src.Phone.Code
                )
            );

        config.NewConfig<Profile, ProfileClosetResponse>()
            .MapWith((src) =>
                new ProfileClosetResponse(
                    src.Id.Value,
                    src.DisplayName.Value,
                    src.AvatarUrl
                )
            );

        // config.NewConfig<List<ProfileDao>, MyProfilesResponse>()
        //     .MapWith(src => ToMyProfilesResponse(src));


    }

    /*private MyProfilesResponse ToMyProfilesResponse(List<ProfileDao> profiles)
    {
        var mainProfile =
            profiles.SingleOrDefault(profile => profile.IsNested);

        if (mainProfile is null)
        {
            throw new Exception("Main profile not found");
        }

        var nestedProfiles =
            profiles
                .Where(profile => !profile.IsNested)
                // .Select(profile => profile.Profile)
                .ToList();

        return new MyProfilesResponse(
            mainProfile.Profile.Adapt<ProfileResponse>(),
            nestedProfiles.Adapt<List<ProfileResponse>>()
        );
    }*/
}