using Google.Authenticator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;


namespace CRM.API
{
    public class GoogleAuthentication 
    {
        public static string AuthenticationCode { get; set; }
        public string AuthenticationTitle { get { return "Ankush"; } }   
        public string AuthenticationBarCodeImage { get; set;}
        public string AuthenticationManualCode { get; set; }  
        
        public bool ValidateTwoFactorPIN(string pin)
        {
            TwoFactorAuthenticator authenticator = new TwoFactorAuthenticator();
            return authenticator.ValidateTwoFactorPIN(AuthenticationCode, pin);
        }

        public bool GenerateTwoFactorAuthentication()
        {
            Guid guid = Guid.NewGuid();
            string uniqueUserKey = Convert.ToString(guid).Replace("-", "").Substring(0, 10);
            AuthenticationCode = uniqueUserKey;          
            TwoFactorAuthenticator authenticator = new TwoFactorAuthenticator();
            var setupInfo = authenticator.GenerateSetupCode("Complio", AuthenticationTitle, AuthenticationCode, false, 300);
            if (setupInfo != null)
            {
                AuthenticationBarCodeImage = setupInfo.QrCodeSetupImageUrl;
                AuthenticationManualCode = setupInfo.ManualEntryKey;
                return true;
            }
            return false;
        }
    }
}
