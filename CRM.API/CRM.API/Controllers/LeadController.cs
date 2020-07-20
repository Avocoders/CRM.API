using System.Collections.Generic;
using CRM.API.Models.Input;
using CRM.API.Models.Output;
using CRM.API.Sha256;
using CRM.Data.DTO;
using CRM.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using CRM.API.Encryptor;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeadController : Controller
    {
        private readonly ILogger<LeadController> _logger;
        
        private readonly Mapper _mapper;
        
        private readonly LeadRepository _repo;

        private bool badLogin = true;
        public string newLogin;

        string constantForPassword = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])([a-zA-Z0-9@#$%^&+=*.\-_]){8,20}$";
        string constantForLogin = @"^((?!.*@.*\..*$))([a-zA-Z0-9@#$%^&+=*.\-_]){6,}$";
        string constantForEmail = @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-0-9a-z]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$";

        private string CreateLogin()
        {
            DataWrapper<int> dataWrapper;
            while (badLogin)
            {
                newLogin = new LoginEncryptor().EncryptorLogin();
                dataWrapper = _repo.FindLeadByLogin(newLogin);
                if (dataWrapper.Data == 0) badLogin = false;                
            }
            return newLogin;
        }

        public LeadController(ILogger<LeadController> logger)
        {
            _logger = logger;
            _mapper = new Mapper();
            _repo = new LeadRepository();
        }

        //[Authorize()]
        [HttpGet]
        public ActionResult<List<LeadOutputModel>> GetLeadsAll()
        {
            DataWrapper<List<LeadDto>> dataWrapper = _repo.GetAll();
            return MakeResponse(dataWrapper, _mapper.ConvertLeadDtosToLeadOutputModels);
        }

        //[Authorize()]
        [HttpGet("{leadId}")]
        public ActionResult<LeadOutputModel> GetLeadById(long leadId) 
        {
            DataWrapper<LeadDto> dataWrapper = _repo.GetById(leadId);                           
            return MakeResponse(dataWrapper, _mapper.ConvertLeadDtoToLeadOutputModel);
        }

        //[Authorize()]
        [HttpPost]
        public ActionResult<LeadOutputModel> CreateLead(LeadInputModel leadModel)
        {
            DataWrapper<int> dataWrapper = new DataWrapper<int>();
            if (string.IsNullOrWhiteSpace(leadModel.FirstName)) return BadRequest("Enter the name");
            if (string.IsNullOrWhiteSpace(leadModel.LastName)) return BadRequest("Enter the last name");
            if (string.IsNullOrWhiteSpace(leadModel.Login)) 
            {
                leadModel.Login = CreateLogin();                
            }             
            if (!string.IsNullOrWhiteSpace(leadModel.Login))
            {
                dataWrapper = _repo.FindLeadByLogin(leadModel.Login);
                if (dataWrapper.Data != 0) return BadRequest("User with this login already exists");
                if (!Regex.IsMatch(leadModel.Login, constantForLogin)) return BadRequest("The Login is incorrect");
            }
            if (string.IsNullOrWhiteSpace(leadModel.Email)) return BadRequest("Enter the email");
            if (!string.IsNullOrWhiteSpace(leadModel.Email))
            {
                dataWrapper = _repo.FindLeadByEmail(leadModel.Email);
                if (dataWrapper.Data != 0) return BadRequest("User with this email already exists");
                if((!Regex.IsMatch(leadModel.Email, constantForEmail))) return BadRequest("The Email is incorrect");
            }
            if (string.IsNullOrWhiteSpace(leadModel.Password)) return BadRequest("Enter a password");
            if (!Regex.IsMatch(leadModel.Password, constantForPassword)) return BadRequest("Password have to be between 8 and 20 characters long and contain lowercase, uppercase and number, possible characters: @#$%^&+=*.-_");
            if (string.IsNullOrWhiteSpace(leadModel.Phone)) return BadRequest("Enter the phone number");
            if (string.IsNullOrWhiteSpace(leadModel.Address)) return BadRequest("Enter the address");
            if (string.IsNullOrWhiteSpace(leadModel.BirthDate)) return BadRequest("Enter the date of birth");
            leadModel.Password = new PasswordEncryptor().EncryptPassword(leadModel.Password);
            DataWrapper<LeadDto> newDataWrapper = _repo.Add(_mapper.ConvertLeadInputModelToLeadDTO(leadModel));
            return MakeResponse(newDataWrapper, _mapper.ConvertLeadDtoToLeadOutputModel);
        }

        //[Authorize()]
        [HttpPut]
        public ActionResult<LeadOutputModel> UpdateLead(LeadInputModel leadModel)
        {
            if (!leadModel.Id.HasValue)
            {
                return BadRequest("ID is empty");
            }     
            var leadId = _repo.GetById(leadModel.Id.Value);
            if (leadId == null) return BadRequest("Lead was not found");
            if (string.IsNullOrWhiteSpace(leadModel.FirstName)) return BadRequest("Enter the name");
            if (string.IsNullOrWhiteSpace(leadModel.LastName)) return BadRequest("Enter the last name");
            if (string.IsNullOrWhiteSpace(leadModel.Phone)) return BadRequest("Enter the phone number");
            if (string.IsNullOrWhiteSpace(leadModel.Address)) return BadRequest("Enter the address");
            if (string.IsNullOrWhiteSpace(leadModel.BirthDate)) return BadRequest("Enter the date of birth");
            if (string.IsNullOrWhiteSpace(leadModel.Password)) return BadRequest("Enter a password");
            if (!Regex.IsMatch(leadModel.Password, constantForPassword)) return BadRequest("Password have to be between 8 and 20 characters long and contain lowercase, uppercase and number, possible characters: @#$%^&+=*.-_");
            leadModel.Password = new PasswordEncryptor().EncryptPassword(leadModel.Password);
            DataWrapper<LeadDto> newDataWrapper = _repo.Update(_mapper.ConvertLeadInputModelToLeadDTO(leadModel));
            return MakeResponse(newDataWrapper, _mapper.ConvertLeadDtoToLeadOutputModel);
        }

        //[Authorize()]      
        [HttpDelete("{leadId}")]
        public ActionResult DeleteLeadById(long leadId) 
        {
            DataWrapper<LeadDto> dataWrapper = _repo.GetById(leadId);
            if (dataWrapper.Data.Id == null) return BadRequest("Lead was not found");
            _repo.Delete(leadId);
            return Ok("Successfully deleted");
        }

        //[Authorize()]
        [HttpPatch]
        public ActionResult<string> UpdateEmailByLeadId(EmailInputModel emailModel)
        {
            var leadId = _repo.GetById(emailModel.Id.Value);
            if (leadId == null) return BadRequest("Lead was not found");
            if (string.IsNullOrWhiteSpace(emailModel.Email))
            {
                return BadRequest("Enter the email");
            }
            else            
            {
                DataWrapper<int> dataWrapper = _repo.FindLeadByEmail(emailModel.Email);
                if (dataWrapper.Data != 0) return BadRequest("User with this email already exists");
                if ((!Regex.IsMatch(emailModel.Email, constantForEmail))) return BadRequest("The Email is incorrect");
            }
            DataWrapper<string> newDataWrapper = _repo.UpdateEmailByLeadId(emailModel.Id, emailModel.Email);
            return MakeResponse(newDataWrapper);
        }

        //[Authorize()]
        [HttpPost("search")]
        public ActionResult<List<LeadOutputModel>> SearchLead(SearchParametersInputModel searchparameters)
        {
            LeadSearchParameters searchParams = _mapper.ConvertSearchParametersInputModelToLeadSearchParameters(searchparameters);
            DataWrapper<List<LeadDto>> dataWrapper = _repo.SearchLeads(searchParams);
            return MakeResponse(dataWrapper, _mapper.ConvertLeadDtosToLeadOutputModels);         
        }

        private delegate T DtoConverter<T,K>(K dto);

        private ActionResult<T> MakeResponse<T>(DataWrapper<T> dataWrapper)
        {
            if (!dataWrapper.IsOk)
            {
                return BadRequest(dataWrapper.ExceptionMessage);
            }
            return Ok(dataWrapper.Data);
        }

        private ActionResult<T> MakeResponse<T, K>(DataWrapper<K> dataWrapper, DtoConverter<T, K> dtoConverter)
        {
            if (!dataWrapper.IsOk)
            {
                return BadRequest(dataWrapper.ExceptionMessage);
            }
            return Ok(dtoConverter(dataWrapper.Data));
        }
    }
}
