namespace Transaction.Framework.Data.Interface
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Transaction.Framework.Data.Entities;
    using Transaction.Framework.Types;

    public interface IAccountTransactionRepository
    {
        Task Update(AccountTransactionEntity accountTransactionEntity, AccountSummaryEntity accountSummaryEntity);
        Task Create(AccountTransactionEntity accountTransactionEntity, AccountSummaryEntity accountSummaryEntity);
        Task<IEnumerable<AccountTransactionEntity>> Get(TransactionReportFilterModel transactionReportFilterModel);
    }
}
