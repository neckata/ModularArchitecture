using System.ComponentModel;

namespace Gamification.Shared.Core.Constants
{
    public static class Permissions
    {
        [DisplayName("Roles")]
        [Description("Roles Permissions")]
        public static class Roles
        {
            public const string View = "Permissions.Roles.View";
            public const string Create = "Permissions.Roles.Create";
            public const string Edit = "Permissions.Roles.Edit";
            public const string Delete = "Permissions.Roles.Delete";
        }

        [DisplayName("ExcelUpload")]
        [Description("ExcelUpload Permissions")]
        public static class ExcelUpload
        {
            public const string View = "ExcelUpload.View";
            public const string Create = "ExcelUpload.Create";
            public const string Edit = "ExcelUpload.Edit";
            public const string Delete = "ExcelUpload.Delete";
        }
    }
}