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
using AutoMapper;

namespace CRM.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LeadController : Controller
    {
        private readonly ILogger<LeadController> _logger;  // подключить или удалить, Лучше подключить
        private readonly ILeadRepository _repo;
        private readonly IMapper _mapper;

        public LeadController(ILogger<LeadController> logger, ILeadRepository repo, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
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

        private string ValidateLoginInfo(LeadInputModel leadModel) //перенести в папку validators, ту да же репо
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

        /// <summary>
        /// gets the lead by Id with all information
        /// </summary>
        /// <param name="leadId"></param>       
        //[Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{leadId}")]
        public ActionResult<LeadOutputModel> GetLeadById(long leadId) // тесты готовы 
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
            LeadValidator validation = new LeadValidator();
            string validationResult = validation.ValidateLeadInputModel(leadModel);
            if (!string.IsNullOrWhiteSpace(validationResult)) return BadRequest(validationResult);
            string loginValidationResult = ValidateLoginInfo(leadModel);
            if (!string.IsNullOrWhiteSpace(loginValidationResult)) return BadRequest(loginValidationResult);
            leadModel.Password = new PasswordEncryptor().EncryptPassword(leadModel.Password);  //возможно не в контроллере должна быть, в автомап
            DataWrapper<LeadDto> newDataWrapper = _repo.AddOrUpdateLead(_mapper.Map<LeadDto>(leadModel));
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
        public ActionResult<LeadOutputModel> UpdateLead(LeadInputModel leadModel) //усё робiць)))
        {
            if (!leadModel.Id.HasValue)
            {
                return BadRequest("ID is empty");
            }
            var leadId = _repo.GetById(leadModel.Id.Value);
            if (leadId == null) return BadRequest("Lead was not found");
            LeadValidator validation = new LeadValidator();
            string badRequest = validation.ValidateLeadInputModel(leadModel);
            if (!string.IsNullOrWhiteSpace(badRequest)) return BadRequest(badRequest);
            leadModel.Password = new PasswordEncryptor().EncryptPassword(leadModel.Password); // нельзя поменять пароль в обычном update, сделать отдельно
            DataWrapper<LeadDto> newDataWrapper = _repo.AddOrUpdateLead(_mapper.Map<LeadDto>(leadModel));
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
            DataWrapper<LeadDto> dataWrapper = _repo.GetById(leadId);
            if (dataWrapper.Data.Id == null) return BadRequest("Lead was not found");
            _repo.Delete(leadId);
            return Ok("Successfully deleted");
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
            var leadId = _repo.GetById(emailModel.Id.Value);
            if (leadId == null) return BadRequest("Lead was not found");
            DataWrapper<int> dataWrapper = _repo.CheckEmail(emailModel.Email);
            if (dataWrapper.Data != 0) return BadRequest("User with this email already exists");            
            DataWrapper<string> newDataWrapper = _repo.UpdateEmailByLeadId(emailModel.Id, emailModel.Email); //не нужно ничего возвращать
            return MakeResponse(newDataWrapper); //вернуть ОК с сообщением
        }

        /// <summary>
        /// searches leads by different parameters
        /// </summary>
        /// <param name="searchparameters"></param>        
        //[Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("search")]
        public ActionResult<List<LeadOutputModel>> SearchLead(SearchParametersInputModel searchparameters)  // тесты есть
        {
            DataWrapper<List<LeadDto>> dataWrapper = _repo.SearchLeads(_mapper.Map<LeadSearchParameters>(searchparameters));
            return MakeResponse(dataWrapper, _mapper.Map<List<LeadOutputModel>>);
        }

        /// <summary>
        /// gets the account by Id with all information
        /// </summary>
        /// <param name="Id"></param>       
        //[Authorize()]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("account/{Id}")]
        public ActionResult<AccountWithLeadOutputModel> GetAccountById(long Id) //accountWithLeadOutputModel с основной инфой о лиде
        {
            DataWrapper<AccountVsLeadDTO> dataWrapper = _repo.GetAccountById(Id);
            return MakeResponse(dataWrapper, _mapper.Map<AccountWithLeadOutputModel>);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpGet("{leadId}/accounts")]
        public ActionResult<List<AccountOutputModel>> GetAccountsByLeadId(long leadId)  //другую outputmodel вставить, просто список акк
        {
            DataWrapper<List<AccountDto>> dataWrapper = _repo.GetAccountByLeadId(leadId);
            return MakeResponse(dataWrapper, _mapper.Map<List<AccountOutputModel>>);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPost("account")]
        public ActionResult<AccountWithLeadOutputModel> AddAccount(AccountInputModel account)
        {
            DataWrapper<AccountVsLeadDTO> dataWrapper = _repo.AddOrUpdateAccount(_mapper.Map<AccountDto>(account));
            return MakeResponse(dataWrapper, _mapper.Map<AccountWithLeadOutputModel>);
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [HttpPut("account")]
        public ActionResult<AccountWithLeadOutputModel> UpdateAccount(AccountInputModel account)
        {

            DataWrapper<AccountVsLeadDTO> dataWrapper = _repo.AddOrUpdateAccount(_mapper.Map<AccountDto>(account));
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
