using VirtualOfficeCloud.Utils.Implementation;

namespace VirtualOfficeCloud.Utils.Interfaces
{
    public interface IToken
    {
        /// <summary>
        /// Create token to use in Autorization headers to prevent outside user
        /// </summary>
        /// <param name="username">username of the user logged in</param>
        /// <param name="email">email of the user logged in</param>
        /// <returns>Returns token and expiration in TokenValues entity (Token, Expiration)</returns>
        TokenValues CreateToken(string username, string email);
    }
}
