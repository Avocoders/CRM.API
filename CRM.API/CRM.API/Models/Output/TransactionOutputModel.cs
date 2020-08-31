namespace CRM.API.Models.Output
{
    public class TransactionOutputModel
    {
        public long Id { get; set; }
        public long AccountId { get; set; }
        public string Type { get; set; }
        public decimal Amount { get; set; }
        public string Timestamp { get; set; }
        public long? AccountIdReceiver { get; set; }

        public override bool Equals(object obj)
        {
            TransactionOutputModel tom = (TransactionOutputModel)obj;
            if (Id == tom.Id &&
                AccountId == tom.AccountId &&
                Type == tom.Type &&
                Amount == tom.Amount &&
                AccountIdReceiver == tom.AccountIdReceiver)
                return true;
            else
                return false;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
