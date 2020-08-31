using CRM.API.Models.Output;
using System;
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
                6 => 520,               
                _ => new List<int>(),
            };
        }                                              

        public dynamic GetIdWithdrawMock(int num)   
        {
            return num switch
            {
                1 => 13,
                2 => 14,
                3 => 15,
                4 => "The amount is missing",
                5 => "0",         
                6 => "The account is not found",
                _ => -1,
            };
        }

        public dynamic GetBalanceMockByAccountId(int num)  
        {
            return num switch
            {
                1 => 0.0000M,
                2 => 5000.0000M,
                3 => 325000.0000M,
                4 => 3551.3713m,
                5 => 419.8625m,
                _ => -1,
            };
        }

        public dynamic GetTransactionMockById(int num) 
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
                                Timestamp = DateTime.Now.ToString("dd:MMMM:yyyy"),
                                AccountIdReceiver =null,
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
                                Amount = 5000,
                                Timestamp = DateTime.Now.ToString("dd:MMMM:yyyy"),
                                AccountIdReceiver = null,
                            }
                        };
                    }
                case 5:
                    {
                        return new List<TransactionOutputModel>()
                        {
                            new TransactionOutputModel()
                            {
                                Id = 4,
                                AccountId = 1,
                                Type = "Transfer",
                                Amount = -500000.0000M,
                                Timestamp = DateTime.Now.ToString("dd:MMMM:yyyy"),
                                AccountIdReceiver = 6,
                            }
                        };
                    }
                case 8:
                    {
                        return new List<TransactionOutputModel>()
                        {
                            new TransactionOutputModel()
                            {
                                Id = 8,
                                AccountId = 4,
                                Type = "Transfer",
                                Amount = -500,
                                Timestamp = DateTime.Now.ToString("dd:MMMM:yyyy"),
                                AccountIdReceiver = 5,
                            }
                        };
                    }                            
            };
            return new List<TransactionOutputModel>();
        }

        public dynamic GetTransactionsMockByAccountId(int num)  
        {
            switch (num)
            {
                case 1:
                    return new List<TransactionOutputModel>()
                    {
                          new TransactionOutputModel()
                            {
                                Id = 1,
                                AccountId = 1,
                                Type = "Deposit",
                                Amount = 800000,
                                Timestamp = DateTime.Now.ToString("dd:MMMM:yyyy"),
                                AccountIdReceiver = null,
                            },
                            new TransactionOutputModel()
                            {
                                Id = 4,
                                AccountId = 1,
                                Type = "Transfer",
                                Amount = -500000,
                                Timestamp = DateTime.Now.ToString("dd:MMMM:yyyy"),
                                AccountIdReceiver = 6,
                            },
                            new TransactionOutputModel()
                            {
                                Id = 6,
                                AccountId = 1,
                                Type = "Transfer",
                                Amount = -300000,
                                Timestamp = DateTime.Now.ToString("dd:MMMM:yyyy"),
                                AccountIdReceiver = 4,
                            }
                        };

            
                case 2:
                    {
                        return new List<TransactionOutputModel>()
                        {
                            new TransactionOutputModel()
                            {
                                Id = 2,
                                AccountId = 2,
                                Type = "Deposit",
                                Amount = 5000,
                                Timestamp = DateTime.Now.ToString("dd:MMMM:yyyy"),
                                AccountIdReceiver = null,
                            },
                           
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
                                Amount = 325000,
                                Timestamp = DateTime.Now.ToString("dd:MMMM:yyyy"),
                                AccountIdReceiver = null,
                            },
                         
                        };
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
                                Amount = -500000,
                                Timestamp = DateTime.Now.ToString("dd:MMMM:yyyy"),
                                AccountIdReceiver = 6,
                            }
                        };
                    }
                case 4:
                    {
                        return new List<TransactionOutputModel>()
                        {
                           
                          
                        };
                    }
            }
            return new List<TransactionOutputModel>();
        }
    }
}
