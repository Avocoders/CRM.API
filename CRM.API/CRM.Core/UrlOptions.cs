namespace CRM.Core
{
    public class APIOptions : IUrlOptions
    {
        public string CrmAPIUrl { get; set; }
        public string TransactionStoreAPIUrl { get; set; }
    }
}
