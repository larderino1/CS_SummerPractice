using Frontend.Services.UserService;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Services.UserService
{
    public class UserService : IUserService
    {

        private readonly UserManager<IdentityUser> _userManager;

        public UserService(UserManager<IdentityUser> manager)
        {
            _userManager = manager;
        }

        public async Task DeleteUser(string userName)
        {
            var user = await _userManager.FindByNameAsync(userName);
            await _userManager.DeleteAsync(user);
        }

        public async Task<List<IdentityUser>> GetAllUsers()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<IdentityUser> GetUserByName(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task UpdateUserRole(string userName, string roleOld, string roleNew)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if(! await _userManager.IsInRoleAsync(user, roleNew))
            {
                await _userManager.AddToRoleAsync(user, roleNew);
                await _userManager.RemoveFromRoleAsync(user, roleOld);
            }
        }

        public async Task UpdateUserEmail(string userName, string email)
        {
            var user = await _userManager.FindByNameAsync(userName);
            user.Email = email;
            await _userManager.UpdateAsync(user);
        }

        public async Task<List<RoleEntity>> GetUsersWithRoles()
        {
            var users = await _userManager.Users.ToListAsync();

            var roles = new List<RoleEntity>();

            foreach(var user in users)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var rolesList = await userRoles.AsQueryable().ToListAsync();
                foreach (var role in rolesList)
                {
                    if (!role.Equals("Administrator"))
                    {
                        var roleEntity = new RoleEntity()
                        {
                            UserName = user.UserName,
                            UserRole = role
                        };
                        roles.Add(roleEntity);
                    }
                    
                }
            }
            return roles;
        }
    }
}
