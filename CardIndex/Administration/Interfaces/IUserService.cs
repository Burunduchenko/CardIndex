using Administration.HelperModels;
using Administration.VievModels;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Administration.Interfaces
{
    /// <summary>
    /// The interface is designed to describe 
    /// functions of User Service
    /// </summary>
    public interface IUserService
    {
        Task RegisterAsync(RegisterModel user);
        Task<IdentityResult> DeleteUserByIdAsync(string id);
        Task<UserViev> GetUserByEmailAndPasswordAsync(string email, string password);
        Task<IEnumerable<UserViev>> GetAllUsersAsync();
        Task<User> UpdateUserAsync(UpdateUser userApp);
        Task<string> LogonAsync(LogonModel logon);
        Task ProvideUserToRoleAsync(ManipWithUserRole assignUserToRoles);
        Task CreateRoleAsync(string roleName);
        Task<IdentityResult> DeleteRoleAsync(string roleName);
        Task<IEnumerable<RoleViev>> GetRolesAsync();
        Task<IEnumerable<User>> GetUsersAsync();
        Task TakeUserFromRoleAsync(ManipWithUserRole assignUserToRoles);
    }
}
