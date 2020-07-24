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
using Microsoft.AspNetCore.Http;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeadController : Controller
    {
        private readonly ILogger<LeadController> _logger;
        
        private readonly Mapper _mapper;
        
        private readonly ILeadRepository _repo;

        private bool badLogin = true;
        public string newLogin;

        public LeadController(ILogger<LeadController> logger, ILeadRepository repo)
        {
            _logger = logger;
            _mapper = new Mapper();
            _repo = repo;
        }

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

        private string BadRequestsForLeadInputModelForUpdadeLead(LeadInputModel leadModel)
        {
            DataWrapper<int> dataWrapper = new DataWrapper<int>();
            if (string.IsNullOrWhiteSpace(leadModel.Login))
            {
                leadModel.Login = CreateLogin();
            }
            if (!string.IsNullOrWhiteSpace(leadModel.Login))
            {
                dataWrapper = _repo.FindLeadByLogin(leadModel.Login);
                if (dataWrapper.Data != 0) return ("User with this login already exists");
                if (!Regex.IsMatch(leadModel.Login, Validation.constantForLogin)) return ("The Login is incorrect");
            }
            if (string.IsNullOrWhiteSpace(leadModel.Email)) return ("Enter the email");
            if (!string.IsNullOrWhiteSpace(leadModel.Email))
            {
                dataWrapper = _repo.FindLeadByEmail(leadModel.Email);
                if (dataWrapper.Data != 0) return ("User with this email already exists");
                if ((!Regex.IsMatch(leadModel.Email, Validation.constantForEmail))) return ("The Email is incorrect");
            }
            return "";
        }

        /// <summary>
        /// gets the list of Leads with city info and role info   подрубить комменты
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>        
        //[Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        [HttpGet]
        public ActionResult<List<LeadOutputModel>> GetLeadsAll()
        {
            DataWrapper<List<LeadDto>> dataWrapper = _repo.GetAll();
            return MakeResponse(dataWrapper, _mapper.ConvertLeadDtosToLeadOutputModels);
        }


        //[Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        [HttpGet("{leadId}")]
        public ActionResult<LeadOutputModel> GetLeadById(long leadId) 
        {
            DataWrapper<LeadDto> dataWrapper = _repo.GetById(leadId);                           
            return MakeResponse(dataWrapper, _mapper.ConvertLeadDtoToLeadOutputModel);
        }

        //[Authorize()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<LeadOutputModel> CreateLead(LeadInputModel leadModel)
        {
            Validation validation = new Validation();
            string badRequest = validation.BadRequestsForLeadInputModel(leadModel);
            if (!string.IsNullOrWhiteSpace(badRequest)) return BadRequest(badRequest);
            string badRequestForUpdateLead = BadRequestsForLeadInputModelForUpdadeLead(leadModel);
            if (!string.IsNullOrWhiteSpace(badRequestForUpdateLead)) return BadRequest(badRequestForUpdateLead);
            leadModel.Password = new PasswordEncryptor().EncryptPassword(leadModel.Password);
            DataWrapper<LeadDto> newDataWrapper = _repo.Add(_mapper.ConvertLeadInputModelToLeadDTO(leadModel));
            return MakeResponse(newDataWrapper, _mapper.ConvertLeadDtoToLeadOutputModel);
        }

        //[Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public ActionResult<LeadOutputModel> UpdateLead(LeadInputModel leadModel)
        {
            if (!leadModel.Id.HasValue)
            {
                return BadRequest("ID is empty");
            }     
            var leadId = _repo.GetById(leadModel.Id.Value);
            if (leadId == null) return BadRequest("Lead was not found");
            Validation validation = new Validation();
            string badRequest = validation.BadRequestsForLeadInputModel(leadModel);
            if (!string.IsNullOrWhiteSpace(badRequest)) return BadRequest(badRequest);
            leadModel.Password = new PasswordEncryptor().EncryptPassword(leadModel.Password);
            DataWrapper<LeadDto> newDataWrapper = _repo.Update(_mapper.ConvertLeadInputModelToLeadDTO(leadModel));
            return MakeResponse(newDataWrapper, _mapper.ConvertLeadDtoToLeadOutputModel);
        }

        //[Authorize()]      
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{leadId}")]
        public ActionResult DeleteLeadById(long leadId) 
        {
            DataWrapper<LeadDto> dataWrapper = _repo.GetById(leadId);
            if (dataWrapper.Data.Id == null) return BadRequest("Lead was not found");
            _repo.Delete(leadId);
            return Ok("Successfully deleted");
        }

        //[Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
                if ((!Regex.IsMatch(emailModel.Email, Validation.constantForEmail))) return BadRequest("The Email is incorrect");
            }
            DataWrapper<string> newDataWrapper = _repo.UpdateEmailByLeadId(emailModel.Id, emailModel.Email);
            return MakeResponse(newDataWrapper);
        }

        //[Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]        
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
