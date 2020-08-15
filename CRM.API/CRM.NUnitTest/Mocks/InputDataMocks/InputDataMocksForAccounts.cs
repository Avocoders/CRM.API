using CRM.API.Models.Input;

namespace CRM.NUnitTest
{
    public class InputDataMocksForAccounts
    {
        public dynamic GetAccountInputModelMock(int num)
        {
            return num switch
            {
                1 => new AccountInputModel()
                {

                    LeadId = 10,
                    CurrencyId = 2

                },
                2 => new AccountInputModel()
                {

                    LeadId = 2,
                    CurrencyId = 4

                },
                3 => new AccountInputModel()
                {

                    LeadId = 5,
                    CurrencyId = 1

                },
                4 => new AccountInputModel()
                {

                    LeadId = 1,
                    CurrencyId = 3

                },
                5 => new AccountInputModel()
                {

                    LeadId = 6,
                    CurrencyId = 2

                },
                6 => new AccountInputModel()
                {

                    LeadId = 4,
                    CurrencyId = 1

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