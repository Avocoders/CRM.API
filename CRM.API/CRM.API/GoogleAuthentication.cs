using Google.Authenticator;

namespace CRM.API
{
    public class GoogleAuthentication 
    {
        public string AuthenticationTitle { get { return "UltraBack";}}           
        public string AuthenticationManualCode { get; set; }  
        
        public bool ValidateTwoFactorPIN(long accountId,string pin)
        {
            var authenticationCode = accountId.ToString() + "ULTRABACK";
            TwoFactorAuthenticator authenticator = new TwoFactorAuthenticator();
            return authenticator.ValidateTwoFactorPIN(authenticationCode, pin);
        }

        public bool GenerateTwoFactorAuthentication(long accountId)
        {
            var authenticationCode = accountId.ToString()+ "ULTRABACK";  
            TwoFactorAuthenticator authenticator = new TwoFactorAuthenticator();
            var setupInfo = authenticator.GenerateSetupCode("Complio", AuthenticationTitle, authenticationCode, false, 300);
            if (setupInfo != null)
            {
                AuthenticationManualCode = setupInfo.ManualEntryKey;
                return true;
            }
            return false;
        }
    }
}
