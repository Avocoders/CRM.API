using CRM.Data.DTO;
using System.Threading.Tasks;

namespace CRM.Data
{
    public interface IOperationRepository
    {
        ValueTask<DataWrapper<long>> AddOperation(OperationDto operation);
        ValueTask CompletedOperation(long id);
        ValueTask<DataWrapper<OperationDto>> GetOperationById(long Id);
    }
}