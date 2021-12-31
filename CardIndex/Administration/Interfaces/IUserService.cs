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
        Task<IdentityResult> DeleteUserByEmailAndPasswordAsync(string email, string password);
        Task<UserViev> GetUserByEmailAndPasswordAsync(string email, string password);
        Task<IEnumerable<UserViev>> GetAllUsersAsync();
        Task<IdentityResult> UpdateUserAsync(User userApp);
        Task<string> LogonAsync(LogonModel logon);
        Task AssignUserToRolesAsync(AssignUserToRolesModel assignUserToRoles);
        Task CreateRoleAsync(string roleName);
        Task<IdentityResult> DeleteRoleAsync(string roleName);
        Task<IEnumerable<RoleViev>> GetRolesAsync();
        Task<IEnumerable<User>> GetUsersAsync();
    }
}
