using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRM.API.Models.Input;
using CRM.API.Models.Output;
using CRM.API.Sha256;
using CRM.Data.DTO;
using CRM.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using CRM.API.Controllers;

namespace CRM.API
{
    public class Validation

    {
        public const string constantForPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])([a-zA-Z0-9@#$%^&+=*.\-_]){8,20}$";
        public const string constantForLogin = @"^((?!.*@.*\..*$))([a-zA-Z0-9@#$%^&+=*.\-_]){6,}$";
        public const string constantForEmail = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                 @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";
        public string BadRequestsForLeadInputModel(LeadInputModel leadModel)
        {
            DataWrapper<int> dataWrapper = new DataWrapper<int>();
            if (string.IsNullOrWhiteSpace(leadModel.FirstName)) return ("Enter the name");
            if (string.IsNullOrWhiteSpace(leadModel.LastName)) return ("Enter the last name");
            if (string.IsNullOrWhiteSpace(leadModel.Password)) return ("Enter a password");
            if (!Regex.IsMatch(leadModel.Password, constantForPassword)) return ("Password have to be between 8 and 20 characters long and contain lowercase, uppercase and number, possible characters: @#$%^&+=*.-_");
            if (string.IsNullOrWhiteSpace(leadModel.Phone)) return ("Enter the phone number");
            if (string.IsNullOrWhiteSpace(leadModel.Address)) return ("Enter the address");
            if (string.IsNullOrWhiteSpace(leadModel.BirthDate)) return ("Enter the date of birth");
            return "";
        }
        
    }
}
