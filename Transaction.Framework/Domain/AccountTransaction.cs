using Transaction.Framework.Types;

namespace Transaction.Framework.Domain
{
    public class AccountTransaction
    {
        public Guid UserID { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
        public int AccountNumber { get; set; }
    }
}
