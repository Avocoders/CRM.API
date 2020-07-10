using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CRM.API.Models
{
    public class TokenOptions
    {
        public const string ISSUER = "80.78.240.16";
        public const string AUDIENCE = "TestingSystem";
        const string KEY = "itdevspbitdevspb"; 
        public const int LIFETIME = 300; 
        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
