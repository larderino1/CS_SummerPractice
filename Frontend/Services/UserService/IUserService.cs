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
        Task<List<IdentityUser>> GetAllUsers();
        Task DeleteUser(string userName);
        Task UpdateUserRole(string userName, string roleOld, string roleNew);
        Task UpdateUserEmail(string userName, string email);
        Task<List<RoleEntity>> GetUsersWithRoles();
    }
}
