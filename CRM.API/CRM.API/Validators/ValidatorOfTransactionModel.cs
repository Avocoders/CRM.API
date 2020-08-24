using CRM.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransactionStore.API.Models.Input;

namespace CRM.API.Validators
{
    public class ValidatorOfTransactionModel
    {
        private readonly ILeadRepository _repo;
        public ValidatorOfTransactionModel()
        {
           
        }
       
        public string ValidateTransferInputModel(TransferInputModel transactionModel)
        {
            if (_repo.GetAccountById(transactionModel.AccountIdReceiver).Data is null) return ("The account of receiver is not found");
            return "";
        }
        public string ValidateTransactionInputModel(TransactionInputModel transactionModel)
        {
            if (_repo.GetAccountById(transactionModel.AccountId).Data is null) return ("The account is not found");
            if (transactionModel.Amount <= 0) return ("The amount is missing");
            return "";
        }
    }

} 
