using System;
using System.Collections.Generic;
using System.Text;

namespace Transaction.Framework.Types
{
    public class TransactionReportFilterModel
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Guid UserID { get; set; }
        public decimal MinAmount { get; set; }
        public decimal MaxAmount { get; set; }
    }
}
