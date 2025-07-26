using Microsoft.VisualStudio.TestTools.UnitTesting;
using CodeTestBackend.Infrastructure;
using System.IO;
using System.Xml.Linq;
using System.Linq;
using System.Collections.Generic;
using CodeTestBackend.Infrastructure.Repository;

namespace CodeTestBackend.Test.TestCases
{
    [TestClass]
    public class SecurityXmlRepositoryTests
    {
        [TestMethod]
        public void GetValidBloombergIds_ShouldExtractIdsFromXml()
        {
            // Arrange: create temp XML file
            var xml = @"<?xml version=""1.0""?>
                <Securities>
                    <Security><Id>1</Id><BloombergId>AAA</BloombergId></Security>
                    <Security><Id>2</Id><BloombergId>BBB</BloombergId></Security>
                </Securities>";
            var tempPath = Path.GetTempFileName();
            File.WriteAllText(tempPath, xml);

            var repo = new SecurityXmlRepository();

            // Act
            var ids = repo.GetValidBloombergIds(tempPath);

            // Assert
            Assert.IsTrue(ids.Contains("AAA"));
            Assert.IsTrue(ids.Contains("BBB"));

            // Clean up
            File.Delete(tempPath);
        }
    }
}
