using CRM.API.Models.Output;
using TransactionStore.API.Models.Input;

namespace CRM.NUnitTest
{
   public class TransactionInputModelMocks
    {
        public TransactionInputModel GetDepositInputModel(int num)
        {
            return num switch
            {
                1 => new TransactionInputModel()
                {
                    AccountId = 1,
                    Amount = 800000
                },
                2 => new TransactionInputModel()
                {
                    AccountId = 2,
                    Amount = 5000
                },
                3 => new TransactionInputModel()
                {
                    AccountId = 3,
                    Amount = 325000
                },
                4 => new TransactionInputModel()
                {
                    AccountId = 4,
                    Amount = -200000
                },
                5 => new TransactionInputModel()
                {
                    AccountId = 5,
                    Amount = 0
                },
                6 => new TransactionInputModel()
                {
                    AccountId = 546,
                    Amount = 200
                },               
                _ => new TransactionInputModel(),
            };
        }

        public TransferInputModel GetTransferInputModel(int num)
        {
            return num switch
            {
                1 => new TransferInputModel()
                {
                    AccountId = 1,                    
                    Amount = 500000,
                    AccountIdReceiver = 2                    
                },

                2 => new TransferInputModel()
                {
                    AccountId = 1,                    
                    Amount = 300000,
                    AccountIdReceiver = 4                    
                },

                3 => new TransferInputModel()
                {
                    AccountId = 4,                    
                    Amount = 500,
                    AccountIdReceiver = 3                   
                },
                4 => new TransferInputModel()
                {
                    AccountId = 3,
                    Amount = 1000,
                    AccountIdReceiver = 999
                },
                5 => new TransferInputModel()
                {
                    AccountId = 3,
                    Amount = -500,
                    AccountIdReceiver = 1
                },
                6 => new TransferInputModel()
                {
                    AccountId = 1,
                    Amount = 2000,
                    AccountIdReceiver = 2
                },
                _ => new TransferInputModel(),
            };
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
    
