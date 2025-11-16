using System.Reflection;

namespace BeatEcoprove.Application.Shared.Helpers;

public class Buckets
{
    public const string DefaultBucket = "default";
    public const string ProfileBucket = "profile";
    public const string GroupBucket = "group";
    public const string ClothBucket = "cloth";
    public const string StoreBucket = "store";
    public const string AdvertisementBucket = "advertisement";

    public static string[] GetAllBuckets()
    {
        return typeof(Buckets)
            .GetFields(BindingFlags.Public | BindingFlags.Static)
            .Where(f => f.FieldType == typeof(string) && f.Name.EndsWith("Bucket"))
            .Select(f => (string)f.GetValue(null)!)
            .ToArray();
    }

    public static string? GetBucketFromDomainName(string domain)
    {
        domain = domain.ToLower();

        foreach (var bucket in GetAllBuckets())
        {
            if (bucket.Equals(domain))
                return bucket;
        }

        return null;
    }
}