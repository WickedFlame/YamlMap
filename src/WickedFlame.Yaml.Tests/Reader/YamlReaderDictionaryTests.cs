using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace WickedFlame.Yaml.Tests.Reader
{
    [TestFixture]
    public class YamlReaderDictionaryTests
    {
        [Test]
        public void WickedFlame_Yaml_YamlReader_Dictionary()
        {
            var lines = new[]
            {
                "Dictionary:",
                "  - Name: one",
                "  - Id: 1"
            };

            var reader = new YamlReader();
            var data = reader.Read<StringNode>(lines);

            Assert.That(data.Dictionary.Count() == 2);
            Assert.That(data.Dictionary["Name"] == "one");
            Assert.That(data.Dictionary["Id"] == "1");
        }

        [Test]
        public void WickedFlame_Yaml_YamlReader_Dictionary_Interface()
        {
            var lines = new[]
            {
                "IDictionary:",
                "  - Name: one",
                "  - Id: 1"
            };

            var reader = new YamlReader();
            var data = reader.Read<StringNode>(lines);

            Assert.That(data.IDictionary.Count() == 2);
            Assert.That(data.IDictionary["Name"] == "one");
            Assert.That(data.IDictionary["Id"] == "1");
        }

        public class StringNode
        {
            public Dictionary<string, string> Dictionary { get; set; }

            public IDictionary<string, string> IDictionary { get; set; }
        }
    }
}
