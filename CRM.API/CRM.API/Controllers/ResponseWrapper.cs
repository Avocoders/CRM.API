using System;
using CRM.API.Models.Input;
using Microsoft.AspNetCore.Mvc;

namespace CRM.API.Controllers
{
    public class ResponseWrapper
    {
        private readonly Validator _validator;

        public ResponseWrapper(Validator validator)
        {
            _validator = validator;
        }

        public string CreateLeadRW(LeadInputModel leadModel)
        {
            var validationResult = new string[] { 
                _validator.ValidateLeadInputModel(leadModel), 
                _validator.ValidateLoginInfo(leadModel) 
            };
            if (!string.IsNullOrWhiteSpace(validationResult[0])) return validationResult[0];
            if (!string.IsNullOrWhiteSpace(validationResult[1])) return validationResult[1];
            return "";
        }

        public string UpdateLeadRW(LeadInputModel leadModel)
        {
            if (!leadModel.Id.HasValue) return "ID is empty";
            var validationResult = _validator.ValidateLeadInputModel(leadModel);
            if (!string.IsNullOrWhiteSpace(validationResult)) return validationResult;
            return "";
        }

        public string UpdatePasswordRW(PasswordInputModel passwordModel)
        {
            string validationResult = _validator.ValidatePasswordInputModel(passwordModel);
            if (!string.IsNullOrWhiteSpace(validationResult)) return validationResult;
            return "";
        }

        public string UpdateEmailRW(EmailInputModel emailModel)
        {
            if (string.IsNullOrWhiteSpace(emailModel.Email)) return "Enter the email";
            string validationResult = _validator.ValidateEmailInputModel(emailModel);
            if (!string.IsNullOrWhiteSpace(validationResult)) return validationResult;
            return "";
        }
    }
}
