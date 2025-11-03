using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public static partial class Errors
{
    public static class Token
    {
        public static Error InvalidToken => Error.Validation(
            code: "Core.Token.InvalidToken.Title",
            description: "Core.Token.InvalidToken.Description");

        public static Error InvalidRefreshToken => Error.Validation(
            code: "Core.Token.InvalidRefreshToken.Title",
            description: "Core.Token.InvalidRefreshToken.Description");

        public static Error ExpiredToken => Error.Validation(
            code: "Core.Token.ExpiredToken.Title",
            description: "Core.Token.ExpiredToken.Description");

        public static Error MissingToken => Error.Validation(
            code: "Core.Token.MissingToken.Title",
            description: "Core.Token.MissingToken.Description");
    }
}