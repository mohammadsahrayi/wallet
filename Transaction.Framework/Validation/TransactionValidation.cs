﻿namespace Transaction.Framework.Validation
{
    using System.Threading.Tasks;
    using Transaction.Framework.Domain;
    using Transaction.Framework.Exceptions;
    using Transaction.Framework.Types;

    public static class TransactionValidation
    {
        public static async Task Validate(this AccountTransaction accountTransaction, AccountSummary accountSummary)
        {
            var amount = accountTransaction.Amount;

            if (amount <= 0)
            {
                throw new InvalidAmountException(amount);
            }

            if (accountTransaction.TransactionType == TransactionType.Withdrawal)
            {
                if (amount > accountSummary.Balance.Amount)
                {
                    throw new InsufficientBalanceException();
                }
            }

            await Task.CompletedTask;
        }

        public static async Task Validate(this AccountSummary accountSummary, int accountNumber)
        {
            if (accountSummary == null)
            {
                throw new InvalidAccountNumberException(accountNumber);
            }            

            await Task.CompletedTask;
        }
    }
}
