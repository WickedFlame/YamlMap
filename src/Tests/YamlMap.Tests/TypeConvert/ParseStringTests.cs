using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace YamlMap.Tests.TypeConvert
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

            Assert.That("value", Is.EqualTo(parsed.Value));
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

            Assert.That("one", Is.EqualTo(parsed.ValueList[0]));
            Assert.That("two", Is.EqualTo(parsed.ValueList[1]));
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

			Assert.That(string.Empty, Is.EqualTo(parsed.Value));
		}
		
		public class PrimitiveValues
        {
            public string Value { get; set; }

            public List<string> ValueList { get; set; }
        }
    }
}
