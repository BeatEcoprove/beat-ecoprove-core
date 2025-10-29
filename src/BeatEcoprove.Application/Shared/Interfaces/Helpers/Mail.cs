namespace BeatEcoprove.Application.Shared.Interfaces.Helpers;

public record Mail
(
    string To,
    string Template,
    Dictionary<string, string> Variables);