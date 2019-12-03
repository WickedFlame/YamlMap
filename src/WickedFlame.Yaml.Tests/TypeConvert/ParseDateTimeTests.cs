using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace WickedFlame.Yaml.Tests.TypeConvert
{
    [TestFixture]
    public class ParseDateTimeTests
    {
        [Test]
        public void ParsePrivitive_DateTime()
        {
            var lines = new[]
            {
                "Value: 31.12.1919"
            };
            
            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(new DateTime(1919, 12, 31), parsed.Value);
        }

        [Test]
        public void ParsePrivitive_DateTime_Slash()
        {
            var lines = new[]
            {
                "Value: 31/12/1919"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(new DateTime(1919, 12, 31), parsed.Value);
        }

        [Test]
        public void ParsePrivitive_DateTime_yyyyMMdd()
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
        public void ParsePrivitive_DateTimelList()
        {
            var lines = new[]
            {
                "ValueList:",
                "  - 1.12.1919",
                "  - 31.12.1919"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(new DateTime(1919, 12, 1), parsed.ValueList[0]);
            Assert.AreEqual(new DateTime(1919, 12, 31), parsed.ValueList[1]);
        }

        public class PrimitiveValues
        {
            public DateTime Value { get; set; }

            public List<DateTime> ValueList { get; set; }
        }
    }
}
