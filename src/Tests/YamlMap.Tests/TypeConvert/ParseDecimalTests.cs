using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace YamlMap.Tests.TypeConvert
{
    [TestFixture]
    public class ParseDecimalTests
    {
        [Test]
        public void ParsePrivitive_Decimal()
        {
            var lines = new[]
            {
                "Value: 1.2"
            };
            
            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(1.2, parsed.Value);
        }

        [Test]
        public void ParsePrivitive_DecimalList()
        {
            var lines = new[]
            {
                "ValueList:",
                "  - 1.1",
                "  - 2.2"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(1.1, parsed.ValueList[0]);
            Assert.AreEqual(2.2, parsed.ValueList[1]);
        }

        public class PrimitiveValues
        {
            public decimal Value { get; set; }

            public List<decimal> ValueList { get; set; }
        }
    }
}
