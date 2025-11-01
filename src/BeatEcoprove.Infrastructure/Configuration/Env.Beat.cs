namespace BeatEcoprove.Infrastructure.Configuration;

public partial class Env
{
    public class Beat
    {
        public static int Port => GetInteger("BEAT_API_REST_PORT");
    }
}