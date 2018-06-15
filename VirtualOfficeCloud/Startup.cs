using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Text;
using VirtualOfficeCloud.Data.DbContext;
using VirtualOfficeCloud.Data.Models;
using VirtualOfficeCloud.Utils.Implementation;
using VirtualOfficeCloud.Utils.Interfaces;

namespace VirtualOfficeCloud
{
    public class Startup
    {
        private readonly IConfiguration _config;
        private readonly IHostingEnvironment _env;

        public Startup(IConfiguration configuration, IHostingEnvironment env)
        {
            _config = configuration;
            _env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<StoreUser, IdentityRole>(o =>
            {
                // configure identity options
                o.Password.RequireDigit = false;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 6;
                o.Lockout.DefaultLockoutTimeSpan = TimeSpan.Zero;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            //this part is to use authentication in api
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(cfg =>
                //this parameter are the same I used when I create it in AccountController
                {
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = _config["Tokens:issuer"],
                        ValidAudience = _config["Tokens:audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:key"]))                        
                    };
                });

            //here set session time for 4
            services.AddSession(opt => opt.IdleTimeout = TimeSpan.FromHours(1));

            //here add the db context in our app
            services.AddDbContext<ApplicationDbContext>(cfg =>
            {
                cfg.UseSqlServer(_config.GetConnectionString("VocConnectionString"));
            });

            /***************************************** Dependency Injection ****************************************/

            //this section is to add dependency injection
            services.AddTransient<ApplicationSeeder>();

            //Main Project Utils-------------------------------------------------
            
            //Use AddScope to get the same reference in the same request
            services.AddScoped<IStoreUserService, StoreUserService>();
            services.AddScoped<IToken, Token>();

            /*******************************************************************************************************/


            //this is to activate the mvc on the project
            services.AddMvc(opt =>
            {
                //this is to activate https in production
                if (_env.IsProduction())
                {
                    opt.Filters.Add(new RequireHttpsAttribute());
                }
            })
            //this is when the process find a loop the json serializer ignore them (this is when I don't have View Models)
            .AddJsonOptions(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //the order matters and I need to place this before UseStaticFiles()
            app.UseStaticFiles();

            //here turn on identity (tis need to be before mvc)
            app.UseAuthentication();

            // must go before app.UseMvc()
            app.UseSession(); 

            //here create the map route for the path on the browser reach the controller and action
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller=Home}/{action=Index}/{id?}");
            });

            //this is to create the seeder class and call one time in development and seed the database the first time
            if (env.IsDevelopment())
            {
                //Seed the database
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var seeder = scope.ServiceProvider.GetService<ApplicationSeeder>();
                    seeder.Seed().Wait();
                   
                }
            }
        }
    }
}
