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
        public string AuthenticationTitle { get { return "UltraBack"; } }   
        public string AuthenticationBarCodeImage { get; set;}
        public string AuthenticationManualCode { get; set; }  
        
        public bool ValidateTwoFactorPIN(long accountId,string pin)
        {
            var authenticationCode = accountId.ToString() + "ULTRABACK";
            TwoFactorAuthenticator authenticator = new TwoFactorAuthenticator();
            return authenticator.ValidateTwoFactorPIN(authenticationCode, pin);
        }

        public bool GenerateTwoFactorAuthentication(long accountId)
        {
            var authenticationCode = accountId.ToString()+"ULTRABACK";  
            TwoFactorAuthenticator authenticator = new TwoFactorAuthenticator();
            var setupInfo = authenticator.GenerateSetupCode("Complio", AuthenticationTitle, authenticationCode, false, 300);
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
