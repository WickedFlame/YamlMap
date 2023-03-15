using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace YamlMap.Tests.Reader
{
    [TestFixture]
    public class YamlReaderEnumerableClassTests
    {
        [Test]
        public void YamlMap_EnumerableClass_Exception()
        {
            var lines = new[]
            {
                "Enumerable:",
                "  - Name: one",
                "    Value: 1",
                "  - Name: two",
                "    Value: 2"
            };

            var reader = new YamlReader();
            Action act = () => reader.Read<EnumerableItem>(lines);
            act.Should().Throw<YamlMap.InvalidConfigurationException>();
        }

        [Test]
        public void YamlMap_EnumerableClass_ExceptionMessage()
        {
            var lines = new[]
            {
                "Enumerable:",
                "  - Name: one",
                "    Value: 1",
                "  - Name: two",
                "    Value: 2"
            };

            var reader = new YamlReader();
            try
            {
                reader.Read<EnumerableItem>(lines);
            }
            catch (InvalidConfigurationException e)
            {
                e.Message.Should().Be("The configured ListItem could not be mapped to the Property 'Enumerable'. Expected Type for Property 'Enumerable' is 'YamlMap.Tests.Reader.YamlReaderEnumerableClassTests+EnumerableNode'");
            }
        }

        [Test]
        public void YamlMap_EnumerableClass_Exception_Json_Comparison()
        {
            var value = "{\"Enumerable\":[{\"Name\":\"one\", \"Value\":1},{\"Name\":\"two\", \"Value\":2}]}";
            
            Action act = () => Newtonsoft.Json.JsonConvert.DeserializeObject<EnumerableItem>(value);
            act.Should().Throw<Newtonsoft.Json.JsonSerializationException>();
        }

        public class EnumerableItem
        {
            public EnumerableNode Enumerable { get; set; }
        }

        public class EnumerableNode : IEnumerable<SimpleNode>
        {
            private List<SimpleNode> _nodes = new List<SimpleNode>();

            public void Add(SimpleNode node)
            {
                _nodes.Add(node);
            }

            public IEnumerator<SimpleNode> GetEnumerator()
            {
                return _nodes.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return GetEnumerator();
            }
        }

        public class SimpleNode
        {
            public string Name { get; set; }

            public int Value { get; set; }
        }
    }
}
