using Administration.Exceptions;
using Administration.HelperModels;
using Administration.Interfaces;
using Administration.Jwt;
using Administration.VievModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Administration.Services
{
    public sealed class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JwtSettings _jwtSettings;

        public UserService(UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager, IOptionsSnapshot<JwtSettings> jwtSettings)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _jwtSettings = jwtSettings.Value;
        }


        private async Task<IEnumerable<string>> GetRolesAsync(User user)
        {
            return (await _userManager.GetRolesAsync(user)).ToList();
        }

        public async Task RegisterAsync(RegisterModel user)
        {
            var result = await _userManager.CreateAsync(new User
            {
                Email = user.Email,
                UserName = user.Login,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber

            }, user.Password);

            if (!result.Succeeded)
            {
                throw new InvalidArgumentException();
            }
        }

        public async Task<string> LogonAsync(LogonModel logon)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == logon.Email);
            if (user is null)
            {
                throw new NotFoundException();
            }

            var roles = await GetRolesAsync(user);

            var JwtToken = JwtHelper.GenerateJwt(user, roles, _jwtSettings);

            return JwtToken;
        }

        public async Task AssignUserToRolesAsync(AssignUserToRolesModel assignUserToRoles)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.UserName == assignUserToRoles.Email);
            var roles = _roleManager.Roles.ToList().Where(r => assignUserToRoles.Roles.Contains(r.Name, StringComparer.OrdinalIgnoreCase))
                .Select(r => r.NormalizedName).ToList();

            var result = await _userManager.AddToRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                throw new InvalidArgumentException();
            }
        }

        public async Task CreateRoleAsync(string roleName)
        {
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));

            if (!result.Succeeded)
            {
                throw new InvalidArgumentException();
            }
        }

        public async Task<IEnumerable<RoleViev>> GetRolesAsync()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            List<RoleViev> result = roles.Select(r => new RoleViev() { Name = r.Name }).ToList();
            return result;
        }



        public async Task<IdentityResult> DeleteUserByEmailAndPasswordAsync(string email, string password)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == email);
            var validuser = await _userManager.CheckPasswordAsync(user, password);
            if (!validuser)
            {
                throw new NotFoundException();
            }

            var result = await _userManager.DeleteAsync(user);
            return result;

        }

        public async Task<UserViev> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == email);
            var validuser = await _userManager.CheckPasswordAsync(user, password);

            if (!validuser)
            {
                throw new NotFoundException();
            }

            UserViev userViev = new UserViev()
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                Login = user.UserName,
                PasswordHash = user.PasswordHash
            };

            var roles = await _userManager.GetRolesAsync(user);
            userViev.Roles = roles.ToList();
            return userViev;
        }

        public async Task<IEnumerable<UserViev>> GetAllUsersAsync()
        {
            var users = await _userManager.Users.ToListAsync();
            List<UserViev> result = new List<UserViev>();
            foreach (var item in users)
            {
                UserViev buff = new UserViev()
                {
                    Email = item.Email,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    PhoneNumber = item.PhoneNumber,
                    Login = item.UserName,
                    PasswordHash = item.PasswordHash
                };
                var roles = await _userManager.GetRolesAsync(item);
                buff.Roles = roles.ToList();
                result.Add(buff);
            }
            return result;
        }

        public async Task<IdentityResult> UpdateUserAsync(User userApp)
        {
            var result = await _userManager.UpdateAsync(userApp);

            if (!result.Succeeded)
            {
                throw new InvalidArgumentException();
            }

            return result;
        }

        public async Task<IdentityResult> DeleteRoleAsync(string roleName)
        {
            var role = _roleManager.Roles.SingleOrDefault(r => r.Name == roleName);
            if (role == null)
            {
                throw new NotFoundException();
            }
            var result = await _roleManager.DeleteAsync(role);

            return result;
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }
    }
}
