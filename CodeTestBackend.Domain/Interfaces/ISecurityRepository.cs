using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTestBackend.Domain.Interfaces
{
    public interface ISecurityRepository
    {
        HashSet<string> GetValidBloombergIds(string securitiesXmlPath);
    }
}
