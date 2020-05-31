using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frontend.Services.UserService;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Frontend.Pages
{
    public class DeleteUserModel : PageModel
    {
        private IUserService _service;
        private UserManager<IdentityUser> _manager;
        private static string _name;

        [BindProperty]
        public IdentityUser UserManager { get; set; }

        public DeleteUserModel(UserManager<IdentityUser> manager)
        {
            _service = new UserService(manager);
            _manager = manager;
        }

        public async Task<IActionResult> OnGet(string name)
        {
            _name = name;
            UserManager = await _manager.FindByEmailAsync(name);
            return Page();
        }

        public async Task<IActionResult> OnPostDeleteUserAsync()
        {
            await _service.DeleteUser(_name);
            return RedirectToPage("./AdminPage");
        }
    }
}