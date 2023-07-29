namespace Transaction.Framework.Services.Interface
{
    using System.Threading.Tasks;
    using Transaction.Framework.Domain;
    using Transaction.Framework.Types;
    using System.Collections.Generic;

    public interface ITransactionService
    {
        Task<TransactionResult> Balance(int accountNumber);
        Task<TransactionResult> Deposit(AccountTransaction accountTransaction);
        Task<TransactionResult> Withdraw(AccountTransaction accountTransaction);
        Task<IEnumerable<TransactionResult>> TransactionReport(TransactionReportFilterModel transactionReportFilterModel);
    }
}
