using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace YamlMap.Tests.TypeConvert
{
    [TestFixture]
    public class ParseBoolTests
    {
        [Test]
        public void ParsePrivitive_Bool_0()
        {
            var lines = new[]
            {
                "Value: 0"
            };
            
            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(false, parsed.Value);
        }

        [Test]
        public void ParsePrivitive_Bool_1()
        {
            var lines = new[]
            {
                "Value: 1"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(true, parsed.Value);
        }

        [Test]
        public void ParsePrivitive_Bool_false()
        {
            var lines = new[]
            {
                "Value: false"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(false, parsed.Value);
        }

        [Test]
        public void ParsePrivitive_Bool_true()
        {
            var lines = new[]
            {
                "Value: true"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(true, parsed.Value);
        }

        [Test]
        public void ParsePrivitive_Bool_false_Case()
        {
            var lines = new[]
            {
                "Value: FALSE"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(false, parsed.Value);
        }

        [Test]
        public void ParsePrivitive_Bool_true_Case()
        {
            var lines = new[]
            {
                "Value: TRUE"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(true, parsed.Value);
        }

        [Test]
        public void ParsePrivitive_Bool_no()
        {
            var lines = new[]
            {
                "Value: no"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(false, parsed.Value);
        }

        [Test]
        public void ParsePrivitive_Bool_yes()
        {
            var lines = new[]
            {
                "Value: yes"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(true, parsed.Value);
        }

        [Test]
        public void ParsePrivitive_Bool_Invalid()
        {
            var lines = new[]
            {
                "Value: 2"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(false, parsed.Value);

            lines = new[]
            {
                "Value: invalid"
            };

            parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(false, parsed.Value);
        }

        [Test]
        public void ParsePrivitive_BoolList()
        {
            var lines = new[]
            {
                "ValueList:",
                "  - true",
                "  - 0"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(true, parsed.ValueList[0]);
            Assert.AreEqual(false, parsed.ValueList[1]);
        }

        public class PrimitiveValues
        {
            public bool Value { get; set; }

            public List<bool> ValueList { get; set; }
        }
    }
}
