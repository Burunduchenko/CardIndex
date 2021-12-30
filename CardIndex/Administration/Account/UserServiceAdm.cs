using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Administration.Account
{
    public sealed class UserServiceAdm : IUserService
    {
        private readonly UserManager<UserApp> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserServiceAdm(UserManager<UserApp> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task Register(Register user)
        {
            var result = await _userManager.CreateAsync(new UserApp
            {
                Email = user.Email,
                UserName = user.Login,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber

            }, user.Password);

            if (!result.Succeeded)
            {
                throw new System.Exception(string.Join(';', result.Errors.Select(x => x.Description)));
            }
        }

        public async Task<UserApp> Logon(Logon logon)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == logon.Email);
            if (user is null) throw new System.Exception($"User not found: '{logon.Email}'.");

            return await _userManager.CheckPasswordAsync(user, logon.Password) ? user : null;
        }

        public async Task AssignUserToRoles(AssignUserToRoles assignUserToRoles)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == assignUserToRoles.Email);
            var roles = _roleManager.Roles.ToList().Where(r => assignUserToRoles.Roles.Contains(r.Name, StringComparer.OrdinalIgnoreCase))
                .Select(r => r.NormalizedName).ToList();

            var result = await _userManager.AddToRolesAsync(user, roles); // THROWS

            if (!result.Succeeded)
            {
                throw new System.Exception(string.Join(';', result.Errors.Select(x => x.Description)));
            }
        }

        public async Task CreateRole(string roleName)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (!result.Succeeded)
            {
                throw new System.Exception($"Role could not be created: {roleName}.");
            }
        }

        public async Task<IEnumerable<IdentityRole>> GetRoles()
        {
            return await _roleManager.Roles.ToListAsync();
        }

        public async Task<IEnumerable<string>> GetRoles(UserApp user)
        {
            return (await _userManager.GetRolesAsync(user)).ToList();
        }

        public async Task<IdentityResult> DeleteUserByEmailAndPassword(string email, string password)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == email);
            var validuser = await _userManager.CheckPasswordAsync(user, password);
            if(!validuser)
            {
                throw new Exception();
            }

            var result = await _userManager.DeleteAsync(user);
            return result;

        }

        public async Task<UserApp> GetUserByEmailAndPassword(string email, string password)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == email);
            var validuser = await _userManager.CheckPasswordAsync(user, password);

            if(!validuser)
            {
                throw new Exception();
            }
            return user;
        }

        public async Task<IEnumerable<UserApp>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return users;
        }

        public async Task<IdentityResult> UpdateUser(UserApp userApp)
        {
            var result = await _userManager.UpdateAsync(userApp);

            return result;
        }

        public async Task<IdentityResult> DeleteRole(string roleName)
        {
            var role = _roleManager.Roles.SingleOrDefault(r => r.Name == roleName);
            if(role == null)
            {
                throw new Exception();
            }
            var result = await _roleManager.DeleteAsync(role);

            return result;
        }
    }
}
