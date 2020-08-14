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
                    AccountIdReceiver = 6                    
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
                    AccountIdReceiver = 5                   
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

        public TransactionInputModel GetWithdrawInputModel(int num)
        {
            return num switch
            {
                1 => new TransactionInputModel()
                {
                    AccountId = 2,
                    Amount = 1000
                },
                2 => new TransactionInputModel()
                {
                    AccountId = 3,
                    Amount = 500
                },
                3 => new TransactionInputModel()
                {
                    AccountId = 4,
                    Amount = 2
                },
                4 => new TransactionInputModel()
                {
                    AccountId = 1,
                    Amount = -200000
                },
                5 => new TransactionInputModel()
                {
                    AccountId = 1,
                    Amount = 2000
                },
                6 => new TransactionInputModel()
                {
                    AccountId = 546,
                    Amount = 2003
                },
                _ => new TransactionInputModel(),
            };
        }
    }
}
    
