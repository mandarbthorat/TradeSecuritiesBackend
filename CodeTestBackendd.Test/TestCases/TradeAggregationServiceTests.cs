using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using CodeTestBackend.Domain.Entities;
using CodeTestBackend.Application;
using System;
using System.IO;
using System.Linq;
using CodeTestBackend.Application.Services;

namespace CodeTestBackend.Test.TestCases
{
    [TestClass]
    public class TradeAggregationServiceTests
    {
        [TestMethod]
        public void AggregateAndDisplay_ShouldAggregateCorrectly()
        {
            // Arrange
            var service = new TradeAggregationService();

            var trades = new List<Trade>
            {
                new Trade { BloombergId = "AAA", TransactionCode = "Buy", TradeDate = new DateTime(2024, 7, 24), Quantity = 100, Price = 10, IsSecurityValid = true },
                new Trade { BloombergId = "AAA", TransactionCode = "Buy", TradeDate = new DateTime(2024, 7, 24), Quantity = 200, Price = 20, IsSecurityValid = true },
                new Trade { BloombergId = "BBB", TransactionCode = "Sell", TradeDate = new DateTime(2024, 7, 24), Quantity = 150, Price = 30, IsSecurityValid = true }
            };

           
            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                service.AggregateAndDisplay(trades);

                // Assert
                var output = sw.ToString();
                Assert.IsTrue(output.Contains("AAA"));
                Assert.IsTrue(output.Contains("BBB"));
                Assert.IsTrue(output.Contains("300.00")); 
                Assert.IsTrue(output.Contains("15.00"));  
                Assert.IsTrue(output.Contains("150.00")); 
                Assert.IsTrue(output.Contains("30.00"));  
            }
        }

        [TestMethod]
        public void DisplayInvalidSecurityFiles_ShouldListFilesWithInvalidTrades()
        {
            // Arrange
            var service = new TradeAggregationService();
            var trades = new List<Trade>
            {
                new Trade { FileName = "Test1.xml", IsSecurityValid = false },
                new Trade { FileName = "Test1.xml", IsSecurityValid = false },
                new Trade { FileName = "Test2.xml", IsSecurityValid = false },
                new Trade { FileName = "Test2.xml", IsSecurityValid = true }
            };

            using (var sw = new StringWriter())
            {
                Console.SetOut(sw);

                // Act
                service.DisplayInvalidSecurityFiles(trades);

                // Assert
                var output = sw.ToString();
                Assert.IsTrue(output.Contains("Test1.xml"));
                Assert.IsTrue(output.Contains("Invalid Trades: 2"));
                Assert.IsTrue(output.Contains("Test2.xml"));
                Assert.IsTrue(output.Contains("Invalid Trades: 1"));
            }
        }
    }
}
