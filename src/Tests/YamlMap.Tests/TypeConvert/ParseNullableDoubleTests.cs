using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Legacy;

namespace YamlMap.Tests.TypeConvert
{
    [TestFixture]
    public class ParseNullableDoubleTests
    {
        [Test]
        public void ParsePrivitive_NullableDouble()
        {
            var lines = new[]
            {
                "Value: 1.2"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            ClassicAssert.AreEqual(1.2, parsed.Value);
        }

        [Test]
        public void ParsePrivitive_NullableDouble_Null()
        {
            var lines = new[]
            {
                "Value: "
            };
            
            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            ClassicAssert.AreEqual(null, parsed.Value);
        }

        [Test]
        public void ParsePrivitive_NullableDoublelList()
        {
            var lines = new[]
            {
                "ValueList:",
                "  - ",
                "  - 2.2"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            ClassicAssert.AreEqual(null, parsed.ValueList[0]);
            ClassicAssert.AreEqual(2.2, parsed.ValueList[1]);
        }

        public class PrimitiveValues
        {
            public Nullable<double> Value { get; set; }

            public List<Nullable<double>> ValueList { get; set; }
        }
    }
}
