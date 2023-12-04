﻿using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace YamlMap.Tests.TypeConvert
{
    [TestFixture]
    public class ParseLongTests
    {
        [Test]
        public void ParsePrivitive_Long()
        {
            var lines = new[]
            {
                "Value: 2"
            };
            
            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.That(2, Is.EqualTo(parsed.Value));
        }

        [Test]
        public void ParsePrivitive_Long_FromDecimal()
        {
            var lines = new[]
            {
                "Value: 2.2"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.That(0, Is.EqualTo(parsed.Value));
        }

        [Test]
        public void ParsePrivitive_Long_Invalid()
        {
            var lines = new[]
            {
                "Value: value"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.That(0, Is.EqualTo(parsed.Value));
        }

        [Test]
        public void ParsePrivitive_LongList()
        {
            var lines = new[]
            {
                "ValueList:",
                "  - 1",
                "  - 2"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.That(1, Is.EqualTo(parsed.ValueList[0]));
            Assert.That(2, Is.EqualTo(parsed.ValueList[1]));
        }

        public class PrimitiveValues
        {
            public long Value { get; set; }

            public List<long> ValueList { get; set; }
        }
    }
}
