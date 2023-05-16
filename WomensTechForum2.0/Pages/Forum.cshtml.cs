using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WomensTechForum2._0.Areas.Identity.Data;
using WomensTechForum2._0.Models;

namespace WomensTechForum2._0.Pages
{
    public class ForumModel : PageModel
    {
        private readonly Data.WomensTechForum2_0Context _context;
        public UserManager<WomensTechForum2_0User> _userManager;

        public ForumModel(Data.WomensTechForum2_0Context context, UserManager<WomensTechForum2_0User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public List<WomensTechForum2_0User> Users { get; set; }
        public WomensTechForum2_0User CurrentUser { get; set; }
        public List<MainCategory>? MainCategories { get; set; }
        public List<SubCategory>? SubCategories { get; set; }
        public MainCategory ChosenMainCategory { get; set; }
        public SubCategory ChosenSubCategory { get; set; }

        [BindProperty]
        public Post ChosenPost { get; set; }
        [BindProperty]
        public PostThread ChosenPostThread { get; set; }
        public List<Post> Posts { get; set; }
        public List<PostThread> PostThreads { get; set; }
        public List<LikePost> LikedPosts { get; set; }
        public List<LikePostThread> LikedPostThreads { get; set; }

        [BindProperty]
        public Post NewPost { get; set; }

        [BindProperty]
        public PostThread NewPostThread { get; set; }

        [BindProperty]
        public IFormFile UploadedImage { get; set; } //Läggs utanför databas-innehållet för att sparas som en sträng i db längre ner


        public async Task<IActionResult> OnGetAsync(int chosenMainId, int chosenSubId, int chosenPostId, int deleteid, int deletePTid, int changeId, int changePTId, int unlikepostid, int likepostid, int unlikePTid, int likePTid)
        {
            Users = await _userManager.Users.ToListAsync();
            CurrentUser = await _userManager.GetUserAsync(User);
            MainCategories = await _context.MainCategory.ToListAsync();
            SubCategories = await _context.SubCategory.ToListAsync();
            Posts = await _context.Post.ToListAsync();
            PostThreads = await _context.PostThread.ToListAsync();
            LikedPosts = await _context.LikePost.ToListAsync();
            LikedPostThreads = await _context.LikePostThread.ToListAsync();


            if (chosenMainId != 0)
            {
                ChosenMainCategory = MainCategories.FirstOrDefault(c => c.Id == chosenMainId);
            }
            if (chosenSubId != 0)
            {
                ChosenSubCategory = SubCategories.FirstOrDefault(c => c.Id == chosenSubId);
            }
            if (chosenPostId != 0)
            {
                ChosenPost = Posts.FirstOrDefault(c => c.Id == chosenPostId);
            }
            if (deleteid != 0)
            {
                Models.Post post = await _context.Post.FindAsync(deleteid);

                if (post != null)
                {
                    if (System.IO.File.Exists("./wwwroot/img/" + post.ImageSrc))
                    {
                        System.IO.File.Delete("./wwwroot/img/" + post.ImageSrc); //Ta bort bilden
                    }
                    _context.Post.Remove(post); //ta bort inlägget
                    await _context.SaveChangesAsync(); //Spara

                    return RedirectToPage("./Forum");//Tillbaka till startsidan
                }
            }
            if (deletePTid != 0)
            {
                Models.PostThread postThread = await _context.PostThread.FindAsync(deletePTid);

                if (postThread != null)
                {
                    if (System.IO.File.Exists("./wwwroot/img/" + postThread.ImageSrc))
                    {
                        System.IO.File.Delete("./wwwroot/img/" + postThread.ImageSrc); //Ta bort bilden
                    }
                    _context.PostThread.RemoveRange(postThread); //ta bort inlägget
                    await _context.SaveChangesAsync(); //Spara

                    return RedirectToPage("./Forum");//Tillbaka till startsidan
                }
            }
            if (changeId != 0)
            {
                Post offensivePost = await _context.Post.FindAsync(changeId);

                if (offensivePost != null)
                {
                    offensivePost.Offensive = true;
                    offensivePost.NoOfReports += 1;
                    await _context.SaveChangesAsync();
                }
            }
            if (changePTId != 0)
            {
                PostThread offensivePost = await _context.PostThread.FindAsync(changePTId);

                if (offensivePost != null)
                {
                    offensivePost.Offensive = true;
                    offensivePost.NoOfReports += 1;
                    await _context.SaveChangesAsync();
                }
            }
            if (unlikepostid != 0)
            {
                LikePost likePost = await _context.LikePost.FindAsync(unlikepostid);

                if (likePost != null)
                {
                    _context.LikePost.Remove(likePost); //ta bort inlägget
                    await _context.SaveChangesAsync(); //Spara

                    return RedirectToPage("./Forum");//Tillbaka till startsidan
                }
            }
            if (likepostid != 0)
            {
                var likePost = new LikePost()
                {
                    PostId = likepostid,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };

                _context.LikePost.Add(likePost); //lägg till i listan av likeposts
                await _context.SaveChangesAsync(); //Spara

                return RedirectToPage("./Forum");//Tillbaka till startsidan

            }
            if (unlikePTid != 0)
            {
                LikePostThread likePostThread = await _context.LikePostThread.FindAsync(unlikePTid);

                if (likePostThread != null)
                {
                    _context.LikePostThread.Remove(likePostThread); //ta bort inlägget
                    await _context.SaveChangesAsync(); //Spara

                    return RedirectToPage("./Forum");//Tillbaka till startsidan
                }
            }
            if (likePTid != 0)
            {
                var likePostThread = new LikePostThread()
                {
                    PostThreadId = likePTid,
                    UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
                };

                _context.LikePostThread.Add(likePostThread); //lägg till i listan av likeposts
                await _context.SaveChangesAsync(); //Spara

                return RedirectToPage("./Forum");//Tillbaka till startsidan

            }





            return Page();
        }

        public async Task<IActionResult> OnPostNewPostAsync()
        {
            string fileName = string.Empty;

            if (UploadedImage != null)
            {
                Random rnd = new();
                fileName = rnd.Next(100000).ToString() + UploadedImage.FileName;
                var file = "./wwwroot/img/" + fileName;

                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await UploadedImage.CopyToAsync(fileStream);
                }
            }


            NewPost.Date = DateTime.Now;
            NewPost.ImageSrc = fileName;
            NewPost.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            _context.Add(NewPost);
            await _context.SaveChangesAsync();



            return RedirectToPage("./Forum");
        }

