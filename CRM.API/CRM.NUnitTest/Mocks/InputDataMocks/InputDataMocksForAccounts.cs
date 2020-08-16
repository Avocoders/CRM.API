using CRM.API.Models.Input;

namespace CRM.NUnitTest
{
    public class InputDataMocksForAccounts
    {
        public AccountInputModel GetAccountInputModelMock(int num)    //done
        {
            return num switch
            {
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

        public AccountInputModel UpdateAccountMock(int num)
        {
            switch (num)
            {
                case 1:
                    return new AccountInputModel()
                    {
                        Id = 8,
                        LeadId = 8,
                        CurrencyId = 2

                    }; break;

                case 2:
                    return new AccountInputModel()
                    {
                        Id = 11,
                        LeadId = 2,
                        CurrencyId = 1

                    }; break;
                case 3:

                    return new AccountInputModel()
                    {
                        Id = 88,
                        LeadId = 10,
                        CurrencyId = 1

                    }; break;

                case 4:

                    return new AccountInputModel()
                    {
                        Id = 9,
                        LeadId = 375,
                        CurrencyId = 3

                    }; break;
            }
            return new AccountInputModel();
        }
    }
}