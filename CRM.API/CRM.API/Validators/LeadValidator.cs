using CRM.API.Models.Input;
using CRM.Data;
using System.Text.RegularExpressions;
using CRM.API.Encryptor;
using TransactionStore.API.Models.Input;

namespace CRM.API
{
    public class LeadValidator
    {
        private readonly ILeadRepository _repo;
        public const string passwordPattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])([a-zA-Z0-9@#$%^&+=*.\-_]){8,20}$";
        public const string loginPattern = @"^((?!.*@.*\..*$))([a-zA-Z0-9@#$%^&+=*.\-_]){6,}$";
        public const string emailPattern = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        public LeadValidator(ILeadRepository repo)
        {
            _repo = repo;
        }

        private string CreateLogin()
        {
            DataWrapper<int> dataWrapper;
            while (true)
            {
                var newLogin = new LoginEncryptor().EncryptorLogin();
                dataWrapper = _repo.FindLeadByLogin(newLogin);
                if (dataWrapper.Data == 0) return newLogin;
            }
        }

        public string ValidateLeadInputModel(LeadInputModel leadModel)
        {            
            if (string.IsNullOrWhiteSpace(leadModel.FirstName)) return ("Enter the name");
            if (string.IsNullOrWhiteSpace(leadModel.LastName)) return ("Enter the last name");
            if (string.IsNullOrWhiteSpace(leadModel.Password)) return ("Enter a password");
            if (!Regex.IsMatch(leadModel.Password, passwordPattern)) return ("Password have to be between 8 and 20 characters long and contain lowercase, uppercase and number, possible characters: @#$%^&+=*.-_");
            if (string.IsNullOrWhiteSpace(leadModel.Phone)) return ("Enter the phone number");
            if (string.IsNullOrWhiteSpace(leadModel.Address)) return ("Enter the address");
            if (string.IsNullOrWhiteSpace(leadModel.BirthDate)) return ("Enter the date of birth");
            return "";
        }
        
        public string ValidatePasswordInputModel(PasswordInputModel passwordModel)
        { 
            if (!Regex.IsMatch(passwordModel.Password, passwordPattern)) return ("Password have to be between 8 and 20 characters long and contain lowercase, uppercase and number, possible characters: @#$%^&+=*.-_");
            return "";
        }

        public string ValidateLoginInfo(LeadInputModel leadModel)
        {
            DataWrapper<int> dataWrapper;
            if (string.IsNullOrWhiteSpace(leadModel.Login))
            {
                leadModel.Login = CreateLogin();
            }
            if (!string.IsNullOrWhiteSpace(leadModel.Login))
            {
                dataWrapper = _repo.FindLeadByLogin(leadModel.Login);
                if (dataWrapper.Data != 0) return ("User with this login already exists");
                if (!Regex.IsMatch(leadModel.Login, LeadValidator.loginPattern)) return ("The Login is incorrect");
            }
            if (string.IsNullOrWhiteSpace(leadModel.Email)) return ("Enter the email");
            if (!string.IsNullOrWhiteSpace(leadModel.Email))
            {
                dataWrapper = _repo.CheckEmail(leadModel.Email);
                if (dataWrapper.Data != 0) return ("User with this email already exists");
                if ((!Regex.IsMatch(leadModel.Email, LeadValidator.emailPattern))) return ("The Email is incorrect");
            }
            return "";
        }

        //public string ValidateTransactionInputModel(TransactionInputModel transactionModel)
        //{
        //    DataWrapper<string> dataWrapper;
        //    if (_repo.GetAccountById(transactionModel.AccountId).Data is null)
        //    {
        //        return BadRequest("The account is not found");
        //    }
        //    if (_repo.GetAccountById(transactionModel.AccountId).Data is null) return BadRequest("The account is not found");
        //    if (_repo.GetAccountById(transactionModel.AccountIdReceiver).Data is null) return BadRequest("The account of re
        //    if (transactionModel.Amount <= 0) return BadRequest("The amount is missing");
        //}
        //return "";
    }
}