        public async Task<IActionResult> OnPostNewPostThreadAsync()
        {
            string fileName = string.Empty;

            if (UploadedImage != null)
            {
                Random rnd = new();
                fileName = rnd.Next(100000).ToString() + UploadedImage.FileName;
                var file = "./wwwroot/img/" + fileName;

                using (var fileStream = new FileStream(file, FileMode.Create))
                {
                    await UploadedImage.CopyToAsync(fileStream);
                }
            }

            NewPostThread.Date = DateTime.Now;
            NewPostThread.ImageSrc = fileName;
            NewPostThread.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _context.Add(NewPostThread);
            await _context.SaveChangesAsync();



            return RedirectToPage("./Forum");
        }

        public bool CheckIfLiked(int postId, string userId)
        {
            var like = _context.LikePost.FirstOrDefault(p => p.PostId == postId && p.UserId == userId);

            return like != null;
        }

        public bool CheckIfPTLiked(int postId, string userId)
        {
            var like = _context.LikePostThread.FirstOrDefault(p => p.PostThreadId == postId && p.UserId == userId);

            return like != null;
        }

        //public async Task<IActionResult> OnPostOffensiveAsync()
        //{
        //    ChosenPost.Offensive = true;
        //    ChosenPost.NoOfReports += 1;
        //    //if (ChosenPost.Id != 0)
        //    //{
        //    //    Post offensivePost = await _context.Post.FindAsync(ChosenPost.Id);

        //    //    if (offensivePost != null)
        //    //    {
        //    //        offensivePost.Offensive = true;
        //    //        offensivePost.NoOfReports += 1;
        //    //        await _context.SaveChangesAsync();
        //    //    }
        //    //}
        //    return RedirectToPage("./Forum");

        //}
    }
}
