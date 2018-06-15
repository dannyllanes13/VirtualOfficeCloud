using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using VirtualOfficeCloud.Data.Models;

namespace VirtualOfficeCloud.Data.DbContext
{
    public class ApplicationSeeder
    {
        private readonly ApplicationDbContext _ctx;
        private readonly IHostingEnvironment _hosting;
        private readonly UserManager<StoreUser> _userManager;

        public ApplicationSeeder(ApplicationDbContext ctx, IHostingEnvironment hosting, UserManager<StoreUser> userManager)
        {
            _ctx = ctx;
            _hosting = hosting;
            _userManager = userManager;
        }

        public async Task Seed()
        {
            //check if the database was created
            _ctx.Database.EnsureCreated();

            //this part is to create the first user
            var user = await _userManager.FindByEmailAsync("dllanes@emphasys-software.com");
            if (user == null)
            {
                user = new StoreUser()
                {
                    FirstName = "Danny",
                    LastName = "Llanes",
                    UserName = "dllanes@emphasys-software.com",
                    Email = "dllanes@emphasys-software.com",
                    Person = new Persons()
                    {
                        AccessLevel = "System",
                        CreateDate = DateTime.UtcNow,
                        IsActive = true,
                        LastModifiedDate = DateTime.UtcNow,
                        Contact = new Contacts()
                        {
                            FirstName = "Danny",
                            LastName = "Llanes",
                            Email = "dllanes@emphasys-software.com"                            
                        }
                    }
                };

                var result = await _userManager.CreateAsync(user, "Dllanes1");
                if (result != IdentityResult.Success)
                {
                    throw new InvalidOperationException("Failed to create default user");
                }

                _ctx.SaveChanges();

            }

        }
    }
}
