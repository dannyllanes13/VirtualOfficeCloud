using System.Threading.Tasks;
using VirtualOfficeCloud.Data.Models;

namespace VirtualOfficeCloud.Utils.Interfaces
{
    public interface IStoreUserService
    {
        /// <summary>
        /// This method is to check is a user exist on database
        /// </summary>
        /// <param name="username"> Username to find in database (email in this case)</param>
        /// <returns> Return user (StoreUser) whether exist or null</returns>
        Task<StoreUser> FindByNameAsync(string username);

        /// <summary>
        /// This method sign in the user into app
        /// </summary>
        /// <param name="user">User entity for login (StoreUser)</param>
        /// <param name="password">Password from view</param>
        /// <param name="lockoutOnFailure"></param>
        /// <returns>Returns true whether user logged successfully or false if not</returns>
        Task<bool> SignInAsyncByPassword(StoreUser user, string password, bool lockoutOnFailure);

        /// <summary>
        /// This is to SignOut the user
        /// </summary>
        /// <returns></returns>
        Task<bool> SignOutAsync();
    }
}
