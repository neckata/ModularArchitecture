using System.ComponentModel;

namespace ModularArchitecture.Shared.Core.Constants
{
    public static class Permissions
    {
        [DisplayName("Connectors")]
        [Description("Connectors Permissions")]
        public static class Connectors
        {
            public const string View = "Permissions.Connectors.View";
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

        [DisplayName("Slack")]
        [Description("Slack Permissions")]
        public static class Slack
        {
            public const string View = "Permissions.Slack.View";
            public const string Create = "Permissions.Slack.Create";
            public const string Edit = "Permissions.Slack.Edit";
            public const string Delete = "Permissions.Slack.Delete";
        }

        [DisplayName("Outlook")]
        [Description("Outlook Permissions")]
        public static class Outlook
        {
            public const string View = "Permissions.Outlook.View";
            public const string Create = "Permissions.Outlook.Create";
            public const string Edit = "Permissions.Outlook.Edit";
            public const string Delete = "Permissions.Outlook.Delete";
        }
    }
}