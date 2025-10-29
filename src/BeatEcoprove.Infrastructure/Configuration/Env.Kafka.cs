namespace BeatEcoprove.Infrastructure.Configuration;

public partial class Env
{
    public static string Host => GetString("KAFKA_HOST");
    public static string Port => GetString("KAFKA_PORT");
}
