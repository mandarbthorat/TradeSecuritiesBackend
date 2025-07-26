using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTestBackend.Domain.Entities
{
    public class Trade
    {
        public string FileName { get; set; }
        public string BloombergId { get; set; }
        public string TransactionCode { get; set; }
        public DateTime TradeDate { get; set; }
        public decimal Quantity { get; set; }
        public decimal Price { get; set; }
        public bool IsSecurityValid { get; set; }
    }
}
