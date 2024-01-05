namespace Transaction.Framework.Domain
{
    using System;
    using Transaction.Framework.Types;

    public class TransactionResult
    {
        public int AccountNumber { get; set; }
        public bool IsSuccessful { get; set; }
        public Money Balance { get; set; }
        public required string Message { get; set; }   
        public Guid UserID { get; set; }
        public DateTime Date { get; set; }
    }
}
