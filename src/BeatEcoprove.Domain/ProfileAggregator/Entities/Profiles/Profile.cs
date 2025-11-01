using BeatEcoprove.Application.Shared.Gaming;
using BeatEcoprove.Domain.ClosetAggregator;
using BeatEcoprove.Domain.ClosetAggregator.ValueObjects;
using BeatEcoprove.Domain.Events;
using BeatEcoprove.Domain.ProfileAggregator.DAOS;
using BeatEcoprove.Domain.ProfileAggregator.Entities.Cloths;
using BeatEcoprove.Domain.ProfileAggregator.Enumerators;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;
using BeatEcoprove.Domain.Shared.Errors;
using BeatEcoprove.Domain.Shared.Models;

using ErrorOr;

namespace BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;

public abstract class Profile : AggregateRoot<ProfileId, Guid>, IGamingObject
{
    private const int ConvertConcurrencyRange = 10;
    private readonly List<ClothEntry> _clothEntries = [];
    private readonly List<BucketEntry> _bucketEntries = [];

    protected Profile()
    {
    }

    protected Profile(
        DisplayName displayName,
        string firstName,
        string lastName,
        string biography,
        Phone phone,
        double xP,
        int sustainabilityPoints,
        int ecoScore,
        UserType type)
    {
        Id = ProfileId.CreateUnique();
        DisplayName = displayName;
        FirstName = firstName;
        LastName = lastName;
        Biography = biography;
        Phone = phone;
        XP = xP;
        SustainabilityPoints = sustainabilityPoints;
        EcoScore = ecoScore;
        Type = type;
        Level = 0;
        EcoCoins = 0;
    }

    public DisplayName DisplayName { get; protected set; } = null!;
    public string FirstName { get; protected set; } = null!;
    public string LastName { get; protected set; } = null!;
    public string Biography { get; protected set; } = null!;
    public Phone Phone { get; protected set; } = null!;
    public double XP { get; set; }
    public int Level { get; set; }
    public int EcoCoins { get; protected set; }
    public int SustainabilityPoints { get; set; }
    public int EcoScore { get; set; }
    public string AvatarUrl { get; protected set; } = null!;
    public AuthId AuthId { get; protected set; } = null!;
    public UserType Type { get; protected set; } = null!;
    public IReadOnlyList<ClothEntry> ClothEntries => _clothEntries.AsReadOnly();
    public IReadOnlyList<BucketEntry> BucketEntries => _bucketEntries.AsReadOnly();

    public void SetProfileAvatar(string avatarUrl)
    {
        AvatarUrl = avatarUrl;
    }

    public void SetAuthId(AuthId authId)
    {
        AuthId = authId;
    }

    public void SetProfileId(ProfileId profileId)
    {
        Id = profileId;
    }

    public ProfileDao ToDao()
    {
        return new ProfileDao(
            Id.Value,
            AuthId.Value,
            DisplayName.Value,
            AvatarUrl,
            Type.ToString()!,
            this is Consumer
        );
    }

    public void AddCloth(Cloth cloth)
    {
        _clothEntries.Add(
            new ClothEntry(this.Id, cloth.Id));

        this.AddDomainEvent(new CreateClothDomainEvent(this, cloth));
    }

    public void AddBucket(Bucket bucket)
    {
        _bucketEntries.Add(
            new BucketEntry(this.Id, bucket.Id));
    }

    public ErrorOr<bool> RemoveCloth(ClothId clothId)
    {
        var clothEntry = _clothEntries
            .SingleOrDefault(clothEntry => clothEntry.ClothId == clothId);

        if (clothEntry is null)
        {
            return Errors.Profile.CannotFindCloth;
        }

        clothEntry.DeletedAt = DateTimeOffset.UtcNow;
        return _clothEntries.Remove(clothEntry);
    }

    public ErrorOr<bool> RemoveBucket(BucketId bucketId)
    {
        var bucketEntry = _bucketEntries
            .SingleOrDefault(bucketEntry => bucketEntry.BucketId == bucketId);

        if (bucketEntry is null)
        {
            return Errors.Profile.CannotFindCloth;
        }

        return _bucketEntries.Remove(bucketEntry);
    }

    public ErrorOr<bool> ConvertToEcoCoins(int sustainabilityPoints)
    {
        if (sustainabilityPoints < 0)
        {
            return Errors.Profile.CannotConvertNegativeEcoCoins;
        }

        if (SustainabilityPoints < sustainabilityPoints)
        {
            return Errors.Profile.NotEnoughEcoCoins;
        }

        var oldEcoCoins = EcoCoins;
        EcoCoins += sustainabilityPoints * ConvertConcurrencyRange;

        var delta = EcoCoins - oldEcoCoins;
        if (delta > 0)
        {
            SustainabilityPoints -= sustainabilityPoints;
        }

        return true;
    }

    public ErrorOr<bool> ConvertToSustainabilityPoints(int ecoCoins)
    {
        if (ecoCoins < 0)
        {
            return Errors.Profile.CannotConvertNegativeEcoCoins;
        }

        if (EcoCoins < ecoCoins)
        {
            return Errors.Profile.NotEnoughEcoCoins;
        }

        var oldSustainabilityPoints = SustainabilityPoints;
        SustainabilityPoints += ecoCoins / ConvertConcurrencyRange;

        var delta = SustainabilityPoints - oldSustainabilityPoints;
        if (delta > 0)
        {
            EcoCoins -= ecoCoins;
        }

        return true;
    }

    public void Update
    (
        DisplayName? userName,
        Phone? phone
    )
    {
        if (userName is not null)
        {
            DisplayName = userName;
        }

        if (phone is not null)
        {
            Phone = phone;
        }
    }
}