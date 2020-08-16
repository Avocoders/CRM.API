namespace CRM.Core
{
    public enum CurrenciesCode
    {
        RUB = 1,
        USD,
        EUR,
        JPY
    }
    public enum CurrenciesName
    {
        RussianRuble = 1,
        USDollar,
        Euro,
        Yen
    }

    public enum SearchMode
    {
        ExactValue = 1,
        ContainsValue,
        StartWithValue
    }

    public enum TransactionType
    {
        Deposit = 1,
        Withdraw,
        Transfer
    }

}
