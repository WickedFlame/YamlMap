using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace YamlMap.Tests.Reader
{
    [TestFixture]
    public class YamlReaderListTests
    {
        [Test]
        public void YamlMap_YamlReader_List()
        {
            var lines = new[]
            {
                "List:",
                "  - one",
                "  - 1"
            };

            var reader = new YamlReader();
            var data = reader.Read<StringNode>(lines);

            Assert.That(data.List.Count() == 2);
            Assert.That(data.List[0] == "one");
            Assert.That(data.List[1] == "1");
        }

        [Test]
        public void YamlMap_YamlReader_List_Interface()
        {
            var lines = new[]
            {
                "IList:",
                "  - one",
                "  - 1"
            };

            var reader = new YamlReader();
            var data = reader.Read<StringNode>(lines);

            Assert.That(data.IList.Count() == 2);
            Assert.That(data.IList[0] == "one");
            Assert.That(data.IList[1] == "1");
        }

        [Test]
        public void YamlMap_YamlReader_List_IEnumerable()
        {
            var lines = new[]
            {
                "IEnumerable:",
                "  - one",
                "  - 1"
            };

            var reader = new YamlReader();
            var data = reader.Read<StringNode>(lines);

            Assert.That(data.IEnumerable.Count() == 2);
            Assert.That(data.IEnumerable.First() == "one");
            Assert.That(data.IEnumerable.Last() == "1");
        }

        [Test]
        public void YamlMap_YamlReader_List_Invalid_Indentation()
        {
            var lines = new[]
            {
                "IEnumerable:",
                "- one",
                "- 1"
            };

            var reader = new YamlReader();
            Action act = () => reader.Read<StringNode>(lines);
            act.Should().Throw<InvalidConfigurationException>();
        }

        [Test]
        public void YamlMap_YamlReader_List_Invalid_Indentation_Message()
        {
            var lines = new[]
            {
                "IEnumerable:",
                "- one",
                "- 1"
            };

            var reader = new YamlReader();
            try
            {
                reader.Read<StringNode>(lines);
            }
            catch (InvalidConfigurationException e)
            {
                e.Message.Should().Be("The configured ListItem 'one' could not be mapped to Type 'YamlMap.Tests.Reader.YamlReaderListTests+StringNode'. This is an indication that the configured Node does not match the expected Type on the Object");
            }
        }

        public class StringNode
        {
            public List<string> List { get; set; }

            public IList<string> IList { get; set; }

            public IEnumerable<string> IEnumerable { get; set; }
        }
    }
}
