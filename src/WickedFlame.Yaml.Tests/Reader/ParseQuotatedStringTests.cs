using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace WickedFlame.Yaml.Tests.Reader
{
    [TestFixture]
    public class ParseQuotatedStringTests
    {
		[Test]
		public void ParseQuotatedString_DoubleQuotations()
		{
			var lines = new[]
			{
				"Value: \"[value] test\""
			};

			var reader = new YamlReader();
			var parsed = reader.Read<PrimitiveValues>(lines);

			Assert.AreEqual("[value] test", parsed.Value);
		}

		[Test]
		public void ParseQuotatedString_SingleQuotations()
		{
			var lines = new[]
			{
				"Value: '[value] test'"
			};

			var reader = new YamlReader();
			var parsed = reader.Read<PrimitiveValues>(lines);

			Assert.AreEqual("[value] test", parsed.Value);
		}

		[Test]
		public void ParseQuotatedString_DoubleQuotations_InnerSingle()
		{
			var lines = new[]
			{
				"Value: \"[value] 'test\""
			};

			var reader = new YamlReader();
			var parsed = reader.Read<PrimitiveValues>(lines);

			Assert.AreEqual("[value] 'test", parsed.Value);
		}

		[Test]
		public void ParseQuotatedString_SingleQuotations_InnerDouble()
		{
			var lines = new[]
			{
				"Value: '[value] \"test'"
			};

			var reader = new YamlReader();
			var parsed = reader.Read<PrimitiveValues>(lines);

			Assert.AreEqual("[value] \"test", parsed.Value);
		}

		[Test]
		public void ParseQuotatedString_DoubleQuotations_NoEnd()
		{
			var lines = new[]
			{
				"Value: \"[value] test"
			};

			var reader = new YamlReader();
			Assert.Throws<InvalidConfigurationException>(() => reader.Read<PrimitiveValues>(lines));
		}

		[Test]
		public void ParseQuotatedString_SingleQuotations_NoEnd()
		{
			var lines = new[]
			{
				"Value: '[value] test"
			};

			var reader = new YamlReader();
			Assert.Throws<InvalidConfigurationException>(() => reader.Read<PrimitiveValues>(lines));
		}

		[Test]
		public void ParseQuotatedString_DoubleQuotations_EndInString()
		{
			var lines = new[]
			{
				"Value: \"[value] t\"est"
			};

			var reader = new YamlReader();
			Assert.Throws<InvalidConfigurationException>(() => reader.Read<PrimitiveValues>(lines));
		}

		[Test]
		public void ParseQuotatedString_SingleQuotations_EndInString()
		{
			var lines = new[]
			{
				"Value: '[value] t'est"
			};

			var reader = new YamlReader();
			Assert.Throws<InvalidConfigurationException>(() => reader.Read<PrimitiveValues>(lines));
		}


		[Test]
		public void ParseQuotatedString_DoubleQuotations_Special()
		{
			var lines = new[]
			{
				"Value: \"c: is a drive\""
			};

			var reader = new YamlReader();
			var parsed = reader.Read<PrimitiveValues>(lines);

			Assert.AreEqual("c: is a drive", parsed.Value);
		}

		public class PrimitiveValues
        {
            public string Value { get; set; }

            public List<string> ValueList { get; set; }
        }
    }
}
