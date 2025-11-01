namespace BeatEcoprove.Application.Shared.Communication.LevelUp;

public record LevelUpContent
(
    int Level,
    double Xp
)
{
    public static string Message => "Parabéns! Subiu de Nível!";
};