namespace CRM.API.Models.Output
{
    public class AccountOutputModel
    {
		public long Id { get; set; }
		public bool IsDeleted { get; set; }		
		public string CurrencyCode { get; set; }
		public string CurrencyName { get; set; }

        public override bool Equals(object obj)
        {
            AccountOutputModel lom = (AccountOutputModel)obj;
            if (Id == lom.Id &&
                IsDeleted == lom.IsDeleted &&
                CurrencyCode == lom.CurrencyCode &&
                CurrencyName == lom.CurrencyName)
                return true;

            return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
