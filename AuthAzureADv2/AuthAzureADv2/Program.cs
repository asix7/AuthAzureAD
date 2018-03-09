using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthAzureADv2
{
    class Program
    {

            static readonly string aadInstance = "https://login.microsoftonline.com/{0}";

            static readonly string tenant = "moderna.com.ec";

            static readonly string clientId = "58a83bcf-cb2f-4444-add8-9d6b6f9433f3";

            static readonly string todoListResourceId = "https://moderna.com.ec/TodoListService";



            static void Main(string[] args)
            {

                string authority = string.Format(CultureInfo.InvariantCulture, aadInstance, tenant);

                AuthenticationContext authContext = new AuthenticationContext(authority);

                UserCredential uc = new UserCredential("ldap.mc1@moderna.com.ec", "Hqtumc2017");

                var result = GetAccessToken(authContext, todoListResourceId, clientId, uc, authority);
                var token = result.AccessToken;


                Console.WriteLine(token);

                token = authContext.AcquireTokenByRefreshTokenAsync(result.RefreshToken, clientId).Result.AccessToken;

                Console.WriteLine(token);
                Console.Read();

            }


            static AuthenticationResult GetAccessToken(AuthenticationContext authContext, string todoListResourceId, string clientId, UserCredential credential, string authority)
            {
                try
                {
                    var result = authContext.AcquireTokenAsync(todoListResourceId, clientId, credential).Result;                                     
                    return result;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error en la obtención del token:" + e.Message);
                    return null;
                }
            }
        }
    
}
