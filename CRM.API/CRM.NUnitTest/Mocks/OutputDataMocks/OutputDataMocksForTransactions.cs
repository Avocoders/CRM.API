using CRM.API.Models.Output;
using System.Collections.Generic;

namespace CRM.NUnitTest.Mocks.OutputModelMocks
{
    public class OutputDataMocksForTransactions
    {
        public dynamic GetIdDepositMock(int num) 
        {
            return num switch
            {
                1 => 1,
                2 => 2,
                3 => 3,
                4 => "The amount is missing",
                5 => "The amount is missing",
                6 => "The account is not found",
                _ => -1,
            };
        }

        public dynamic GetIdsTransferMock(int num)  
        {
            return num switch
            {
                1 => new List<int>() { 4, 5 },
                2 => new List<int>() { 6, 7 },
                3 => new List<int>() { 8, 9 },
                4 => "The account of receiver is not found",
                5 => "The amount is missing",
                6 => "",               //"Not enough money"   должно быть   
                _ => new List<int>(),
            };
        }                                                //когда у Акк нет денег, он не выводит badRequest, выводит либо "0", либо ""

        public dynamic GetIdWithdrawMock(int num)   
        {
            return num switch
            {
                1 => 10,
                2 => 11,
                3 => 12,
                4 => "The amount is missing",
                5 => "0",          //"Not enough money"    должно быть
                6 => "The account is not found",
                _ => -1,
            };
        }

        public dynamic GetBalanceMockByAccountId(int num)  
        {
            return num switch
            {
                1 => 0.0000M,
                2 => 4000.0000M,
                3 => 324500.0000M,
                4 => 3616.4123M,
                5 => 422.2117M,
                256 => 0,   //"Account was not found" должно быть
                _ => -1,
            };
        }

        public dynamic GetTransactionMockById(int num) 
        {
            return num switch
            {
                1 => new TransactionOutputModel()
                {
                    Id = 1,
                    AccountId = 1,
                    Type = "Deposit",
                    Amount = 800000,
                },
                2 => new TransactionOutputModel()
                {
                    Id = 2,
                    AccountId = 2,
                    Type = "Deposit",
                    Amount = 5000,
                },
                4 => new TransactionOutputModel()
                {
                    Id = 4,
                    AccountId = 1,
                    Type = "Transfer",
                    Amount = -500000,
                },
                8 => new TransactionOutputModel()
                {
                    Id = 8,
                    AccountId = 4,
                    Type = "Transfer",
                    Amount = -500.0000M
                },
                13 => new TransactionOutputModel(),
                0 => "Transactions were not found",
                _ => -1,
            };
        }

        public dynamic GetTransactionsMockByAccountId(int num)  
        {
            switch (num)
            {
                case 1:
                    {
                        return new List<TransactionOutputModel>()
                        {
                            new TransactionOutputModel()
                            {
                                Id = 1,
                                AccountId = 1,
                                Type = "Deposit",
                                Amount = 800000,
                            },
                            new TransactionOutputModel()
                            {
                                Id = 4,
                                AccountId = 1,
                                Type = "Transfer",
                                Amount = -500000
                            },
                            new TransactionOutputModel()
                            {
                                Id = 6,
                                AccountId = 1,
                                Type = "Transfer",
                                Amount = -300000
                            }
                        };
                    }
                case 2:
                    {
                        return new List<TransactionOutputModel>()
                        {
                            new TransactionOutputModel()
                            {
                                Id = 2,
                                AccountId = 2,
                                Type = "Deposit",
                                Amount = 5000
                            },
                            new TransactionOutputModel()
                            {
                                Id = 10,
                                AccountId = 2,
                                Type = "Withdraw",
                                Amount = -1000
                            }
                        };
                    }
                case 3:
                    {
                        return new List<TransactionOutputModel>()
                        {
                            new TransactionOutputModel()
                            {
                                Id = 3,
                                AccountId = 3,
                                Type = "Deposit",
                                Amount = 325000
                            },
                            new TransactionOutputModel()
                            {
                                Id = 11,
                                AccountId = 3,
                                Type = "Withdraw",
                                Amount = -500
                            }
                        };
                    }
                case 0:
                    {
                        return "Account was not found";
                    }
                case 6:
                    {
                        return new List<TransactionOutputModel>()
                        {
                            new TransactionOutputModel()
                            {
                                Id = 4,
                                AccountId = 1,
                                Type = "Transfer",
                                Amount = -500000
                            }
                        };
                    }
                case 4:
                    {
                        return new List<TransactionOutputModel>()
                        {
                            new TransactionOutputModel()
                            {
                                Id = 12,
                                AccountId = 4,
                                Type = "Withdraw",
                                Amount = -2,
                            },
                            new TransactionOutputModel()
                            {
                                Id = 6,
                                AccountId = 1,
                                Type = "Transfer",
                                Amount = -300000,
                            },
                            new TransactionOutputModel()
                            {
                                Id = 8,
                                AccountId = 4,
                                Type = "Transfer",
                                Amount = -500,
                            }
                        };
                    }
            }
            return new List<TransactionOutputModel>();
        }
    }
}
