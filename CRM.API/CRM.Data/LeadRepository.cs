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
            return _connection.Query<int>(sqlExpression, leadDto).FirstOrDefault();
        }

        public void Delete(long id)
        {            
            string sqlExpression = "Lead_Delete";
            _connection.Execute(sqlExpression, new { id }, commandType: CommandType.StoredProcedure);
        }

        public List<LeadDto> GetAll()
        {
            string sqlExpression = "Lead_GetAll";
            return _connection.Query<LeadDto>(sqlExpression, commandType: CommandType.StoredProcedure).ToList();            
        }

        public LeadDto GetById(long leadId)
        {
            string sqlExpression = "Lead_GetById";
            return _connection.Query<LeadDto>(sqlExpression, new { leadId }, commandType: CommandType.StoredProcedure).FirstOrDefault();            
        }
        public LeadDto GetByLogin(string login)
        {
            string sqlExpression = "Lead_GetByLogin";
            return _connection.Query<LeadDto>(sqlExpression, new { login }, commandType: CommandType.StoredProcedure).FirstOrDefault();            
        }

        public LeadDto Update(LeadDto leadDto)
        {
            string sqlExpression = "Lead_Update  @id, @firstName, @lastName, @patronymic, @password, @phone, @cityId, @address, @birthDate";
            return _connection.Query<LeadDto>(sqlExpression, leadDto).FirstOrDefault();            
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
