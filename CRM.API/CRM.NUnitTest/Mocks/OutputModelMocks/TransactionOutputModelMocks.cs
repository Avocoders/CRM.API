using CRM.API.Models.Output;
using System.Collections.Generic;

namespace CRM.NUnitTest.Mocks.OutputModelMocks
{
    public class TransactionOutputModelMocks
    {
        public dynamic GetIdDeposit(int num)
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
        public dynamic GetIdsTransfer(int num)
        {
            return num switch
            {
                1 => new List<int>() {4, 5},  
                2 => new List<int>() {6, 7},                
                3 => new List<int>() {8, 9},
                4 => "The account of receiver is not found",
                5 => "The amount is missing",
                6 => "",           //"Not enough money"      
                _ => new List<int>(),
            };
        }                                                //когда у Акк нет денег, он не выводит badRequest, выводит либо "0", либо ""
        public dynamic GetIdWithdraw(int num)
        {
            return num switch
            {
                1 => 10,
                2 => 11,
                3 => 12,
                4 => "The amount is missing",
                5 => "0",     //"Not enough money"
                6 => "The account is not found",
                _ => -1,
            };
        }

        public List<TransactionOutputModel> GetTransactionsMockByAccountId(int num)
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
                case 5:
                    {
                        return new List<TransactionOutputModel>()
                        {
                            new TransactionOutputModel()
                            {
                                Id = 8,
                                AccountId = 4,
                                Type = "Transfer",
                                Amount = -500 
                            }
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
