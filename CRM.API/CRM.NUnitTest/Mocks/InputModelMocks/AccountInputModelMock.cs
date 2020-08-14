using CRM.API.Models.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace CRM.NUnitTest
{
    public class AccountInputModelMock
    {
        public AccountInputModel AddAccountMock(int num)
        {
            switch (num)
            {
                case 1:
                    return new AccountInputModel()
                    {

                        LeadId = 10,
                        CurrencyId = 2

                    }; break;

                case 2:
                    return new AccountInputModel()
                    {

                        LeadId = 2,
                        CurrencyId = 4

                    }; break;
                case 3:

                    return new AccountInputModel()
                    {

                        LeadId = 88,
                        CurrencyId = 1

                    }; break;

                case 4:

                    return new AccountInputModel()
                    {

                        LeadId = 0,
                        CurrencyId = 1

                    }; break;
            }
            return new AccountInputModel();
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