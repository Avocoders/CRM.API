using CRM.API.Models.Output;
using System;
using System.Collections.Generic;
using System.Text;
using TransactionStore.API.Models.Input;

namespace CRM.NUnitTest
{
   public class TransactionMock
    {
        public TransactionInputModel DepositMock(int num)
        {
            switch (num)
            {
                case 1:

                    return  new TransactionInputModel()
                    {
                        AccountId = 1,
                        Amount = 80500
                    };
                case 2:

                    return new TransactionInputModel()
                    {
                        AccountId = 2,
                        Amount = 500
                    };
                case 3:

                    return new TransactionInputModel()
                    {
                        AccountId = 3,                        
                        Amount = 10500
                    };
                case 4:

                    return new TransactionInputModel()
                    {
                        AccountId = 11,
                        Amount = 1500
                    };
                case 5:

                    return new TransactionInputModel()
                    {
                        AccountId = 12,
                        Amount = 18000
                    };
                   
            }
            return new TransactionInputModel();
        }

        public TransactionOutputModel DepositOutputMock(int num)
        {
            switch (num)
            {
                case 1:
                    {
                        return new TransactionOutputModel()
                        {
                            Id = 1,
                            AccountId = 1,
                            Type = "Deposit",
                            Amount = 80500.0000M,
                            Timestamp = "11.08.2020 23:32:04",
                            AccountIdReceiver = null
                        };
                    }
                case 2:
                    {
                        return new TransactionOutputModel()
                        {
                            Id = 2,
                            AccountId = 2,
                            Type = "Deposit",
                            Amount = 500.0000M,
                            Timestamp = "11.08.2020 23:11:04",
                            AccountIdReceiver = null
                        };
                    }
                case 3:
                    {
                        return new TransactionOutputModel()
                        {
                            Id = 3,
                            AccountId = 3,
                            Type = "Deposit",
                            Amount = 10500.0000M,
                            Timestamp = "11.08.2020 23:11:04",
                            AccountIdReceiver = null
                        };
                    }
                case 4:
                    {
                        return new TransactionOutputModel()
                        {
                            Id = 4,
                            AccountId = 11,
                            Type = "Deposit",
                            Amount = 1500.0000M,
                            Timestamp = "11.08.2020 23:11:04",
                            AccountIdReceiver = null
                        };
                    }
                case 5:
                    {
                        return new TransactionOutputModel()
                        {
                            Id = 5,
                            AccountId = 12,
                            Type = "Deposit",
                            Amount = 15000.0000M,
                            Timestamp = "11.08.2020 23:11:04",
                            AccountIdReceiver = null
                        };
                    }
            }
            return new TransactionOutputModel();
        }
    }
}
    
