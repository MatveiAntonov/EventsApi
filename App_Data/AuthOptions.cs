using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Events.App_Data {
    public class AuthOptions {
        public const string ISSUER = "http://localhost:5000"; // издатель токена
        public const string AUDIENCE = "http://localhost:5000"; // потребитель токена
        const string KEY = "mysupersecret_secretkey!123";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
        }
    }
}
