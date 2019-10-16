using Plugin.Permissions.Abstractions;

namespace JToolbox.XamarinForms.Permissions
{
    public class PermissionsStatus
    {
        public Permission Permission { get; set; }
        public PermissionStatus Status { get; set; }
    }
}