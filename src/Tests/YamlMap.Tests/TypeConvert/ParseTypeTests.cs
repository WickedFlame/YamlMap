using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using NUnit.Framework.Legacy;
using YamlMap.Serialization;

namespace YamlMap.Tests.TypeConvert
{
    [TestFixture]
    public class ParseTypeTests
    {
        [Test]
        public void ParsePrivitive_Type()
        {
            var lines = new[]
            {
                "Value: YamlMap.Tests.TypeConvert.ParseTypeTests, YamlMap.Tests"
            };
            
            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            ClassicAssert.AreEqual(typeof(ParseTypeTests), parsed.Value);
        }

        [Test]
        public void ParsePrivitive_Type_InvalidType()
        {
            var lines = new[]
            {
                "Value: YamlMap.Tests.TypeConvert.InvalidType, YamlMap.Tests"
            };

            var reader = new YamlReader();
            Assert.Throws<YamlSerializationException>(() => reader.Read<PrimitiveValues>(lines));
        }

        [Test]
        public void ParsePrivitive_Type_Null()
        {
            var lines = new[]
            {
                "Value: "
            };

            var reader = new YamlReader();
            Assert.Throws<YamlSerializationException>(() => reader.Read<PrimitiveValues>(lines));
        }

        [Test]
        public void ParsePrivitive_TypeList()
        {
            var lines = new[]
            {
                "ValueList:",
                "  - YamlMap.Tests.TypeConvert.ParseTypeTests+PrimitiveValues, YamlMap.Tests",
                "  - YamlMap.Tests.TypeConvert.ParseTypeTests, YamlMap.Tests"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            ClassicAssert.AreEqual(typeof(PrimitiveValues), parsed.ValueList[0]);
            ClassicAssert.AreEqual(typeof(ParseTypeTests), parsed.ValueList[1]);
        }

        public class PrimitiveValues
        {
            public Type Value { get; set; }

            public List<Type> ValueList { get; set; }
        }
    }
}
