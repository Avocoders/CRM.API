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
                6 => "",            //когда у Акк нет денег, он не выводит badRequest
                _ => new List<int>(),
            };
        }
    }
}
