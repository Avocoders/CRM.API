namespace CRM.API.Models.Output
{
    public class TransferOutputModel : TransactionOutputModel
    {
        public long LeadIdReceiver { get; set; }
    }
}
