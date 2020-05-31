using Frontend.Services.UserService;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Frontend.Services.UserService
{
    interface IUserService
    {
        public Task<List<IdentityUser>> GetAllUsers();
        public Task DeleteUser(string userName);
        public Task UpdateUserRole(string userName, string roleOld, string roleNew);
        public Task UpdateUserEmail(string userName, string email);
        public Task<List<RoleEntity>> GetUsersWithRoles();
    }
}
