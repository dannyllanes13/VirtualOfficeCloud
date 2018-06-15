using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using VirtualOfficeCloud.Data.Models;
using VirtualOfficeCloud.Utils.Interfaces;

namespace VirtualOfficeCloud.Utils.Implementation
{
    public class StoreUserService : IStoreUserService
    {
        private readonly ILogger<StoreUserService> _logger;
        private readonly SignInManager<StoreUser> _signInManager;
        private readonly UserManager<StoreUser> _userManager;

        public StoreUserService(ILogger<StoreUserService> logger, SignInManager<StoreUser> signInManager, UserManager<StoreUser> userManager)
        {
            _logger = logger;
            _signInManager = signInManager;
            _userManager = userManager;
        }


        public async Task<StoreUser> FindByNameAsync(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user != null)
            {
                return user;
            }

            return null;
        }

        public async Task<bool> SignInAsyncByPassword(StoreUser user, string password, bool lockoutOnFailure)
        {
            var result = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure);
            return result.Succeeded;
        }

        public async Task<bool> SignOutAsync()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return true;
            }
            catch (Exception e)
            {
                _logger.LogError("User Log Out", e.Message);
                return false;
            }
        }
    }
}
