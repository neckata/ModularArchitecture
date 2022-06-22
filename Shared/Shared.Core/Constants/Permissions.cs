using System.ComponentModel;

namespace ModularArchitecture.Shared.Core.Constants
{
    public static class Permissions
    {
        [DisplayName("Modules")]
        [Description("Modules Permissions")]
        public static class Modules
        {
            public const string View = "Permissions.Modules.View";
        }

        [DisplayName("Roles")]
        [Description("Roles Permissions")]
        public static class Roles
        {
            public const string View = "Permissions.Roles.View";
            public const string Create = "Permissions.Roles.Create";
            public const string Edit = "Permissions.Roles.Edit";
            public const string Delete = "Permissions.Roles.Delete";
        }

        [DisplayName("Actions")]
        [Description("Actions Permissions")]
        public static class Actions
        {
            public const string View = "Permissions.Actions.View";
            public const string Create = "Permissions.Actions.Create";
            public const string Edit = "Permissions.Actions.Edit";
            public const string Delete = "Permissions.Actions.Delete";
        }
    }
}