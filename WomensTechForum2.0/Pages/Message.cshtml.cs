using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WomensTechForum2._0.Areas.Identity.Data;
using WomensTechForum2._0.Models;

namespace WomensTechForum2._0.Pages
{
    public class MessageModel : PageModel
    {
        public UserManager<WomensTechForum2_0User> _userManager;

        [BindProperty]
        public Message NewMessage { get; set; }
        public List<Message> Messages { get; set; }
        public List<WomensTechForum2_0User> Users { get; set; }
        public WomensTechForum2_0User CurrentUser { get; set; }

        public MessageModel(UserManager<WomensTechForum2_0User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            ViewData["ReceiverId"] = new SelectList(_userManager.Users, "Id", "FirstName");


            Users = await _userManager.Users.ToListAsync();
            CurrentUser = await _userManager.GetUserAsync(User);
            Messages = await DAL.MessageManager.GetAllMessages();

            return Page();
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (NewMessage.Title != null)
            {
                await DAL.MessageManager.SaveMessage(NewMessage);
            }
            Messages = await DAL.MessageManager.GetAllMessages();
            return RedirectToPage("./Message");

        }
    }
}
