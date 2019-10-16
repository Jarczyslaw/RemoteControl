using Plugin.Permissions.Abstractions;
using System.Collections.Generic;
using System.Linq;

namespace JToolbox.XamarinForms.Permissions
{
    public class PermissionsResult
    {
        public List<PermissionsStatus> PermissionsStatuses { get; set; } = new List<PermissionsStatus>();
        public bool ContainsNotGrantedPermission => PermissionsStatuses.Any(p => p.Status != PermissionStatus.Granted);
    }
}