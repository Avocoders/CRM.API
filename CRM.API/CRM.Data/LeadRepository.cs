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
            return connection.Query<int>(sqlExpression, leadDto).FirstOrDefault();
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
                string sqlExpression = "Lead_GetAll";
                return connection.Query<LeadDto>(sqlExpression, commandType: CommandType.StoredProcedure).ToList();
            }
        }

        public LeadDto GetById(long leadId)
        {
            using IDbConnection connection = Connection.GetConnection();
            {
                string sqlExpression = "Lead_GetById";
                return connection.Query<LeadDto>(sqlExpression, new { leadId }, commandType: CommandType.StoredProcedure).FirstOrDefault();
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
                return connection.Query<LeadDto>(sqlExpression, leadDto).FirstOrDefault();
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
