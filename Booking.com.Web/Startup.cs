using Autofac;
using Booking.com.Flights.Contexts;
using Booking.com.Flights;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Booking.com.Membership;
using Booking.com.Membership.Contexts;
using Booking.com.Membership.Entities;
using Booking.com.Membership.Services;
using Booking.com.Membership.BusinessObjects;
using Microsoft.AspNetCore.Authorization;

namespace Booking.com.Web
{
    public class Startup
    {
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            WebHostEnvironment = env;
            Configuration = builder.Build();
        }
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHostEnvironment { get; set; }
        public static ILifetimeScope AutofacContainer { get; set; }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            var connectionInfo = ConnectionStringAndAssemblyName();
            builder.RegisterModule(new WebModule());
            builder.RegisterModule(new FlightsModule(connectionInfo.connectionString,
                connectionInfo.migrationAssemblyName));
            builder.RegisterModule(new MembershipModule(connectionInfo.connectionString,
                connectionInfo.migrationAssemblyName));
        }
        private (string connectionString, string migrationAssemblyName) ConnectionStringAndAssemblyName()
        {
            var connectionStringName = "DefaultConnection";
            var connectionString = Configuration.GetConnectionString(connectionStringName);
            var migrationAssemblyName = typeof(Startup).Assembly.FullName;
            return (connectionString, migrationAssemblyName);
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionInfo = ConnectionStringAndAssemblyName();

            services.AddDbContext<FlightsContext>(options =>
                options.UseSqlServer(connectionInfo.connectionString,
                m => m.MigrationsAssembly(connectionInfo.migrationAssemblyName)));

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionInfo.connectionString,
                m => m.MigrationsAssembly(connectionInfo.migrationAssemblyName)));

            // Identity customization started here
            services
                .AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddUserManager<UserManager>()
                .AddRoleManager<RoleManager>()
                .AddSignInManager<SignInManager>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });


            services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
                options.LoginPath = "/Account/Signin";
                options.AccessDeniedPath = "/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(100);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            services.AddAuthorization(options =>
            { 
                options.AddPolicy("RequireCustomerRole", policy =>
                {
                     policy.RequireAuthenticatedUser();
                     policy.RequireRole("Customer");
                });

                options.AddPolicy("RequireViewPermissionClaim", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("ViewPermission");
                });

                options.AddPolicy("RequireViewPermission", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.AddRequirements(new ViewRequirement());
                });
            });

            services.AddSingleton<IAuthorizationHandler, ViewRequirementHandler>();
            services.AddControllersWithViews();
            services.AddHttpContextAccessor();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutofacContainer = app.ApplicationServices.GetAutofacRoot();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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

                endpoints.MapControllerRoute(
                name: "areas",
                pattern:
                "{area:exists}/{controller=Customer}/{action=Index}/{Id?}");
                
                endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{Id?}");
                endpoints.MapRazorPages();

            });
        }
    }
}
