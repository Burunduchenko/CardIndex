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
    /// <summary>
    /// The class is designed to work with User and Role Identities
    /// </summary>
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
            var adduser = new User()
            {
                Email = user.Email,
                UserName = user.Login,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber

            };
            var result = await _userManager.CreateAsync(adduser, user.Password);


            if (!result.Succeeded)
            {
                throw new InvalidArgumentException();
            }

            await AssignUserToRolesAsync(new AssignUserToRolesModel()
            {
                Email = user.Email,
                Roles = new string[] { "user" }
            });

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

        /// <summary>
        /// The method is designed to give 
        /// the user different access rights
        /// </summary>
        /// <param name="assignUserToRoles">Model that includes the user's 
        /// email and specified access rights</param>
        /// <returns></returns>
        /// <exception cref="InvalidArgumentException">An error is thrown when the 
        /// entered data is invalid</exception>
        public async Task AssignUserToRolesAsync(AssignUserToRolesModel assignUserToRoles)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Email == assignUserToRoles.Email);
            var roles = _roleManager.Roles.Where(r => assignUserToRoles.Roles.Contains(r.Name)).Select(r => r.Name.ToUpper()).ToList();

            var result = await _userManager.AddToRolesAsync(user, roles);

            if (!result.Succeeded)
            {
                throw new InvalidArgumentException();
            }
        }

        public async Task CreateRoleAsync(string roleName)
        {
            var result = await _roleManager.CreateAsync(
                new IdentityRole() { Name = roleName, NormalizedName = roleName.ToUpper()});

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

        public async Task<IdentityResult> DeleteUserByIdAsync(string id)
        {
            var user = _userManager.Users.SingleOrDefault(u => u.Id == id);
            
            if (user == null)
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
                Id = user.Id
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
                    Id = item.Id
                };
                var roles = await _userManager.GetRolesAsync(item);
                buff.Roles = roles.ToList();
                result.Add(buff);
            }
            return result;
        }

        public async Task<User> UpdateUserAsync(UpdateUser userApp)
        {
            var dbuser = await _userManager.FindByIdAsync(userApp.Id);
            if (dbuser == null)
            {
                throw new InvalidArgumentException();
            }
            User updateUser = dbuser;
            updateUser.FirstName = userApp.FirstName;
            updateUser.LastName = userApp.LastName;
            updateUser.PhoneNumber = userApp.PhoneNumber;
            updateUser.Email = userApp.Email;
            await _userManager.UpdateAsync(updateUser);
            //await _userManager.SetPhoneNumberAsync(dbuser, userApp.PhoneNumber);
            //await _userManager.SetEmailAsync(dbuser, userApp.PhoneNumber);
            
            return dbuser;
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

        /// <summary>
        /// The method is designed to find all users in Article Rate Controller, method 
        /// AddAsync 
        /// </summary>
        /// <returns>User Identity</returns>
        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }
    }
}
