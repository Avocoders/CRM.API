using System.Collections.Generic;
using CRM.API.Models.Input;
using CRM.API.Models.Output;
using CRM.API.Sha256;
using CRM.Data.DTO;
using CRM.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using AutoMapper;

namespace CRM.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LeadController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ILeadRepository _repo;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        public LeadController(ILogger<LeadController> logger, ILeadRepository repo, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _repo = repo;
        }

        /// <summary>
        /// gets the lead by Id with all information
        /// </summary>
        /// <param name="leadId"></param>       
        //[Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{leadId}")]
        public ActionResult<LeadOutputModel> GetLeadById(long leadId)
        {
            DataWrapper<LeadDto> dataWrapper = _repo.GetById(leadId);
            return MakeResponse(dataWrapper, _mapper.Map<LeadOutputModel>);
        }

        /// <summary>
        /// creates a new lead
        /// </summary>
        /// <param name="leadModel"></param>       
        //[Authorize()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<LeadOutputModel> CreateLead(LeadInputModel leadModel)   //поменять названия переменных
        {
            LeadValidator validation = new LeadValidator(_repo);
            string validationResult = validation.ValidateLeadInputModel(leadModel);
            if (!string.IsNullOrWhiteSpace(validationResult)) return BadRequest(validationResult);
            string loginValidationResult = validation.ValidateLoginInfo(leadModel);
            if (!string.IsNullOrWhiteSpace(loginValidationResult)) return BadRequest(loginValidationResult);
            leadModel.Password = new PasswordEncryptor().EncryptPassword(leadModel.Password);  //возможно не в контроллере должна быть, в автомап
            DataWrapper<LeadDto> newDataWrapper = _repo.AddOrUpdateLead(_mapper.Map<LeadDto>(leadModel));
            _logger.LogInformation($"Create new lead with Id: {newDataWrapper.Data.Id}");
            return MakeResponse(newDataWrapper, _mapper.Map<LeadOutputModel>);
        }

        /// <summary>
        /// edits lead's information by leadId
        /// </summary>
        /// <param name="leadModel"></param>        
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
            LeadValidator validation = new LeadValidator(_repo);
            string badRequest = validation.ValidateLeadInputModel(leadModel);
            if (!string.IsNullOrWhiteSpace(badRequest)) return BadRequest(badRequest);
            leadModel.Password = new PasswordEncryptor().EncryptPassword(leadModel.Password); // нельзя поменять пароль в обычном update, сделать отдельно
            DataWrapper<LeadDto> newDataWrapper = _repo.AddOrUpdateLead(_mapper.Map<LeadDto>(leadModel));
            _logger.LogInformation($"Update lead info with Id: {newDataWrapper.Data.Id}");
            return MakeResponse(newDataWrapper, _mapper.Map<LeadOutputModel>);
        }

        /// <summary>
        /// deletes the lead by Id
        /// </summary>
        /// <param name="leadId"></param>        
        //[Authorize()]              // сделать токен вечным!!!!!!!!, а потом после тестирования обычным
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{leadId}")]
        public ActionResult DeleteLeadById(long leadId)    // тесты есть 
        {
            DataWrapper<LeadDto> dataWrapper = _repo.GetById(leadId);     //зачем??? мы выбираем лида и его удаляем, то есть айди всегда есть
            if (dataWrapper.Data == null) return BadRequest("Lead was not found");
            _repo.Delete(leadId);
            _logger.LogInformation($"Delete lead with Id: {dataWrapper.Data.Id}");
            return Ok("Successfully deleted");
        }

        /// <summary>
        /// updates password for lead
        /// </summary>
        /// <param name="passwordModel"></param>       
        //[Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut("change-password")]
        public ActionResult UpdatePassword(PasswordInputModel passwordModel)
        {
            if (!passwordModel.Id.HasValue)
            {
                return BadRequest("ID is empty");
            }
            var leadId = _repo.GetById(passwordModel.Id.Value);
            if (leadId == null) return BadRequest("Lead was not found");
            LeadValidator validation = new LeadValidator(_repo);
            string check = validation.ValidatePasswordInputModel(passwordModel);
            if (!string.IsNullOrWhiteSpace(check)) return BadRequest(check);
            passwordModel.Password = new PasswordEncryptor().EncryptPassword(passwordModel.Password);
            _repo.UpdatePassword(_mapper.Map<PasswordDto>(passwordModel));
            _logger.LogInformation($"Update password for lead with Id: {passwordModel.Id}");
            return Ok("Successfully updated");
        }

        /// <summary>
        /// edits lead's email by leadId 
        /// </summary>
        /// <param name="emailModel"></param>        
        //[Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("email")]
        public ActionResult<string> UpdateEmailByLeadId(EmailInputModel emailModel) //тесты есть
        {
            if (string.IsNullOrWhiteSpace(emailModel.Email))
            {
                return BadRequest("Enter the email");
            }
            if ((!Regex.IsMatch(emailModel.Email, LeadValidator.emailPattern))) return BadRequest("The Email is incorrect");
            if (emailModel.Id != null)
            {
                var leadId = _repo.GetById(emailModel.Id.Value);
                if (leadId == null) return BadRequest("Lead was not found");
                DataWrapper<int> dataWrapper = _repo.CheckEmail(emailModel.Email);
                if (dataWrapper.Data != 0) return BadRequest("User with this email already exists");
                _logger.LogInformation($"Update e-mail for lead with Id: {leadId} - {emailModel.Email} ");
                return Ok("E-mail was updated");
            }
            return BadRequest("The Email is incorrect");
        }

        /// <summary>
        /// searches leads by different parameters
        /// </summary>
        /// <param name="searchparameters"></param>        
        //[Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("search")]
        public ActionResult<List<LeadOutputModel>> SearchLead(SearchParametersInputModel searchparameters)  // тесты есть
        {
            DataWrapper<List<LeadDto>> dataWrapper = _repo.SearchLeads(_mapper.Map<LeadSearchParameters>(searchparameters));
            return MakeResponse(dataWrapper, _mapper.Map<List<LeadOutputModel>>);
        }

        /// <summary>
        /// gets the account by Id with all information
        /// </summary>
        /// <param name="id"></param>       
        //[Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("account/{Id}")]
        public ActionResult<AccountWithLeadOutputModel> GetAccountById(long id) // переделать в OutputModel с AccId, LeadId, CurrId, Balance 
        {
            DataWrapper<AccountWithLeadDto> dataWrapper = _repo.GetAccountById(id);
            return MakeResponse(dataWrapper, _mapper.Map<AccountWithLeadOutputModel>);
        }

        /// <summary>
        /// GetAccountsByLeadId
        /// </summary>
        /// <param name="leadId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{leadId}/accounts")]
        public ActionResult<List<AccountOutputModel>> GetAccountsByLeadId(long leadId)  //другую outputmodel вставить, просто список акк
        {
            DataWrapper<List<AccountDto>> dataWrapper = _repo.GetAccountByLeadId(leadId);
            return MakeResponse(dataWrapper, _mapper.Map<List<AccountOutputModel>>);
        }

        /// <summary>
        /// CreateAccount
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("account")]

        public ActionResult<AccountWithLeadOutputModel> CreateAccount(AccountInputModel account)
        {
            if (account.CurrencyId == null) return BadRequest("Choose currency");
            DataWrapper<AccountWithLeadDto> dataWrapper = _repo.AddOrUpdateAccount(_mapper.Map<AccountDto>(account));
            _logger.LogInformation($"Create new account with Id: {dataWrapper.Data.AccountId}");
            return MakeResponse(dataWrapper, _mapper.Map<AccountWithLeadOutputModel>);
        }

        /// <summary>
        /// Update account
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("account")]
        public ActionResult<AccountWithLeadOutputModel> UpdateAccount(AccountInputModel account)
        {
            DataWrapper<AccountWithLeadDto> dataWrapper = _repo.AddOrUpdateAccount(_mapper.Map<AccountDto>(account));
            _logger.LogInformation($"Update account with Id: {dataWrapper.Data.AccountId}");
            return MakeResponse(dataWrapper, _mapper.Map<AccountWithLeadOutputModel>);
        }
        
        private delegate T DtoConverter<T, K>(K dto);

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
