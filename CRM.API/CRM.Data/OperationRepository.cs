using CRM.Core;
using CRM.Data.DTO;
using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace CRM.Data
{
    public class OperationRepository : IOperationRepository
    {
        private readonly IDbConnection _connection;

        public OperationRepository(IOptions<DatabaseOptions> options)
        {
            _connection = new SqlConnection(options.Value.DBConnectionString);
        }

        public async ValueTask<DataWrapper<long>> AddOperation(OperationDto operation)
        {
            var result = new DataWrapper<long>();
            try
            {
                var tmp = await _connection.QueryAsync<long>(StoredProcedures.AddOperation,
                new
                {
                    operation.AccountId,
                    operation.Amount
                },
                commandType: CommandType.StoredProcedure);
                result.Data = tmp.FirstOrDefault();
                result.IsOk = true;
            }
            catch (Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
        }

        public async ValueTask<DataWrapper<OperationDto>> GetOperationById(long Id)
        {
            var result = new DataWrapper<OperationDto>();
            try
            {
                var tmp = await _connection.QueryAsync<OperationDto>(StoredProcedures.GetOperationById, new { Id }, commandType: CommandType.StoredProcedure);
                result.Data = tmp.FirstOrDefault();
                result.IsOk = true;
            }
            catch (Exception e)
            {
                result.ExceptionMessage = e.Message;
            }
            return result;
        }

        public async ValueTask CompletedOperation(long id)
        {
            var tmp = await _connection.ExecuteAsync(StoredProcedures.CompletedOperation, new { id }, commandType: CommandType.StoredProcedure);
        }
    }
}
