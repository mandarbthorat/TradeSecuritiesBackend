using CodeTestBackend.Domain.Entities;
using CodeTestBackend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CodeTestBackend.Infrastructure.Repository
{
    public class TradeXmlRepository : ITradeRepository
    {
        public List<Trade> ReadTrades(string tradesRootPath, HashSet<string> validBloombergIds)
        {
            var trades = new List<Trade>();
            var files = Directory.GetFiles(tradesRootPath, "Trades*.xml", SearchOption.AllDirectories);
            foreach (var file in files)
            {
                var doc = XDocument.Load(file);
                foreach (var tradeElem in doc.Descendants("trade"))
                {
                    var code = tradeElem.Element("security")?.Element("code")?.Value;
                    var tradeDateStr = tradeElem.Element("tradeDate")?.Value;
                    DateTime tradeDate = DateTime.TryParseExact(tradeDateStr, "yyyyMMdd", null, System.Globalization.DateTimeStyles.None, out var dt)
                        ? dt
                        : DateTime.MinValue;

                    var trade = new Trade
                    {
                        FileName = Path.GetFileName(file),
                        BloombergId = code,
                        TransactionCode = tradeElem.Element("transactionCode")?.Value,
                        TradeDate = tradeDate,
                        Quantity = decimal.Parse(tradeElem.Element("quantity")?.Value ?? "0"),
                        Price = decimal.Parse(tradeElem.Element("price")?.Value ?? "0"),
                        IsSecurityValid = validBloombergIds.Contains(code)
                    };
                    trades.Add(trade);
                }
            }
            return trades;
        }

    }
}
