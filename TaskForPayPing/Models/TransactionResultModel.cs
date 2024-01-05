namespace Transaction.WebApi.Models
{
    public class TransactionResultModel
    {
        public int? AccountNumber { get; set; }
        public bool IsSuccessful { get; set; }
        public decimal? Balance { get; set; }
        public required string Currency { get; set; }
        public required string Message { get; set; }
        public Guid UserID { get; set; }
        public DateTime Date { get; set; }

    }
}
