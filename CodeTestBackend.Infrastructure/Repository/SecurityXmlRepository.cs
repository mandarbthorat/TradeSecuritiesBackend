using CodeTestBackend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace CodeTestBackend.Infrastructure.Repository
{
    public class SecurityXmlRepository : ISecurityRepository
    {
        public HashSet<string> GetValidBloombergIds(string securitiesXmlPath)
        {
            var doc = XDocument.Load(securitiesXmlPath);
            return new HashSet<string>(
                doc.Descendants("Security")
                   .Select(sec => sec.Element("BloombergId")?.Value)
                   .Where(id => !string.IsNullOrEmpty(id))
            );
        }
    }
}
