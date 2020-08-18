namespace CRM.Core
{
    public class UrlOptions : IUrlOptions
    {
        public string CrmAPIUrl { get; set; }
        public string TransactionStoreAPIUrl { get; set; }
    }
}
