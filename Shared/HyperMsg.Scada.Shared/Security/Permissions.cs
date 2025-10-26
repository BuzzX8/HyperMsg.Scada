using System.Security.Claims;

namespace HyperMsg.Scada.Shared.Security;

public static class Permissions
{
    public static readonly string ClaimType = typeof(Permissions).FullName ?? "Permissions";

    public static class Devices
    {
        public const string View = "Devices.View";
        public const string Edit = "Devices.Edit";
        public const string Delete = "Devices.Delete";
    }

    public static class DeviceTypes
    {
        public const string View = "DeviceTypes.View";
        public const string Edit = "DeviceTypes.Edit";
        public const string Delete = "DeviceTypes.Delete";
    }

    public static class Metrics
    {
        public const string View = "Metrics.View";
        public const string Edit = "Metrics.Edit";
        public const string Delete = "Metrics.Delete";
    }

    public static class Users
    {
        public const string Create = "Users.Create";
        public const string View = "Users.View";
        public const string Edit = "Users.Edit";
        public const string Delete = "Users.Delete";
        public const string AssignPermissions = "Users.AssignPermissions";
    }

    public static IEnumerable<string> AllUsers
    {
        get
        {
            yield return Users.Create;
            yield return Users.View;
            yield return Users.Edit;
            yield return Users.Delete;
            yield return Users.AssignPermissions;
        }
    }

    public static Claim Claim(string permission) => new(ClaimType, permission);
}
