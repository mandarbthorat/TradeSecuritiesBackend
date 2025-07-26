using CodeTestBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTestBackend.Domain.Interfaces
{
    public interface ITradeRepository
    {
       List<Trade> ReadTrades(string tradesRootPath, HashSet<string> validBloombergIds);
    }
}
