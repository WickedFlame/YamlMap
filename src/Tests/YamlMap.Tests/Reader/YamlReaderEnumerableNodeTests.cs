using System.Collections.Generic;
using NUnit.Framework;
using Polaroider;

namespace YamlMap.Tests.Reader
{
    [TestFixture]
    public class YamlReaderEnumerableNodeTests
    {
        [Test]
        public void YamlMap_EnumerableNode()
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
            var data = reader.Read<EnumerableItem>(lines);

            data.MatchSnapshot();
        }

        public class EnumerableItem
        {
            // 
            // Uses List<SimpleNode> to map to IEnumerable<SimpleNode>
            //
            public IEnumerable<SimpleNode> Enumerable { get; set; }
        }

        public class SimpleNode
        {
            public string Name { get; set; }

            public int Value { get; set; }
        }
    }
}
