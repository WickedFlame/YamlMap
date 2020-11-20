using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace WickedFlame.Yaml.Tests.TypeConvert
{
    [TestFixture]
    public class ParseTypeTests
    {
        [Test]
        public void ParsePrivitive_Type()
        {
            var lines = new[]
            {
                "Value: WickedFlame.Yaml.Tests.TypeConvert.ParseTypeTests, WickedFlame.Yaml.Tests"
            };
            
            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(typeof(ParseTypeTests), parsed.Value);
        }

        [Test]
        public void ParsePrivitive_Type_InvalidType()
        {
            var lines = new[]
            {
                "Value: WickedFlame.Yaml.Tests.TypeConvert.InvalidType, WickedFlame.Yaml.Tests"
            };

            var reader = new YamlReader();
            Assert.Throws<InvalidConfigurationException>(() => reader.Read<PrimitiveValues>(lines));
        }

        [Test]
        public void ParsePrivitive_Type_Null()
        {
            var lines = new[]
            {
                "Value: "
            };

            var reader = new YamlReader();
            Assert.Throws<InvalidConfigurationException>(() => reader.Read<PrimitiveValues>(lines));
        }

        [Test]
        public void ParsePrivitive_TypeList()
        {
            var lines = new[]
            {
                "ValueList:",
                "  - WickedFlame.Yaml.Tests.TypeConvert.ParseTypeTests+PrimitiveValues, WickedFlame.Yaml.Tests",
                "  - WickedFlame.Yaml.Tests.TypeConvert.ParseTypeTests, WickedFlame.Yaml.Tests"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(typeof(PrimitiveValues), parsed.ValueList[0]);
            Assert.AreEqual(typeof(ParseTypeTests), parsed.ValueList[1]);
        }

        public class PrimitiveValues
        {
            public Type Value { get; set; }

            public List<Type> ValueList { get; set; }
        }
    }
}
