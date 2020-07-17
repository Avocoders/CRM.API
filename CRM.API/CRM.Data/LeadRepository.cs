using CRM.Data.DTO;
using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace CRM.Data
{
    public class LeadRepository
    {
        private readonly IDbConnection _connection;

        public LeadRepository()
        {
            _connection = Connection.GetConnection();
        }

        public LeadDto Add(LeadDto leadDto)
        {
            return _connection.Query<LeadDto, RoleDto, CityDto, LeadDto>("Lead_Add_Or_Update",
                (lead, role, city) =>
                {
                    LeadDto leadEntry;

                    leadEntry = lead;
                    leadEntry.Role = role;
                    leadEntry.City = city;

                    return leadEntry;
                },
                new
                {
                    leadDto.Id,
                leadDto.FirstName,
                leadDto.LastName,
                leadDto.Patronymic,
                leadDto.Login,
                leadDto.Password,
                leadDto.Phone,
                leadDto.Email,
                CityId = leadDto.City.Id,
                leadDto.Address,
                leadDto.BirthDate

            }, splitOn: "Id",
                commandType: CommandType.StoredProcedure).FirstOrDefault();

        }

        public void Delete(long id)
        {
            _connection.Execute("Lead_Delete", new { id }, commandType: CommandType.StoredProcedure);
        }

        public List<LeadDto> GetAll()
        {
            var leads = new List<LeadDto>();

            _connection.Query<LeadDto, RoleDto, CityDto, LeadDto>(
                "Lead_GetAll",
                (lead, role, city) =>
                {
                    LeadDto leadEntry;

                    leadEntry = lead;
                    leadEntry.Role = role;
                    leadEntry.City = city;
                    leads.Add(leadEntry);

                    return leadEntry;
                },
                splitOn: "Id").ToList();
            return leads;
        }

        public LeadDto GetById(long leadId)
        {
            return _connection.Query<LeadDto, RoleDto, CityDto, LeadDto>(
                "Lead_GetById",
                (lead, role, city) =>
                {
                    LeadDto leadEntry;

                    leadEntry = lead;
                    leadEntry.Role = role;
                    leadEntry.City = city;

                    return leadEntry;
                },
                new { leadId },
                splitOn: "Id",
                commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public LeadDto GetByLogin(string login)
        {
            return _connection.Query<LeadDto, RoleDto, CityDto, LeadDto>(
                "Lead_GetByLogin",
                (lead, role, city) =>
                {
                    LeadDto leadEntry;

                    leadEntry = lead;
                    leadEntry.Role = role;
                    leadEntry.City = city;
                    return leadEntry;
                },
                new { login },
                splitOn: "Id",
                commandType: CommandType.StoredProcedure).FirstOrDefault();
            //return _connection.Query<LeadDto>("Lead_GetByLogin", new { login }, commandType: CommandType.StoredProcedure).FirstOrDefault();            
        }

        public LeadDto Update(LeadDto leadDto)
        {
            return _connection.Query<LeadDto, RoleDto, CityDto, LeadDto>("Lead_Add_Or_Update",
                (lead, role, city) =>
                {
                    LeadDto leadEntry;

                    leadEntry = lead;
                    leadEntry.Role = role;
                    leadEntry.City = city;
                    
                    return leadEntry;
                },
                new
                {
                    leadDto.Id,
                    leadDto.FirstName,
                    leadDto.LastName,
                    leadDto.Patronymic,
                    leadDto.Login,
                    leadDto.Password,
                    leadDto.Phone,
                    leadDto.Email,  
                    CityId = leadDto.City.Id,
                    leadDto.Address,
                    leadDto.BirthDate
                },
                splitOn: "Id",
                commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public int FindLeadByLogin(string login)
        {
            return _connection.Query<int>("Lead_FindByLogin", new { login }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public int FindLeadByEmail(string email)
        {
            return _connection.Query<int>("Lead_FindByEmail", new { email }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public string UpdateEmailByLeadId(long? id, string email)
        {
            return _connection.Query<string>("Lead_UpdateEmail", new { id, email }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }

        public List<LeadDto> SearchLeads(LeadSearchParameters searchParameters)
        {
            return _connection.Query<LeadDto, RoleDto, CityDto, LeadDto>("Lead_Search", 
                (lead, role, city) =>
                 {
                    LeadDto leadEntry;

                    leadEntry = lead;
                    leadEntry.Role = role;
                    leadEntry.City = city;
                    return leadEntry;
                },
                searchParameters, splitOn: "Id",
                commandType: CommandType.StoredProcedure).ToList();           
        }
    }
}
