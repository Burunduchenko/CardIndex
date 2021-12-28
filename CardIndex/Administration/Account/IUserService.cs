using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administration.Account
{
    public interface IUserService
    {
        Task Register(Register user);
        Task<UserApp> Logon(Logon logon);
        Task AssignUserToRoles(AssignUserToRoles assignUserToRoles);
        Task CreateRole(string roleName);
        Task<IEnumerable<string>> GetRoles(UserApp user);
        Task<IEnumerable<IdentityRole>> GetRoles();
    }
}
