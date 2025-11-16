using ErrorOr;

namespace BeatEcoprove.Domain.Shared.Errors;

public partial class Errors
{
    public class Groups
    {
        public static Error NotFound => Error.Conflict(
            "Core.Group.NotFound.Title",
            "Core.Group.NotFound.Description");

        public static Error WSNotFound => Error.Conflict(
            "Core.Group.WSNotFound.Title",
            "Core.Group.WSNotFound.Description");

        public static Error WSIsntConnected => Error.Conflict(
            "Core.Group.WSIsntConnected.Title",
            "Core.Group.WSIsntConnected.Description");

        public static Error CannotAccess => Error.Conflict(
            "Core.Group.CannotAccess.Title",
            "Core.Group.CannotAccess.Description");

        public static Error MemberNotFound => Error.Conflict(
            "Core.Group.MemberNotFound.Title",
            "Core.Group.MemberNotFound.Description");

        public static Error CannotPromoteToSameRole => Error.Conflict(
            "Core.Group.CannotPromoteToSameRole.Title",
            "Core.Group.CannotPromoteToSameRole.Description");

        public static Error DontBelongToGroup => Error.Conflict(
            "Core.Group.DontBelongToGroup.Title",
            "Core.Group.DontBelongToGroup.Description");

        public static Error PermissionNotValid => Error.Conflict(
            "Core.Group.PermissionNotValid.Title",
            "Core.Group.PermissionNotValid.Description");

        public static Error CannotPromoteMember => Error.Conflict(
            "Core.Group.CannotPromoteMember.Title",
            "Core.Group.CannotPromoteMember.Description");

        public static Error CannotPromoteYourself => Error.Conflict(
            "Core.Group.CannotPromoteYourself.Title",
            "Core.Group.CannotPromoteYourself.Description");

        public static Error CannotKickMember => Error.Conflict(
            "Core.Group.CannotKickMember.Title",
            "Core.Group.CannotKickMember.Description");

        public static Error MemberAlreadyExists => Error.Conflict(
            "Core.Group.MemberAlreadyExists.Title",
            "Core.Group.MemberAlreadyExists.Description");

        public static Error InviteNotFound => Error.Conflict(
            "Core.Group.InviteNotFound.Title",
            "Core.Group.InviteNotFound.Description");

        public static Error InviteAlreadyUsed => Error.Conflict(
            "Core.Group.InviteAlreadyAccepted.Title",
            "Core.Group.InviteAlreadyAccepted.Description");
    }
}