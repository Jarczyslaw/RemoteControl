using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JToolbox.XamarinForms.Permissions
{
    public class PermissionsService : IPermissionsService
    {
        public Task<PermissionStatus> CheckPermission(Permission permission)
        {
            return CrossPermissions.Current.CheckPermissionStatusAsync(permission);
        }

        public async Task<PermissionStatus> CheckAndRequestPermission(Permission permission)
        {
            var permissionStatus = await CrossPermissions.Current.CheckPermissionStatusAsync(permission);
            if (permissionStatus != PermissionStatus.Granted)
            {
                var results = await CrossPermissions.Current.RequestPermissionsAsync(permission);
                if (results.ContainsKey(permission))
                {
                    permissionStatus = results[permission];
                }
            }
            return permissionStatus;
        }

        public async Task<PermissionsResult> CheckAndRequestPermissions(IEnumerable<Permission> permissions)
        {
            var result = new PermissionsResult();
            foreach (var permission in permissions)
            {
                var status = await CheckAndRequestPermission(permission);
                result.PermissionsStatuses.Add(new PermissionsStatus
                {
                    Permission = permission,
                    Status = status
                });
            }
            return result;
        }
    }
}