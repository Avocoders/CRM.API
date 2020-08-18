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
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    public class LeadController : Controller
    {
        private readonly ILogger _logger;
        private readonly IMapper _mapper;
        private readonly ILeadRepository _repo;
        private readonly ResponseWrapper _wrapper;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repo"></param>
        /// <param name="mapper"></param>
        /// <param name="validator"></param>
        public LeadController(ILogger<LeadController> logger, ILeadRepository repo, IMapper mapper, ResponseWrapper wrapper)
        {
            _logger = logger;
            _mapper = mapper;
            _repo = repo;
            _wrapper = wrapper;
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
        public ActionResult<LeadOutputModel> CreateLead(LeadInputModel leadModel)
        {
            var message = _wrapper.CreateLeadRW(leadModel);
            if (string.IsNullOrWhiteSpace(message))
            {
                leadModel.Password = new PasswordEncryptor().EncryptPassword(leadModel.Password);  //возможно не в контроллере должна быть, в автомап !!!
                DataWrapper<LeadDto> newDataWrapper = _repo.AddOrUpdateLead(_mapper.Map<LeadDto>(leadModel));
                _logger.LogInformation($"Create new lead with Id: {newDataWrapper.Data.Id}");
                return MakeResponse(newDataWrapper, _mapper.Map<LeadOutputModel>);
            }
            else
                return BadRequest(message);
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
            var message = _wrapper.UpdateLeadRW(leadModel);
            if (string.IsNullOrWhiteSpace(message))
            {
                leadModel.Password = new PasswordEncryptor().EncryptPassword(leadModel.Password); // нельзя поменять пароль в обычном update, сделать отдельно !!!
                DataWrapper<LeadDto> newDataWrapper = _repo.AddOrUpdateLead(_mapper.Map<LeadDto>(leadModel));
                _logger.LogInformation($"Update lead info with Id: {newDataWrapper.Data.Id}");
                return MakeResponse(newDataWrapper, _mapper.Map<LeadOutputModel>);
            }
            else
                return BadRequest(message);
        }

        /// <summary>
        /// deletes the lead by Id
        /// </summary>
        /// <param name="leadId"></param>        
        //[Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{leadId}")]
        public ActionResult DeleteLeadById(long leadId)
        {
            DataWrapper<LeadDto> dataWrapper = _repo.GetById(leadId);
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
            var message = _wrapper.UpdatePasswordRW(passwordModel);
            if (string.IsNullOrWhiteSpace(message))
            {
                passwordModel.Password = new PasswordEncryptor().EncryptPassword(passwordModel.Password);
                _repo.UpdatePassword(_mapper.Map<PasswordDto>(passwordModel));
                _logger.LogInformation($"Update password for lead with Id: {passwordModel.Id}");
                return Ok("Successfully updated");
            }
            else
                return BadRequest(message);
        }

        /// <summary>
        /// edits lead's email by leadId 
        /// </summary>
        /// <param name="emailModel"></param>        
        //[Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("email")]
        public ActionResult<string> UpdateEmailByLeadId(EmailInputModel emailModel)
        {
            var message = _wrapper.UpdateEmailRW(emailModel);
            if (string.IsNullOrWhiteSpace(message))
            {
                _repo.UpdateEmailByLeadId(_mapper.Map<EmailDto>(emailModel));
                _logger.LogInformation($"Update e-mail for lead with Id: {emailModel.LeadId} - {emailModel.Email} ");
                return Ok("E-mail was updated");
            }
            else
                return BadRequest(message);
        }

        /// <summary>
        /// searches leads by different parameters
        /// </summary>
        /// <param name="searchparameters"></param>        
        //[Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("search")]
        public ActionResult<List<LeadOutputModel>> SearchLead(SearchParametersInputModel searchparameters)
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
        /// 
        ///withdrawing invoices by id lead
        /// </summary>
        /// <param name="leadId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{leadId}/accounts")]
        public ActionResult<List<AccountOutputModel>> GetAccountsByLeadId(long leadId)
        {
            DataWrapper<List<AccountDto>> dataWrapper = _repo.GetAccountsByLeadId(leadId);
            return MakeResponse(dataWrapper, _mapper.Map<List<AccountOutputModel>>);
        }
        /// <summary>
        /// 
        ///creating an invoice for lead in the required currency
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("account")]
        public ActionResult<AccountWithLeadOutputModel> CreateAccount(AccountInputModel account)
        {
            if (account.CurrencyId == null) return BadRequest("Choose currency");
            DataWrapper<AccountWithLeadDto> dataWrapper = _repo.AddOrUpdateAccount(_mapper.Map<AccountDto>(account));
            _logger.LogInformation($"Create new account with Id: {dataWrapper.Data.Id}");
            return MakeResponse(dataWrapper, _mapper.Map<AccountWithLeadOutputModel>);
        }
        /// <summary>
        /// change of account currency
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("account")]
        public ActionResult<AccountWithLeadOutputModel> UpdateAccount(AccountInputModel account)
        {
            DataWrapper<AccountWithLeadDto> dataWrapper = _repo.AddOrUpdateAccount(_mapper.Map<AccountDto>(account));
            _logger.LogInformation($"Update account with Id: {dataWrapper.Data.Id}");
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
