using CRM.Core;
using CRM.Data.DTO;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CRM.Data
{
    public class OperatinRepository
    {
        private readonly IDbConnection _connection;

        public OperatinRepository(IOptions<DatabaseOptions> options)
        {
            _connection = new SqlConnection(options.Value.DBConnectionString);
        }

        public DataWrapper<long> AddOperation(OperationDto operation)
        {
            var result = new DataWrapper<long>();
            try
            {
                result.Data = _connection.Query<long>(StoredProcedures.AddOperation, operation, commandType: CommandType.StoredProcedure).FirstOrDefault();
                result.IsOk = true;
            }
            catch (Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
        }

        public DataWrapper<OperationDto> GetOperationById(long Id)
        {
            var result = new DataWrapper<OperationDto>();
            try
            {
                result.Data = _connection.Query<OperationDto>(StoredProcedures.GetOperationById,  new { Id }, commandType: CommandType.StoredProcedure).FirstOrDefault();
                result.IsOk = true;
            }
            catch (Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
        }

        public void CompletedOperation(long id)
        {
            _connection.Execute(StoredProcedures.CompletedOperation, new { id }, commandType: CommandType.StoredProcedure);           
        }
    }
}
