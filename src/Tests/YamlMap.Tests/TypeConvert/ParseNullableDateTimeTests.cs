using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace YamlMap.Tests.TypeConvert
{
    [TestFixture]
    public class ParseNullableDateTimeTests
    {
        [Test]
        public void ParsePrivitive_NullableDateTime()
        {
            var lines = new[]
            {
                "Value: 1919.12.31"
            };
            
            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(new DateTime(1919, 12, 31), parsed.Value);
        }

        [Test]
        public void ParsePrivitive_NullableDateTime_Slash()
        {
            var lines = new[]
            {
                "Value: 1919/12/31"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(new DateTime(1919, 12, 31), parsed.Value);
        }

        [Test]
        public void ParsePrivitive_NullableDateTime_yyyyMMdd()
        {
            var lines = new[]
            {
                "Value: 19191231"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(new DateTime(1919, 12, 31), parsed.Value);
        }

        [Test]
        public void ParsePrivitive_NullableDateTime_Null()
        {
            var lines = new[]
            {
                "Value: 1919.12.31"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(new DateTime(1919, 12, 31), parsed.Value);
        }

        [Test]
        public void ParsePrivitive_NullableDateTimelList()
        {
            var lines = new[]
            {
                "ValueList:",
                "  - ",
                "  - 1919.12.01",
                "  - 1919.12.31"
            };

            var reader = new YamlReader();
            Assert.Throws<FormatException>(() => reader.Read<PrimitiveValues>(lines));
            //var parsed = reader.Read<PrimitiveValues>(lines);

            //Assert.That(parsed.ValueList.Count == 2);
            //Assert.AreEqual(new DateTime(1919, 12, 1), parsed.ValueList[0]);
            //Assert.AreEqual(new DateTime(1919, 12, 31), parsed.ValueList[1]);
        }

        public class PrimitiveValues
        {
            public Nullable<DateTime> Value { get; set; }

            public List<Nullable<DateTime>> ValueList { get; set; }
        }
    }
}
