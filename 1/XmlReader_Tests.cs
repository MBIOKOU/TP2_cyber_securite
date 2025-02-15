using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace XXEExamples.Tests
{
    [TestFixture]
    public class XmlReader_Tests
    {
        [Test]
        public void XMLReader_WithDTDProcessingIgnored_Safe()
        {
            var exception = Assert.Throws<XmlException>(() =>
            {
                AssertXXE.IsXMLParserSafe((string xml) =>
                {
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.DtdProcessing = DtdProcessing.Ignore; // Désactiver le traitement des DTD
                    settings.MaxCharactersFromEntities = 6000;

                    using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
                    {
                        XmlReader reader = XmlReader.Create(stream, settings);

                        var xmlDocument = new XmlDocument();
                        xmlDocument.XmlResolver = null; // Désactiver l'utilisation de XmlResolver
                        xmlDocument.Load(reader);
                        return xmlDocument.InnerText;
                    }
                }, true);
            });

            Assert.IsTrue(exception.Message.StartsWith("Reference to undeclared entity 'xxe'."));
        }

        [Test]
        public void XMLReader_WithDTDProcessingProhibited_Safe()
        {
            var exception = Assert.Throws<XmlException>(() =>
            {
                AssertXXE.IsXMLParserSafe((string xml) =>
                {
                    XmlReaderSettings settings = new XmlReaderSettings();
                    settings.DtdProcessing = DtdProcessing.Prohibit; // Interdire le traitement des DTD
                    settings.MaxCharactersFromEntities = 6000;

                    using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
                    {
                        XmlReader reader = XmlReader.Create(stream, settings);

                        var xmlDocument = new XmlDocument();
                        xmlDocument.XmlResolver = null; // Désactiver l'utilisation de XmlResolver
                        xmlDocument.Load(reader);
                        return xmlDocument.InnerText;
                    }
                }, true);
            });

            Assert.IsTrue(exception.Message.StartsWith("For security reasons DTD is prohibited in this XML document."));
        }
    }
}
