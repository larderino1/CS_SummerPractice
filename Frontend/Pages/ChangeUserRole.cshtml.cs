using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frontend.Data;
using Frontend.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Frontend.Pages
{
    [Authorize(Roles = "Administrator")]
    public class ChangeUserRoleModel : PageModel
    {

        private readonly IUserService userService;

        private readonly ApplicationDbContext context;

        private RoleStore<IdentityRole> store;

        [BindProperty]
        public IdentityUser User { get; set; }

        [BindProperty]
        public IdentityRole Role { get; set; }

        public static string _role;

        public static string _name;

        public ChangeUserRoleModel(UserManager<IdentityUser> manager, ApplicationDbContext context)
        {
            userService = new UserService(manager);
            this.context = context;
        }

        public async Task<IActionResult> OnGet(string name, string role)
        {
            _role = role;

            _name = name;

            User = await userService.GetUserByName(name);

            store = new RoleStore<IdentityRole>(context);

            var roles = await store.Roles.ToListAsync();

            ViewData["Roles"] = new SelectList(roles, "Id", "Name");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(IdentityRole Role)
        {
            var store = new RoleStore<IdentityRole>(context);

            var role = await store.FindByIdAsync(Role.Id);

            await userService.UpdateUserRole(_name, _role, role.Name);

            return RedirectToPage("./AdminPage");
        }
    }
}