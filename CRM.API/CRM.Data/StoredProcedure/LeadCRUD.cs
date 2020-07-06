using CRM.Data.DTO;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CRM.Data.StoredProcedure
{
    public class LeadCRUD
    {
        public int Add(LeadDTO leadDTO)
        {
            var connection = Connection.GetConnection();
            connection.Open();
            string sqlExpression = "Lead_Add @roleId, @firstName, @lastName, @patronymic, @login, @password, @phone, @email, @cityId, @address, @birthDate, @registrationDate, @changeDate";
            return connection.Query<int>(sqlExpression, leadDTO).FirstOrDefault();
        }

        public int Delete(int id)
        {
            var connection = Connection.GetConnection();
            string sqlExpression = "Lead_Delete";
            connection.Execute(sqlExpression, new { id }, commandType: CommandType.StoredProcedure);
            return id;
        }
    }
}
