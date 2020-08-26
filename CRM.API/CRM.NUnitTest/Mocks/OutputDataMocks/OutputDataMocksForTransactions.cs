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
                1 => new List<int>() { 5, 6 },
                2 => new List<int>() { 7, 8 },
                3 => new List<int>() { 9, 10 },
                4 => "The account of receiver is not found",
                5 => "The amount is missing",
                6 => "",               
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
                2 => 4000.0000M,
                3 => 323500.0000M,
                4 => 3598.0436M,
                5 => 422.495M,
                256 => 0,  
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
                                Id = 5,
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
                                Id = 7,
                                AccountId = 1,
                                Type = "Transfer",
                                Amount = -300000.0000M,
                                Timestamp = DateTime.Now.ToString("dd:MMMM:yyyy"),
                                AccountIdReceiver = 4,
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
                                AccountIdReceiver = null,
                            },
                            new TransactionOutputModel()
                            {
                                Id = 5,
                                AccountId = 1,
                                Type = "Transfer",
                                Amount = -500000,
                                Timestamp = DateTime.Now.ToString("dd:MMMM:yyyy"),
                                AccountIdReceiver = 6,
                            },
                            new TransactionOutputModel()
                            {
                                Id = 7,
                                AccountId = 1,
                                Type = "Transfer",
                                Amount = -300000,
                                Timestamp = DateTime.Now.ToString("dd:MMMM:yyyy"),
                                AccountIdReceiver = 4,
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
                            },
                            new TransactionOutputModel()
                            {
                                Id = 13,
                                AccountId = 2,
                                Type = "Withdraw",
                                Amount = -1000,
                                Timestamp = DateTime.Now.ToString("dd:MMMM:yyyy"),
                                AccountIdReceiver = null,
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
                                Amount = 325000,
                                Timestamp = DateTime.Now.ToString("dd:MMMM:yyyy"),
                                AccountIdReceiver = null,
                            },
                            new TransactionOutputModel()
                            {
                                Id = 14,
                                AccountId = 3,
                                Type = "Withdraw",
                                Amount = -500,
                                Timestamp = DateTime.Now.ToString("dd:MMMM:yyyy"),
                                AccountIdReceiver = null,
                            },
                             new TransactionOutputModel()
                            {
                                Id = 11,
                                AccountId = 3,
                                Type = "Transfer",
                                Amount = -1000,
                                Timestamp = DateTime.Now.ToString("dd:MMMM:yyyy"),
                                AccountIdReceiver = -1,
                            }
                        };
                    }
                case 6:
                    {
                        return new List<TransactionOutputModel>()
                        {
                            new TransactionOutputModel()
                            {
                                Id = 5,
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
                            new TransactionOutputModel()
                            {
                                Id = 15,
                                AccountId = 4,
                                Type = "Withdraw",
                                Amount = -2,
                                Timestamp = DateTime.Now.ToString("dd:MMMM:yyyy"),
                                AccountIdReceiver = null,
                            },
                            new TransactionOutputModel()
                            {
                                Id = 7,
                                AccountId = 1,
                                Type = "Transfer",
                                Amount = -300000,
                                Timestamp = DateTime.Now.ToString("dd:MMMM:yyyy"),
                                AccountIdReceiver = 4,
                            },
                            new TransactionOutputModel()
                            {
                                Id = 9,
                                AccountId = 4,
                                Type = "Transfer",
                                Amount = -500,
                                Timestamp = DateTime.Now.ToString("dd:MMMM:yyyy"),
                                AccountIdReceiver = 5,
                            }
                        };
                    }
            }
            return new List<TransactionOutputModel>();
        }
    }
}
