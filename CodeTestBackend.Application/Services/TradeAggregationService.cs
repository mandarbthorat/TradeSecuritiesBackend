using CodeTestBackend.Application.Interfaces;
using CodeTestBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTestBackend.Application.Services
{
    public class TradeAggregationService : ITradeAggregationService
    {
        public void AggregateAndDisplay(List<Trade> trades)
        {
            var grouped = trades
                .Where(t => t.IsSecurityValid)
                .GroupBy(t => new { t.BloombergId, t.TransactionCode, t.TradeDate })
                .Select(g => new
                {
                    g.Key.BloombergId,
                    g.Key.TransactionCode,
                    TradeDate = g.Key.TradeDate.ToString("yyyy-MM-dd"),
                    SumQuantity = g.Sum(x => x.Quantity),
                    AvgPrice = g.Average(x => x.Price)
                });

            Console.WriteLine("\nAggregated Trades:");
            int widthId = 12;
            int widthTran = 16;
            int widthDate = 12;
            int widthSum = 15;
            int widthAvg = 13;

            Console.WriteLine();
            Console.WriteLine(
                $"{ "BloombergId".PadRight(widthId) }" +
                $"{ "TransactionCode".PadRight(widthTran) }" +
                $"{ "TradeDate".PadRight(widthDate) }" +
                $"{ "SumQuantity".PadRight(widthSum) }" +
                $"{ "AveragePrice".PadRight(widthAvg) }"
            );

            Console.WriteLine(
                $"{ new string('-', widthId) }" +
                $"{ new string('-', widthTran) }" +
                $"{ new string('-', widthDate) }" +
                $"{ new string('-', widthSum) }" +
                $"{ new string('-', widthAvg) }"
            );

            foreach (var group in grouped)
            {
                Console.WriteLine(
                    $"{ group.BloombergId.PadRight(widthId) }" +
                    $"{ group.TransactionCode.PadRight(widthTran) }" +
                    $"{ group.TradeDate.PadRight(widthDate) }" +
                    $"{ group.SumQuantity.ToString("F2").PadLeft(widthSum) }" +
                    $"{ group.AvgPrice.ToString("F2").PadLeft(widthAvg) }"
                );
            }

        }

        public void DisplayInvalidSecurityFiles(List<Trade> trades)
        {
            var invalidTrades = trades.Where(t => !t.IsSecurityValid);
            var grouped = invalidTrades
                .GroupBy(t => t.FileName)
                .Select(g => new { File = g.Key, Count = g.Count() });

            Console.WriteLine("\nInvalid Security Trades:");
            foreach (var item in grouped)
            {
                Console.WriteLine($"File: {item.File}, Invalid Trades: {item.Count}");
            }
        }
    }
}
