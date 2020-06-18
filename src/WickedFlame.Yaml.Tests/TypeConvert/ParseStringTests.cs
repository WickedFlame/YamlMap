using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace WickedFlame.Yaml.Tests.TypeConvert
{
    [TestFixture]
    public class ParseStringTests
    {
        [Test]
        public void ParsePrivitive_String()
        {
            var lines = new[]
            {
                "Value: value "
            };
            
            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual("value", parsed.Value);
        }

        [Test]
        public void ParsePrivitive_StringList()
        {
            var lines = new[]
            {
                "ValueList:",
                "  - one",
                "  - two"
            };

            var reader = new YamlReader();
            var parsed = reader.Read<PrimitiveValues>(lines);

            Assert.AreEqual("one", parsed.ValueList[0]);
            Assert.AreEqual("two", parsed.ValueList[1]);
        }

		[Test]
		public void ParsePrivitive_EmptyString()
		{
			var lines = new[]
			{
				"Value:",
				"ValueList:",
				"  - one",
				"  - two"
			};

			var reader = new YamlReader();
			var parsed = reader.Read<PrimitiveValues>(lines);

			Assert.AreEqual(string.Empty, parsed.Value);
		}

		public class PrimitiveValues
        {
            public string Value { get; set; }

            public List<string> ValueList { get; set; }
        }
    }
}
