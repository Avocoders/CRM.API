using CRM.Data.DTO;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRM.Data
{
    public class LeadRepository
    {
        public int Add(LeadDto leadDto)
        {
            var connection = Connection.GetConnection();
            connection.Open();
            string sqlExpression = "Lead_Add @firstName, @lastName, @patronymic, @login, @password, @phone, @email, @cityId, @address, @birthDate";
            return connection.Query<int>(sqlExpression, new
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
            var connection = Connection.GetConnection();
            string sqlExpression = "Lead_Delete";
            connection.Execute(sqlExpression, new { id }, commandType: CommandType.StoredProcedure);
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
            using IDbConnection connection = Connection.GetConnection();
            {
                string sqlExpression = "Lead_GetByLogin";
                return connection.Query<LeadDto>(sqlExpression, new { login }, commandType: CommandType.StoredProcedure).FirstOrDefault();
            }
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
            var connection = Connection.GetConnection();
            connection.Open();
            string sqlExpression = "Lead_FindByLogin @login";
            return connection.Query<int>(sqlExpression, new { login }).FirstOrDefault();
        }

        public int FindLeadByEmail(string email)
        {
            var connection = Connection.GetConnection();
            connection.Open();
            string sqlExpression = "Lead_FindByEmail @email";
            return connection.Query<int>(sqlExpression, new { email }).FirstOrDefault();
        }

        public string UpdateEmailByLeadId(long? id, string email)
        {
            var connection = Connection.GetConnection();
            connection.Open();
            string sqlExpression = "Lead_UpdateEmail";
            return connection.Query<string>(sqlExpression, new { id, email }, commandType: CommandType.StoredProcedure).FirstOrDefault();
        }
    }
}
