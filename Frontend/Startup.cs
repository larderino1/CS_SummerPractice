using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Frontend.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity.UI.Services;
using Frontend.Services;
using Frontend.Services.ItemService;
using Frontend.Services.CategoryService;
using Frontend.Services.CartService;
using Frontend.Services.AzureStorageService;

namespace Frontend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("SqlDbConnectionString")));

            services.AddDbContext<AzureSqlDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("SqlDbConnectionString")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<IEmailSender, EmailSender>();
            services.AddScoped<IItemService, ItemService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<ICartService, CartService>();
            services.AddScoped<IAzureBlobStorage, AzureStorageService>();

            services.AddSession();

            services.AddHttpsRedirection(options => { options.HttpsPort = 443; });

            services.ConfigureApplicationCookie(o =>
            {
                o.ExpireTimeSpan = TimeSpan.FromDays(5);
                o.SlidingExpiration = true;
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("RequireAdministrationRole",
                    policy => policy.RequireRole("Administrator"));
                options.AddPolicy("RequiredSupplierRole", 
                    policy => policy.RequireRole("Supplier"));
                options.AddPolicy("RequiredRegisteredUser",
                    policy => policy.RequireAuthenticatedUser());
                options.AddPolicy("RequiredGuestRole",
                    policy => policy.RequireRole("Guest"));
            });

            services.AddAuthentication().AddMicrosoftAccount(options => 
            {
                options.ClientId = Configuration.GetValue<string>("MicrosoftClientId");
                options.ClientSecret = Configuration.GetValue<string>("MicrosoftClientSecret");
            });


            CreateRoles(service: services.BuildServiceProvider()).Wait();
            
            services.Configure<DataProtectionTokenProviderOptions>(o =>
            o.TokenLifespan = TimeSpan.FromHours(1));
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
            });

            app.UseHttpsRedirection();

            
        }

        private async Task CreateRoles(IServiceProvider service)
        {
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = service.GetRequiredService<UserManager<IdentityUser>>();

            string[] roles = { "Administrator", "Supplier", "Guest" };

            IdentityResult res;

            foreach (var role in roles)
            {
                var isRoleExist = await roleManager.RoleExistsAsync(role);
                if (!isRoleExist)
                {
                    res = await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var users = await userManager.Users.ToListAsync();
            foreach(var user in users)
            {
                if(await userManager.IsInRoleAsync(user, "Administrator"))
                {
                    continue;
                }
                if (await userManager.IsInRoleAsync(user, "Supplier"))
                {
                    continue;
                }
                else
                {
                    await userManager.AddToRoleAsync(user, "Guest");
                }
            }
        }
    }
}
