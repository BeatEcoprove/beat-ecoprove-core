namespace BeatEcoprove.Infrastructure.Configuration;

public partial class Env
{
    public class Jwt
    {
        public static string JwtIssuer => GetString("JWT_ISSUER");
        public static string JwtAudience => GetString("JWT_AUDIENCE");

        public static string SecretKey => GetString("JWT_SECRET_KEY");

    }
}