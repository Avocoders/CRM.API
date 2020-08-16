namespace CRM.Core
{
    public interface IUrlOptions
    {
        string CrmAPIUrl { get; set; }
        string TransactionStoreAPIUrl { get; set; }
    }
}