using Asp.Versioning;

namespace BeatEcoprove.Api;

public static class ApiVersions
{
    private static readonly ApiVersion V1 = new(1, 0);
    private static readonly ApiVersion V2 = new(2, 0);

    public static readonly ApiVersion Current = V2;
}