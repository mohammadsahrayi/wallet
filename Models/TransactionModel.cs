using Microsoft.AspNetCore.Identity;
using Transaction.Framework.Types;

namespace Transaction.WebApi.Models
{
    public class TransactionModel
    {
        public IdentityUser UserID { get; set; }
        public TransactionType TransactionType { get; set; }
        public decimal Amount { get; set; }
    }
}
