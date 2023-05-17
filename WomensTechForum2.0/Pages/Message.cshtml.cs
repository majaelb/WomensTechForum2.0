using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WomensTechForum2._0.Models;

namespace WomensTechForum2._0.Pages
{
    public class MessageModel : PageModel
    {
        [BindProperty]
        public Message NewMessage { get; set; }

        public List<Message> Messages { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
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
            return Page();

        }
    }
}
