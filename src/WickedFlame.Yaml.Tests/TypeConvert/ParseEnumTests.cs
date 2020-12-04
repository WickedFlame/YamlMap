using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace WickedFlame.Yaml.Tests.TypeConvert
{
    [TestFixture]
    public class ParseEnumTests
	{
        [Test]
        public void ParsePrivitive_Enum()
        {
            var lines = new[]
            {
                "Value: First"
            };
            
            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(EnumValue.First, parsed.Value);
        }

        [Test]
        public void ParsePrivitive_Enum_CaseInsensitive()
        {
	        var lines = new[]
	        {
		        "Value: fIRst"
	        };

	        var reader = new YamlReader();
	        var parsed = reader.Read<PrimitiveValues>(lines);

	        Assert.AreEqual(EnumValue.First, parsed.Value);
        }

		[Test]
        public void ParsePrivitive_Enum_Invalid()
        {
            var lines = new[]
            {
                "Value: test"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual(EnumValue.None, parsed.Value);
        }

        [Test]
        public void ParsePrivitive_EnumList()
        {
            var lines = new[]
            {
                "ValueList:",
                "  - First",
                "  - Second"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

			Assert.AreEqual(EnumValue.First, parsed.ValueList[0]);
			Assert.AreEqual(EnumValue.Second, parsed.ValueList[1]);
		}

        [Test]
        public void ParsePrivitive_EmptyEnum()
        {
	        var lines = new[]
	        {
				"Value:",
		        "ValueList:",
		        "  - First",
		        "  - Second"
	        };

	        var reader = new YamlReader();
	        var parsed = reader.Read<PrimitiveValues>(lines);

	        Assert.AreEqual(EnumValue.None, parsed.Value);
	        Assert.AreEqual(EnumValue.First, parsed.ValueList[0]);
	        Assert.AreEqual(EnumValue.Second, parsed.ValueList[1]);
        }

        [Test]
        public void ParsePrivitive_Enum_FromInt()
        {
	        var lines = new[]
	        {
		        "Value: 1",
		        "ValueList:",
		        "  - 0",
		        "  - 1",
		        "  - 2"
			};

	        var reader = new YamlReader();
	        var parsed = reader.Read<PrimitiveValues>(lines);

	        Assert.AreEqual(EnumValue.First, parsed.Value);

	        Assert.AreEqual(EnumValue.None, parsed.ValueList[0]);
			Assert.AreEqual(EnumValue.First, parsed.ValueList[1]);
			Assert.AreEqual(EnumValue.Second, parsed.ValueList[2]);
		}

		public enum EnumValue
        {
			None,
			First,
			Second
        }

		public class PrimitiveValues
        {
            public EnumValue Value { get; set; }

            public List<EnumValue> ValueList { get; set; }
        }
    }
}
