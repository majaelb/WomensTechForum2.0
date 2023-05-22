using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WomensTechForum2._0.Areas.Identity.Data;

namespace WomensTechForum2._0.Pages.Admin.RoleAdmin
{
    public class IndexModel : PageModel
    {
        public UserManager<WomensTechForum2_0User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public List<WomensTechForum2_0User> Users { get; set; }
        public List<IdentityRole> Roles { get; set; }

        [BindProperty]
        public string RoleName { get; set; }


        [BindProperty(SupportsGet = true)]
        public string AddUserId { get; set; }


        [BindProperty(SupportsGet = true)]
        public string RemoveUserId { get; set; }


        [BindProperty(SupportsGet = true)]
        public string Role { get; set; }
        [BindProperty]
        public int ModalUserId { get; set; }
        [BindProperty]
        public string ModalRoleName { get; set; }



        public IndexModel(RoleManager<IdentityRole> roleManager,
             UserManager<WomensTechForum2_0User> userManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
     

        public async Task<IActionResult> OnGetAsync(int modalUserId, string modalRoleName)
        {
            Roles = await _roleManager.Roles.ToListAsync();
            Users = await _userManager.Users.ToListAsync();
            ModalUserId = modalUserId;
            ModalRoleName = modalRoleName;
            if (AddUserId != null)
            {
                var alterUser = await _userManager.FindByIdAsync(AddUserId);
                var roleResult = await _userManager.AddToRoleAsync(alterUser, Role);
            }
            if (RemoveUserId != null)
            {
                var alterUser = await _userManager.FindByIdAsync(RemoveUserId);
                var roleResult = await _userManager.RemoveFromRoleAsync(alterUser, Role);
            }


            //Demo av roller
            //var currentUser = await _userManager.GetUserAsync(User);
            //if (currentUser != null)
            //{

            //    IsUser = await _userManager.IsInRoleAsync(currentUser, "User");
            //    IsAdmin = await _userManager.IsInRoleAsync(currentUser, "Admin");
            //}

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (RoleName != null)
            {
                await CreateRole(RoleName);
            }

            return RedirectToPage("./Index");
        }


        public async Task CreateRole(string roleName)
        {
            bool exist = await _roleManager.RoleExistsAsync(roleName);

            if (!exist)
            {
                var role = new IdentityRole()
                {
                    Name = roleName
                };

                await _roleManager.CreateAsync(role);
            }
        }
    }
}
