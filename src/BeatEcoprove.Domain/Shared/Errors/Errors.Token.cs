using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public static partial class Errors
{
    public static class Token
    {
        public static Error InvalidToken => Error.Validation(
            code: "Token.InvalidToken",
            description: "Token inválido");

        public static Error InvalidRefreshToken => Error.Validation(
            code: "Token.InvalidRefreshToken",
            description: "Token de atualização inválido");

        public static Error ExpiredToken => Error.Validation(
            code: "Token.ExpiredToken",
            description: "Token expirado");

        public static Error MissingToken => Error.Validation(
            code: "Token.MissingToken",
            description: "Token não encontrado");
    }
}