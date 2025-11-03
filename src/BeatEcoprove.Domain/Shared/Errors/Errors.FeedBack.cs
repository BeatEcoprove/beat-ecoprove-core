using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public partial class Errors
{
    public class FeedBack
    {
        public static Error TitleNotDefine => Error.Validation(
            "Core.FeedBack.TitleNotDefine.Title",
            "Core.FeedBack.TitleNotDefine.Description");

        public static Error DescriptionNotProvided => Error.Validation(
            "Core.FeedBack.DescriptionNotProvided.Title",
            "Core.FeedBack.DescriptionNotProvided.Description");

        public static Error TitleMaxExceeded => Error.Validation(
            "Core.FeedBack.TitleMaxExceeded.Title",
            "Core.FeedBack.TitleMaxExceeded.Description");

        public static Error TitleMinExceeded => Error.Validation(
            "Core.FeedBack.TitleMinExceeded.Title",
            "Core.FeedBack.TitleMinExceeded.Description");

    }
}