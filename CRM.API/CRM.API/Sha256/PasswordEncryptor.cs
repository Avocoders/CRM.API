using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace CRM.API.Sha256
{
    public class PasswordEncryptor
    {
        private readonly string secretCode = "dQ7BDI967C7uSyX7";

        private readonly List<string> salts = new List<string>()
        {
            "sL45tY0g",
            "4C9VLP9P",
            "tof027Rx",
            "LrFT92h9",
            "Yx4kf5q8",
            "3lJT1DU2",
            "N4gv1mX1",
            "3t9n3kJG",
            "Xc3XDu72",
            "gK52BSH6"
        };

        public string EncryptPassword(string password)
        {
            Random random = new Random();
            int index = random.Next(0, salts.Count);
            byte[] data = new UTF8Encoding().GetBytes(password + secretCode + salts[index]);
            byte[] result;
            SHA256 converter = new SHA256Managed();
            result = converter.ComputeHash(data);
            return BitConverter.ToString(result).Replace("-", "").ToLower();
        }

        public bool CheckPassword(string passwordDb, string password)
        {
            for (int i = 0; i < salts.Count; i++)
            {
                byte[] data = new UTF8Encoding().GetBytes(password + secretCode + salts[i]);
                byte[] result;
                SHA256 converter = new SHA256Managed();
                result = converter.ComputeHash(data);
                string convertedPassword = BitConverter.ToString(result).Replace("-", "").ToLower();
                if (passwordDb.Equals(convertedPassword))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
