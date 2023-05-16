using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WomensTechForum2._0.Data;
namespace WomensTechForum2._0
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var connectionString = builder.Configuration.GetConnectionString("WomensTechForum2_0ContextConnection") ?? throw new InvalidOperationException("Connection string 'WomensTechForum2_0ContextConnection' not found.");

            builder.Services.AddDbContext<WomensTechForum2_0Context>(options => options.UseSqlServer(connectionString));

            builder.Services.AddDefaultIdentity<Areas.Identity.Data.WomensTechForum2_0User>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<WomensTechForum2_0Context>();

            // Add services to the container.
            builder.Services.AddRazorPages();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}