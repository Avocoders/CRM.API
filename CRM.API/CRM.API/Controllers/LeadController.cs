using System.Collections.Generic;
using CRM.API.Models.Input;
using CRM.API.Models.Output;
using CRM.API.Sha256;
using CRM.Data.DTO;
using CRM.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using NLog;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeadController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ILeadRepository _repo;
        private readonly ResponseWrapper _wrapper;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public LeadController(ILeadRepository repo, IMapper mapper, ResponseWrapper wrapper)
        {
            _mapper = mapper;
            _repo = repo;
            _wrapper = wrapper;
        }

        /// <summary>
        /// Gets the lead by Id with all information
        /// </summary>
        /// <param name="leadId"></param>       
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{leadId}")]
        public async ValueTask<ActionResult<LeadOutputModel>> GetLeadById(long leadId)
        {
            DataWrapper<LeadDto> dataWrapper = await _repo.GetById(leadId);
            _logger.Info($"Get info about lead with Id: {leadId}");
            return MakeResponse(dataWrapper, _mapper.Map<LeadOutputModel>);
        }

        /// <summary>
        /// Creates a new lead
        /// </summary>
        /// <param name="leadModel"></param>       
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public async ValueTask <ActionResult<LeadOutputModel>> CreateLead(LeadInputModel leadModel)
        {
            var message = await _wrapper.CreateLeadRW(leadModel);
            if (string.IsNullOrWhiteSpace(message))
            {
                leadModel.Password = new PasswordEncryptor().EncryptPassword(leadModel.Password);  
                DataWrapper<LeadDto> newDataWrapper = await _repo.AddOrUpdateLead(_mapper.Map<LeadDto>(leadModel));
                _logger.Info($"Create new lead with Id: {newDataWrapper.Data.Id}");
                return MakeResponse(newDataWrapper, _mapper.Map<LeadOutputModel>);
            }
            else
                return BadRequest(message);
        }

        /// <summary>
        /// Edits lead's information by leadId
        /// </summary>
        /// <param name="leadModel"></param>        
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPut]
        public async ValueTask<ActionResult<LeadOutputModel>> UpdateLead(LeadInputModel leadModel)
        {
            var message = await _wrapper.UpdateLeadRW(leadModel);
            if (string.IsNullOrWhiteSpace(message))
            {
                leadModel.Password = new PasswordEncryptor().EncryptPassword(leadModel.Password);
                DataWrapper<LeadDto> newDataWrapper = await _repo.AddOrUpdateLead(_mapper.Map<LeadDto>(leadModel));
                _logger.Info($"Update lead info with Id: {newDataWrapper.Data.Id}");
                return MakeResponse(newDataWrapper, _mapper.Map<LeadOutputModel>);
            }
            else
                return BadRequest(message);
        }

        /// <summary>
        /// Deletes the lead by Id
        /// </summary>
        /// <param name="leadId"></param>        
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpDelete("{leadId}")]
        public async ValueTask<ActionResult> DeleteLeadById(long leadId)
        {
            DataWrapper<LeadDto> dataWrapper = await _repo.GetById(leadId);
            if (dataWrapper.Data == null) return BadRequest("Lead was not found");
            await _repo.Delete(leadId);
            _logger.Info($"Delete lead with Id: {dataWrapper.Data.Id}");
            return Ok("Successfully deleted");
        }

        /// <summary>
        /// Updates password for lead
        /// </summary>
        /// <param name="passwordModel"></param>       
        [Authorize()]
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
                _logger.Info($"Update password for lead with Id: {passwordModel.Id}");
                return Ok("Successfully updated");
            }
            else
                return BadRequest(message);
        }

        /// <summary>
        /// Edits lead's email by leadId 
        /// </summary>
        /// <param name="emailModel"></param>        
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("email")]
        public async ValueTask<ActionResult<string>> UpdateEmailByLeadId(EmailInputModel emailModel)
        {
            var message = await _wrapper.UpdateEmailRW(emailModel);
            if (string.IsNullOrWhiteSpace(message))
            {
                await _repo.UpdateEmailByLeadId(_mapper.Map<EmailDto>(emailModel));
                _logger.Info($"Update e-mail for lead with Id: {emailModel.LeadId} - {emailModel.Email} ");
                return Ok("E-mail was updated");
            }
            else
                return BadRequest(message);
        }

        /// <summary>
        /// Searches leads by different parameters
        /// </summary>
        /// <param name="searchparameters"></param>        
        [Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]        
        [HttpPost("search")]
        public async ValueTask<ActionResult<List<LeadOutputModel>>> SearchLead(SearchParametersInputModel searchparameters)
        {
            DataWrapper<List<LeadDto>> dataWrapper = await _repo.SearchLeads(_mapper.Map<LeadSearchParameters>(searchparameters));
            return MakeResponse(dataWrapper, _mapper.Map<List<LeadOutputModel>>);
        }

        /// <summary>
        /// Gets the account by Id with all information
        /// </summary>
        /// <param name="id"></param>  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("account/{Id}")]
        public async ValueTask<ActionResult<AccountWithLeadOutputModel>> GetAccountById(long id)  
        {
            DataWrapper<AccountDto> dataWrapper = await _repo.GetAccountById(id);
            return MakeResponse(dataWrapper, _mapper.Map<AccountWithLeadOutputModel>);
        }

        /// <summary>
        /// Gets the account by LeadId with all information
        ///withdrawing invoices by id lead
        /// </summary>
        /// <param name="leadId"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{leadId}/accounts")]
        public async ValueTask<ActionResult<List<AccountOutputModel>>> GetAccountsByLeadId(long leadId)
        {
            DataWrapper<List<AccountDto>> dataWrapper = await _repo.GetAccountsByLeadId(leadId);
            return MakeResponse(dataWrapper, _mapper.Map<List<AccountOutputModel>>);
        }

        /// <summary>
        /// Creates new account
        ///creating an invoice for lead in the required currency
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost("account")]
        public async ValueTask<ActionResult<AccountWithLeadOutputModel>> CreateAccount(AccountInputModel account)
        {
            if (account.CurrencyId == null) return BadRequest("Choose currency");
            DataWrapper<AccountDto> dataWrapper = await _repo.AddOrUpdateAccount(_mapper.Map<AccountDto>(account));
            _logger.Info($"Create new account with Id: {dataWrapper.Data.Id}");
            return MakeResponse(dataWrapper, _mapper.Map<AccountWithLeadOutputModel>);
        }

        /// <summary>
        /// Changes account's currency
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("account")]
        public async ValueTask<ActionResult<AccountWithLeadOutputModel>> UpdateAccount(AccountInputModel account)
        {
            DataWrapper<AccountDto> dataWrapper = await _repo.AddOrUpdateAccount(_mapper.Map<AccountDto>(account));
            _logger.Info($"Update account with Id: {dataWrapper.Data.Id}");
            return MakeResponse(dataWrapper, _mapper.Map<AccountWithLeadOutputModel>);
        }

        private delegate T DtoConverter<T, K>(K dto);

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
