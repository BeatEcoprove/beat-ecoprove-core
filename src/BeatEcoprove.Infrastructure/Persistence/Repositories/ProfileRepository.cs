using BeatEcoprove.Application.Shared.Interfaces.Persistence.Repositories;
using BeatEcoprove.Domain.ClosetAggregator;
using BeatEcoprove.Domain.ClosetAggregator.DAOs;
using BeatEcoprove.Domain.ClosetAggregator.Enumerators;
using BeatEcoprove.Domain.ClosetAggregator.ValueObjects;
using BeatEcoprove.Domain.ProfileAggregator.DAOS;
using BeatEcoprove.Domain.ProfileAggregator.Entities.Profiles;
using BeatEcoprove.Domain.ProfileAggregator.Enumerators;
using BeatEcoprove.Domain.ProfileAggregator.ValueObjects;
using BeatEcoprove.Domain.Shared.Entities;
using BeatEcoprove.Domain.StoreAggregator;
using BeatEcoprove.Infrastructure.Persistence.Shared;

using Microsoft.EntityFrameworkCore;

namespace BeatEcoprove.Infrastructure.Persistence.Repositories;

public class ProfileRepository : Repository<Profile, ProfileId>, IProfileRepository
{
    public ProfileRepository(IApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<bool> DisableSubProfiles(ProfileId profileId, CancellationToken cancellationToken = default)
    {
        var profile = await DbContext.Profiles.FindAsync(new object[] { profileId }, cancellationToken: cancellationToken);

        if (profile is null)
        {
            return false;
        }

        var getAllSubProfiles = from p in DbContext.Set<Profile>()
                                where p.AuthId == profile.AuthId
                                select p;

        var subProfiles = await getAllSubProfiles.ToListAsync(cancellationToken);

        if (subProfiles.Count == 0)
        {
            return false;
        }

        foreach (var subProfile in subProfiles)
        {
            DbContext.Profiles.Remove(subProfile);
        }

        return true;
    }

    public async Task<ProviderDao?> GetOrganizationAsync(ProfileId organizationId, CancellationToken cancellationToken = default)
    {
        var getOrganization = from profile in DbContext.Set<Profile>()
                              let organization = profile as Organization
                              where
                                  organization != null && profile.Type.Equals(UserType.Organization)
                              let totalRating = (
                                  from store in DbContext.Set<Store>()
                                  where
                                      store.Owner == organization.Id
                                  group store by store.Owner into storeGroup
                                  select new
                                  {
                                      TotalRating = storeGroup.Sum(s => s.Ratings.Sum(r => r.Rate)),
                                      TotalItems = storeGroup.Count()
                                  }
                              ).FirstOrDefault()
                              select new ProviderDao(
                                  organization.Id,
                                  organization.DisplayName,
                                  organization.AvatarUrl,
                                  organization.TypeOption,
                                  new(),
                                  totalRating != null ? totalRating.TotalItems / totalRating.TotalItems : 0
                              );

        return await getOrganization.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<Organization>> GetAllOrganizationsAsync(
        string? search = null,
        int page = 1,
        int pageSize = 10,
        CancellationToken cancellationToken = default)
    {
        var getAllOrganizations = from profile in DbContext.Set<Profile>()
                                  let organization = profile as Organization
                                  where
                                      profile.Type.Equals(UserType.Organization) && organization != null &&
                                      (search == null || ((string)profile.DisplayName).ToLower().Contains(search.ToLower()))
                                  select organization;

        getAllOrganizations = getAllOrganizations
           .Skip((page - 1) * pageSize)
           .Take(pageSize);

        return await getAllOrganizations.ToListAsync(cancellationToken);
    }

    public async Task<List<Profile>> GetAllProfilesAsync(string? search, int pageSize = 10, int page = 1,
        CancellationToken cancellationToken = default)
    {
        var getAllProfiles =
            from profile in DbContext.Profiles
            where
                (search == null || ((string)profile.DisplayName).ToLower().Contains(search.ToLower()))
            select profile;

        getAllProfiles = getAllProfiles
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        return await getAllProfiles
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsUserByUserNameAsync(DisplayName displayName, CancellationToken cancellationToken)
    {
        return await DbContext
            .Profiles
            .AnyAsync(p => p.DisplayName == displayName, cancellationToken);
    }

    public async Task<Profile?> GetProfileByAuthId(AuthId id, CancellationToken cancellationToken)
    {
        return await
            DbContext
            .Profiles
            .SingleOrDefaultAsync(p => p.AuthId == id, cancellationToken);
    }

    public async Task<List<ClothDao>> GetClosetCloth(
        Guid mainProfileId,
        List<ProfileId> queryProfiles,
        string? search,
        List<ClothType>? category = null,
        List<ClothSize>? size = null,
        List<Guid>? colorValue = null,
        List<Guid>? brandValue = null,
        string? order = null,
        string? sortBy = null,
        int pageSize = 10,
        int page = 1,
        CancellationToken cancellationToken = default)
    {
        var getAllCloth =
            from profile in DbContext.Profiles
            from clothEntry in profile.ClothEntries
            from color in DbContext.Set<Color>()
            from brand in DbContext.Set<Brand>()
            join cloth in DbContext.Cloths on clothEntry.ClothId equals cloth.Id
            where
                cloth.Color == color.Id &&
                cloth.Brand == brand.Id &&
                queryProfiles.Contains(profile.Id) &&
                (brandValue == null || brandValue.Contains(brand.Id)) &&
                (colorValue == null || colorValue.Contains(color.Id)) &&
                (size == null || size.Contains(cloth.Size)) &&
                (category == null || category.Contains(cloth.Type)) &&
                (search == null || cloth.Name.ToLower().Contains(search) || brand.Name.ToLower().Contains(search))
            select mainProfileId != clothEntry.ProfileId ?
                new ClothDaoWithProfile(
                cloth.Id,
                cloth.Name,
                cloth.Type.ToString(),
                cloth.Size.ToString(),
                brand.Name,
                color.Hex,
                cloth.EcoScore,
                cloth.State.ToString(),
                cloth.ClothAvatar,
                profile
            ) : new ClothDao(
                    cloth.Id,
                    cloth.Name,
                    cloth.Type.ToString(),
                    cloth.Size.ToString(),
                    brand.Name,
                    color.Hex,
                    cloth.EcoScore,
                    cloth.State.ToString(),
                    cloth.ClothAvatar);

        getAllCloth = getAllCloth
            .Skip((page - 1) * pageSize)
            .Take(pageSize);

        return await getAllCloth
            .ToListAsync(cancellationToken);
    }

    public Task<Profile?> GetByUserNameAsync(DisplayName username, CancellationToken cancellationToken)
    {
        return DbContext
            .Profiles
            .SingleOrDefaultAsync(p => p.DisplayName == username, cancellationToken);
    }

    public async Task UpdateWorkerProfileSustainablePoints(
        List<ProfileId> workerProfileIds,
        int valueSustainablePoints,
        CancellationToken cancellationToken = default)
    {
        var getWorkers = from profile in DbContext.Set<Profile>()
                         where profile.Type.Equals(UserType.Employee) && workerProfileIds.Contains(profile.Id)
                         select profile;

        var workers = await getWorkers.ToListAsync(cancellationToken);
        workers.ForEach(worker =>
        {
            worker.SustainabilityPoints = valueSustainablePoints;
        });
    }

    public async Task<List<ProfileId>> GetNestedProfileIds(ProfileId profileId, CancellationToken cancellationToken)
    {
        var profile = await DbContext.Profiles.FindAsync(new object[] { profileId }, cancellationToken: cancellationToken);

        if (profile is null)
        {
            return new List<ProfileId>();
        }

        var getNestedProfileIds =
            from p in DbContext.Profiles
            where p.AuthId == profile.AuthId
            select p.Id;

        return await getNestedProfileIds.ToListAsync(cancellationToken);
    }

    public async Task<List<Bucket>> GetBucketCloth(
        ProfileId profileId,
        List<Guid> clothIds,
        CancellationToken cancellationToken = default)
    {
        var getAllBuckets =
            from profile in DbContext.Profiles
            from bucketEntry in profile.BucketEntries
            from bucket in DbContext.Buckets
            from clothEntry in bucket.BucketClothEntries
            where
                profile.Id == profileId &&
                bucketEntry.BucketId == bucket.Id &&
                clothEntry.BucketId == bucket.Id &&
                clothIds.Contains(clothEntry.ClothId) &&
                clothEntry.DeletedAt == null
            select bucket;

        getAllBuckets = getAllBuckets.Distinct();
        return await getAllBuckets.ToListAsync(cancellationToken);
    }

    public async Task<bool> CanProfileAccessBucket(ProfileId profileId, BucketId bucketId, CancellationToken cancellationToken = default)
    {
        var profile = await DbContext.Profiles.FindAsync(new object[] { profileId }, cancellationToken: cancellationToken);

        if (profile == null)
        {
            return false;
        }

        var canAccessBucket =
            from p in DbContext.Profiles
            from bucketEntry in p.BucketEntries
            where
                p.AuthId == profile.AuthId &&
                bucketEntry.BucketId == bucketId
            select bucketEntry;

        return await canAccessBucket.AnyAsync(cancellationToken);
    }

    public async Task<bool> CanProfileAccessCloth(ProfileId profileId, ClothId clothId, CancellationToken cancellationToken = default)
    {
        var profile = await DbContext.Profiles.FindAsync(new object[] { profileId }, cancellationToken: cancellationToken);

        if (profile == null)
        {
            return false;
        }

        var canAccessCloth =
            from p in DbContext.Profiles
            from clothEntry in p.ClothEntries
            where
                p.AuthId == profile.AuthId &&
                clothEntry.ClothId == clothId
            select clothEntry;

        return await canAccessCloth.AnyAsync(cancellationToken);
    }

    public Task<List<ProfileDao>> GetAllProfilesOfAuthIdAsync(AuthId authId, CancellationToken cancellationToken)
    {
        // TODO: How to determine main profile?
        var profiles =
            from profile in DbContext.Profiles
            where profile.AuthId == authId
            select new ProfileDao(
                profile.Id.Value,
                profile.AuthId.Value,
                (string)profile.DisplayName,
                profile.AvatarUrl,
                profile.Type.ToString()!,
                false
            );

        return profiles.ToListAsync(cancellationToken);
    }

    public Task DeleteAsync(ProfileId id, CancellationToken cancellationToken)
    {
        var searchProfile =
            from profile in DbContext.Profiles
            where profile.Id == id
            select profile;


        return searchProfile.ExecuteDeleteAsync(cancellationToken);
    }
}