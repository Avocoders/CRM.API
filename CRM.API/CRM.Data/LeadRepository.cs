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

        public int Add(LeadDto leadDto)
        {            
            string sqlExpression = "Lead_Add @firstName, @lastName, @patronymic, @login, @password, @phone, @email, @cityId, @address, @birthDate";
            return _connection.Query<int>(sqlExpression, new
            {
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

            }).FirstOrDefault();
        }

        public void Delete(long id)
        {            
            string sqlExpression = "Lead_Delete";
            _connection.Execute(sqlExpression, new { id }, commandType: CommandType.StoredProcedure);
        }

        public List<LeadDto> GetAll()
        { 
            using IDbConnection connection = Connection.GetConnection();
            {
                var leads = new List<LeadDto>();


                connection.Query<LeadDto, RoleDto, CityDto, LeadDto>(
                    "Lead_GetAll1",
                    (lead, role, city) =>
                    {
                        LeadDto leadEntry;

                        leadEntry = lead;
                        leadEntry.Role = role;
                        leadEntry.City = city;
                        leads.Add(leadEntry);

                        return leadEntry;
                    },
                    splitOn: "Id")
                .ToList();

                return leads;
            }

        }

        public LeadDto GetById(long leadId)
        {

            using IDbConnection connection = Connection.GetConnection();
            {

                var lead = connection.Query<LeadDto, RoleDto, CityDto, LeadDto>(
                    "Lead_GetById1",
                    (lead, role, city) =>
                    {
                        LeadDto leadEntry;

                        leadEntry = lead;
                        leadEntry.Role = role;
                        leadEntry.City = city;

                        return leadEntry;
                    },
                    new {leadId},
                    splitOn: "Id",
                    commandType: CommandType.StoredProcedure)
                .FirstOrDefault();

                return lead;
            }
        }
        public LeadDto GetByLogin(string login)
        {
            string sqlExpression = "Lead_GetByLogin";
            return _connection.Query<LeadDto>(sqlExpression, new { login }, commandType: CommandType.StoredProcedure).FirstOrDefault();            
        }

        public LeadDto Update(LeadDto leadDto)
        {
            using IDbConnection connection = Connection.GetConnection();
            {
                string sqlExpression = "Lead_Update  @id, @firstName, @lastName, @patronymic, @password, @phone, @cityId, @address, @birthDate";
                return connection.Query<LeadDto>(sqlExpression, new
                {
                    leadDto.Id,
                    leadDto.FirstName,
                    leadDto.LastName,
                    leadDto.Patronymic,
                    leadDto.Password,
                    leadDto.Phone,
                    CityId = leadDto.City.Id,
                    leadDto.Address,
                    leadDto.BirthDate

                }).FirstOrDefault();
            }
        }

        public int FindLeadByLogin(string login)
        {            
            string sqlExpression = "Lead_FindByLogin @login";
            return _connection.Query<int>(sqlExpression, new { login }).FirstOrDefault();
        }

        public int FindLeadByEmail(string email)
        {            
            string sqlExpression = "Lead_FindByEmail @email";
            return _connection.Query<int>(sqlExpression, new { email }).FirstOrDefault();
        }

        public string UpdateEmailByLeadId(long? id, string email)
        {            
            string sqlExpression = "Lead_UpdateEmail";
            return _connection.Query<string>(sqlExpression, new { id, email }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }
    }
}
