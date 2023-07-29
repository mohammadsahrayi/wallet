using Transaction.Framework.Types;

namespace Transaction.Framework.Domain
{
    public class AccountTransaction
    {
        public int UserID { get; set; }
        public TransactionType TransactionType { get; set; }
        public Money Amount { get; set; }
        public int AccountNumber { get; set; }
    }
}
