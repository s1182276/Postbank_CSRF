using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Postbank.Data;
using System.Configuration;
namespace Postbank
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddDbContext<PostbankContext>(options =>
                options.UseSqlite(builder.Configuration.GetConnectionString("PostbankContext") ?? throw new InvalidOperationException("Connection string 'PostbankContext' not found.")));

            // Add services to the container.

            // Disable CSRF token globally -- we set this manually on the codebehind!
            //builder.Services.AddRazorPages().AddRazorPagesOptions(o =>
            //{
            //    o.Conventions.ConfigureFilter(new IgnoreAntiforgeryTokenAttribute());
            //});

            builder.Services.AddRazorPages();
            builder.Services.AddSession();
            //builder.Services.AddCors();

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

            //Allow any origin
            //app.UseCors(builder => builder
            //           .AllowAnyHeader()
            //           .AllowAnyMethod()
            //           .AllowAnyOrigin()
            //        );



            app.UseAuthorization();
            app.UseSession();

            app.MapRazorPages();

            app.Run();
        }
    }
}
