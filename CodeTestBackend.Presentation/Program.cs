using CodeTestBackend.Application.Interfaces;
using CodeTestBackend.Application.Services;
using CodeTestBackend.Domain.Interfaces;
using CodeTestBackend.Infrastructure.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity;
namespace CodeTestBackend.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            container.RegisterSingleton<ISecurityRepository, SecurityXmlRepository>();
            container.RegisterSingleton<ITradeRepository, TradeXmlRepository>();
            container.RegisterSingleton<ITradeAggregationService, TradeAggregationService>();

            string securitiesXmlPath = ConfigurationManager.AppSettings["SecuritiesXmlPath"];
            string tradesRootPath = ConfigurationManager.AppSettings["TradesRootPath"];

            securitiesXmlPath = GetOrPromptValidPath(
                securitiesXmlPath,
                "Enter full path to Securities.xml:",
                isFile: true
            );

            tradesRootPath = GetOrPromptValidPath(
                tradesRootPath,
                "Enter full path to Trades root folder:",
                isFile: false
            );
            var securityRepo = container.Resolve<ISecurityRepository>();
            var tradeRepo = container.Resolve<ITradeRepository>();
            var aggregator = container.Resolve<ITradeAggregationService>();

            try
            {
                var validBloombergIds = securityRepo.GetValidBloombergIds(securitiesXmlPath);
                var trades = tradeRepo.ReadTrades(tradesRootPath, validBloombergIds);

                aggregator.AggregateAndDisplay(trades);
                aggregator.DisplayInvalidSecurityFiles(trades);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }

            Console.WriteLine("\nDone! Press any key to exit.");
            Console.ReadKey();
        }

        /// <summary>
        /// Checks for a valid path; if missing or invalid, prompts user for input until a valid path is entered.
        /// </summary>
        static string GetOrPromptValidPath(string initialPath, string prompt, bool isFile)
        {
            string path = initialPath;
            while (true)
            {
                if (!string.IsNullOrWhiteSpace(path))
                {
                    if (isFile && File.Exists(path))
                        return path;
                    if (!isFile && Directory.Exists(path))
                        return path;

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"The {(isFile ? "file" : "folder")} path '{path}' does not exist or is inaccessible.");
                    Console.ResetColor();
                }
                Console.WriteLine(prompt);
                path = Console.ReadLine();
            }
        }
    }
}
