using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public partial class Errors
{
    public class ForgotPassword
    {
        public static Error ForgotTokenNotValid => Error.Conflict(
           "Core.ForgotPassword.ForgotTokenNotValid.Title",
           "Core.ForgotPassword.ForgotTokenNotValid.Description");
    }
}