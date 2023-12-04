using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace YamlMap.Tests.Reader
{
    [TestFixture]
    public class YamlReaderDictionaryTypeTests
    {
        [Test]
        public void YamlReaderDictionaryType_Dictionary()
        {
            var lines = new[]
            {
                "Dictionary:",
                "  Name: one",
                "  Id: 1"
            };

            var reader = new YamlReader();
            var data = reader.Read(typeof(StringNode), lines) as StringNode;

            Assert.That(data.Dictionary.Count() == 2);
            Assert.That(data.Dictionary["Name"] == "one");
            Assert.That(data.Dictionary["Id"] == "1");
        }

        [Test]
        public void YamlReaderDictionaryType_Dictionary_Interface()
        {
            var lines = new[]
            {
                "IDictionary:",
                "  Name: one",
                "  Id: 1"
            };

            var reader = new YamlReader();
            var data = reader.Read(typeof(StringNode), lines) as StringNode;

            Assert.That(data.IDictionary.Count() == 2);
            Assert.That(data.IDictionary["Name"] == "one");
            Assert.That(data.IDictionary["Id"] == "1");
        }

        [Test]
        public void YamlReaderDictionaryType_Dictionary_List()
        {
			// this is a list of object instead of a dictionary
			// to use dictionary remove the -
            var lines = new[]
            {
                "Dictionary:",
                "  - Name: one",
                "  - Id: 1"
            };

            var reader = new YamlReader();
			Assert.Throws<InvalidConfigurationException>(() => reader.Read(typeof(StringNode), lines));
		}

        [Test]
        public void YamlReaderDictionaryType_Dictionary_Objects()
        {
            var lines = new[]
            {
                "ObjectDictionary:",
                "  one:",
                "    Id: 1",
                "    Name: test one",
                "  two:",
                "    Id: 2",
                "    Name: test two"
            };

            var reader = new YamlReader();
            var data = reader.Read(typeof(StringNode), lines) as StringNode;

            Assert.That(data.ObjectDictionary.Count() == 2);
            Assert.That(data.ObjectDictionary["one"].Id == 1);
            Assert.That(data.ObjectDictionary["one"].Name == "test one");
            Assert.That(data.ObjectDictionary["two"].Id == 2);
            Assert.That(data.ObjectDictionary["two"].Name == "test two");
        }

        [Test]
        public void YamlReaderDictionaryType_Dictionary_Objects_Direct()
        {
            var lines = new[]
            {
                "one:",
                "  Id: 1",
                "  Name: test one",
                "two:",
                "  Id: 2",
                "  Name: test two"
            };

            var reader = new YamlReader();
            var data = reader.Read<Dictionary<string, DictObject>>(lines);

            Assert.That(data.Count() == 2);
            Assert.That(data["one"].Id == 1);
            Assert.That(data["one"].Name == "test one");
            Assert.That(data["two"].Id == 2);
            Assert.That(data["two"].Name == "test two");
        }

        public class StringNode
        {
            public Dictionary<string, string> Dictionary { get; set; }

            public IDictionary<string, string> IDictionary { get; set; }

            public Dictionary<string, DictObject> ObjectDictionary { get; set; }
        }

        public class DictObject
        {
            public int Id { get; set; }

            public string Name { get; set; }
        }
    }
}
