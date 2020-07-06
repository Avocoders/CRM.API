using CRM.Data.DTO;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CRM.Data.StoredProcedure
{
    public class CityCRUD
    {
        public int Add(CityDTO cityDTO)
        {
            var connection = Connection.GetConnection();
            connection.Open();
            string sqlExpression = "City_Add @name"; 
            return connection.Query<int>(sqlExpression, cityDTO).FirstOrDefault();
        }
    }
}
