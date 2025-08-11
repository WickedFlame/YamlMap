using System;
using System.Collections.Generic;
using System.Linq;

namespace YamlMap.Tests.Reader
{
    [TestFixture]
    public class YamlReaderDictionaryTests
    {
        [Test]
        public void YamlMap_YamlReader_Dictionary()
        {
            var lines = new[]
            {
                "Dictionary:",
                "  Name: one",
                "  Id: 1"
            };

            var reader = new YamlReader();
            var data = reader.Read<StringNode>(lines);

            Assert.That(data.Dictionary.Count() == 2);
            Assert.That(data.Dictionary["Name"] == "one");
            Assert.That(data.Dictionary["Id"] == "1");
        }

        [Test]
        public void YamlMap_YamlReader_Dictionary_Interface()
        {
            var lines = new[]
            {
                "IDictionary:",
                "  Name: one",
                "  Id: 1"
            };

            var reader = new YamlReader();
            var data = reader.Read<StringNode>(lines);

            Assert.That(data.IDictionary.Count() == 2);
            Assert.That(data.IDictionary["Name"] == "one");
            Assert.That(data.IDictionary["Id"] == "1");
        }

        [Test]
        public void YamlMap_YamlReader_Dictionary_List()
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
            Action act = () => reader.Read<StringNode>(lines);
            act.Should().Throw<InvalidConfigurationException>();
		}

        [Test]
        public void YamlMap_YamlReader_Dictionary_List_ExceptionMessage()
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
            try
            {
                reader.Read<StringNode>(lines);
            }
            catch (InvalidConfigurationException e)
            {
                e.Message.Should().Be("The configured Node 'Name: one' could not be mapped to Type 'System.String'. This is an indication that the configured Node does not match the expected Type on the Object");
            }
        }

        [Test]
        public void YamlMap_YamlReader_Dictionary_Objects()
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
            var data = reader.Read<StringNode>(lines);

            Assert.That(data.ObjectDictionary.Count() == 2);
            Assert.That(data.ObjectDictionary["one"].Id == 1);
            Assert.That(data.ObjectDictionary["one"].Name == "test one");
            Assert.That(data.ObjectDictionary["two"].Id == 2);
            Assert.That(data.ObjectDictionary["two"].Name == "test two");
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
