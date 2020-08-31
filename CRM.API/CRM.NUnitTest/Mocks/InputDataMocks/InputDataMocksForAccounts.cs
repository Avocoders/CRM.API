using CRM.API.Models.Input;

namespace CRM.NUnitTest
{
    public class InputDataMocksForAccounts
    {
        public AccountInputModel GetAccountInputModelMock(int num)    
        {
            return num switch
            {
                7 => new AccountInputModel()
                {
                    Id = 7,
                    LeadId = 3,
                    CurrencyId = 2

                },
                8 => new AccountInputModel()
                {
                    Id = 8,
                    LeadId = 4,
                    CurrencyId = 1

                },
                9 => new AccountInputModel()
                {
                    Id = 9,
                    LeadId = 5,
                    CurrencyId = 3

                },
                10 => new AccountInputModel()
                {
                    Id = 10,
                    LeadId = 6,
                    CurrencyId = 3

                },
                11 => new AccountInputModel()
                {
                    Id = 11,
                    LeadId = 6,
                    CurrencyId = 4

                },
                12 => new AccountInputModel()
                {
                    Id = 12,
                    LeadId = 6,
                    CurrencyId = 3

                },
                18 => new AccountInputModel()
                {
                    LeadId = 10,
                    CurrencyId = 2
                },
                19 => new AccountInputModel()
                {
                    LeadId = 2,
                    CurrencyId = 4
                },
                20 => new AccountInputModel()
                {
                    LeadId = 5,
                    CurrencyId = 1
                },
                21 => new AccountInputModel()
                {
                    LeadId = 1,
                    CurrencyId = 3
                },
                22 => new AccountInputModel()
                {
                    LeadId = 6,
                    CurrencyId = 2
                },
                23 => new AccountInputModel()
                {
                    LeadId = 4,
                    CurrencyId = null
                },                
                _ => new AccountInputModel(),
            };
        }        
    }
}