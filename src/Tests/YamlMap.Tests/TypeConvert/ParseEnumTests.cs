using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using YamlMap;

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

            Assert.That(EnumValue.First, Is.EqualTo(parsed.Value)   );
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

	        Assert.That(EnumValue.First, Is.EqualTo(parsed.Value));
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

            Assert.That(EnumValue.None, Is.EqualTo(parsed.Value));
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

			Assert.That(EnumValue.First, Is.EqualTo(parsed.ValueList[0]));
			Assert.That(EnumValue.Second, Is.EqualTo(parsed.ValueList[1]));
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

	        Assert.That(EnumValue.None, Is.EqualTo(parsed.Value));
	        Assert.That(EnumValue.First, Is.EqualTo(parsed.ValueList[0]));
	        Assert.That(EnumValue.Second, Is.EqualTo(parsed.ValueList[1]));
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

	        Assert.That(EnumValue.First, Is.EqualTo(parsed.Value));

	        Assert.That(EnumValue.None, Is.EqualTo(parsed.ValueList[0]));
			Assert.That(EnumValue.First, Is.EqualTo(parsed.ValueList[1]));
			Assert.That(EnumValue.Second, Is.EqualTo(parsed.ValueList[2]));
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
