using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Polaroider;
using YamlMap.Tests.Serialization;

namespace YamlMap.Tests.Reader
{
    public class YamlReaderTests
    {
        [Test]
        public void YamlReader_Read()
        {

            var value = new StringBuilder().AppendLine("Simple: root")
                .AppendLine("StringList:")
                .AppendLine("  - one")
                .AppendLine("  - 2")
                .AppendLine("ObjList:")
                .AppendLine("  - Simple: simple")
                .AppendLine("  - Child:")
                .AppendLine("      Simple: child").ToString();

            var reader = new YamlReader();
            var item = reader.Read<TestlItem>(value);
            item.MatchSnapshot();
        }

        public class TestlItem
        {
            public string Simple { get; set; }

            public DeserializerTests.TestlItem Child { get; set; }

            public IEnumerable<string> StringList { get; set; }

            public IEnumerable<DeserializerTests.TestlItem> ObjList { get; set; }
        }
    }
}
