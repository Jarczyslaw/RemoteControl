using System.Collections.Generic;
using System.Threading.Tasks;
using Plugin.Permissions.Abstractions;

namespace JToolbox.XamarinForms.Permissions
{
    public interface IPermissionsService
    {
        Task<PermissionStatus> CheckAndRequestPermission(Permission permission);
        Task<PermissionsResult> CheckAndRequestPermissions(IEnumerable<Permission> permissions);
        Task<PermissionStatus> CheckPermission(Permission permission);
    }
}