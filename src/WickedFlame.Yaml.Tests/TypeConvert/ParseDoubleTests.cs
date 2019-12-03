using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace WickedFlame.Yaml.Tests.TypeConvert
{
    [TestFixture]
    public class ParseDoubleTests
    {
        [Test]
        public void ParsePrivitive_Double()
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
        public void ParsePrivitive_Double_Invalid()
        {
            var lines = new[]
            {
                "Value: value"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(0, parsed.Value);
        }

        [Test]
        public void ParsePrivitive_Double_InvalidFormat()
        {
            var lines = new[]
            {
                "Value: 1.2.2"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(0, parsed.Value);
        }

        [Test]
        public void ParsePrivitive_DoublelList()
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
            public double Value { get; set; }

            public List<double> ValueList { get; set; }
        }
    }
}
