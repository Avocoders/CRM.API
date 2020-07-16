using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.API.Encryptor
{
    public class LoginEncryptor
    {
        public string EncryptorLogin()
        {
            string pass = "Log-";
            var r = new Random();
            while (pass.Length < 26)
            {
                Char c = (char)r.Next(0, 126);
                if (Char.IsLetterOrDigit(c))
                    pass += c;
            }
            return pass;
        }
    }
}
