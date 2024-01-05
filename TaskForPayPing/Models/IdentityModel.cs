using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transaction.WebApi.Models
{
    public class IdentityModel
    {
        public Guid UserID { get; set; }
        public int AccountNumber { get; set; }
        public required string FullName { get; set; }
        public required string Currency { get; set; }
    }
}
