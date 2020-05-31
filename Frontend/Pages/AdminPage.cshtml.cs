using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frontend.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace Frontend.Pages
{
    [Authorize(Roles = "Administrator")]
    public class AdminPageModel : PageModel
    {

        public List<RoleEntity> Users { get; set; }

        private readonly IUserService userService;
        public AdminPageModel(UserManager<IdentityUser> manager)
        {
            userService = new UserService(manager);
        }

        public async Task<IActionResult> OnGet()
        {
            Users = await userService.GetUsersWithRoles();
            return Page();
        }
    }
}