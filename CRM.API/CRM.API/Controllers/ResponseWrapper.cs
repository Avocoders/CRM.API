using System;
using System.Threading.Tasks;
using CRM.API.Models.Input;
using Microsoft.AspNetCore.Mvc;
using TransactionStore.API.Models.Input;

namespace CRM.API.Controllers
{
    public class ResponseWrapper
    {
        private readonly Validator _validator;

        public ResponseWrapper(Validator validator)
        {
            _validator = validator;
        }

        public async ValueTask<string> CreateLeadRW(LeadInputModel leadModel)
        {
            var validationResult = new string[] { 
                await _validator.ValidateLeadInputModel(leadModel), 
                await _validator.ValidateLoginInfo(leadModel) 
            };
            if (!string.IsNullOrWhiteSpace(validationResult[0])) return validationResult[0];
            if (!string.IsNullOrWhiteSpace(validationResult[1])) return validationResult[1];
            return "";
        }

        public async ValueTask<string> UpdateLeadRW(LeadInputModel leadModel)
        {
            if (!leadModel.Id.HasValue) return "ID is empty";
            var validationResult = await _validator.ValidateLeadInputModel(leadModel);
            if (!string.IsNullOrWhiteSpace(validationResult)) return validationResult;
            return "";
        }

        public string UpdatePasswordRW(PasswordInputModel passwordModel)
        {
            string validationResult = _validator.ValidatePasswordInputModel(passwordModel);
            if (!string.IsNullOrWhiteSpace(validationResult)) return validationResult;
            return "";
        }

        public async ValueTask<string> UpdateEmailRW(EmailInputModel emailModel)
        {
            if (string.IsNullOrWhiteSpace(emailModel.Email)) return "Enter the email";
            string validationResult = await _validator.ValidateEmailInputModel(emailModel);
            if (!string.IsNullOrWhiteSpace(validationResult)) return validationResult;
            return "";
        }

       
    }
}
