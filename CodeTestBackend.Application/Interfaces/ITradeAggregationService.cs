using CodeTestBackend.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTestBackend.Application.Interfaces
{
    public interface ITradeAggregationService
    {
        void AggregateAndDisplay(List<Trade> trades);
        void DisplayInvalidSecurityFiles(List<Trade> trades);
    }
}
