using CRM.Data.DTO;

namespace CRM.Data
{
    public interface IOperationRepository
    {
        DataWrapper<long> AddOperation(OperationDto operation);
        void CompletedOperation(long id);
        DataWrapper<OperationDto> GetOperationById(long Id);
    }
}