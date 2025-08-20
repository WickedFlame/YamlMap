using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace YamlMap.Tests.Reader
{
    [TestFixture]
    public class ParseQuotatedTypeStringTests
	{
		[Test]
		public void ParseQuotatedTypeString_DoubleQuotations()
		{
			var lines = new[]
			{
				"Value: \"[value] test\""
			};

			var reader = new YamlReader();
			var parsed = reader.Read(typeof(PrimitiveValues), lines) as PrimitiveValues;

			Assert.That("[value] test", Is.EqualTo(parsed.Value));
		}

		[Test]
		public void ParseQuotatedTypeString_SingleQuotations()
		{
			var lines = new[]
			{
				"Value: '[value] test'"
			};

			var reader = new YamlReader();
			var parsed = reader.Read(typeof(PrimitiveValues), lines) as PrimitiveValues;

			Assert.That("[value] test", Is.EqualTo(parsed.Value));
		}

		[Test]
		public void ParseQuotatedTypeString_DoubleQuotations_InnerSingle()
		{
			var lines = new[]
			{
				"Value: \"[value] 'test\""
			};

			var reader = new YamlReader();
			var parsed = reader.Read(typeof(PrimitiveValues), lines) as PrimitiveValues;

			Assert.That("[value] 'test", Is.EqualTo(parsed.Value));
		}

		[Test]
		public void ParseQuotatedTypeString_SingleQuotations_InnerDouble()
		{
			var lines = new[]
			{
				"Value: '[value] \"test'"
			};

			var reader = new YamlReader();
			var parsed = reader.Read(typeof(PrimitiveValues), lines) as PrimitiveValues;

			Assert.That("[value] \"test", Is.EqualTo(parsed.Value));
		}

		[Test]
		public void ParseQuotatedTypeString_DoubleQuotations_NoEnd()
		{
			var lines = new[]
			{
				"Value: \"[value] test"
			};

			var reader = new YamlReader();
			Assert.Throws<InvalidConfigurationException>(() => reader.Read(typeof(PrimitiveValues), lines));
		}

		[Test]
		public void ParseQuotatedTypeString_SingleQuotations_NoEnd()
		{
			var lines = new[]
			{
				"Value: '[value] test"
			};

			var reader = new YamlReader();
			Assert.Throws<InvalidConfigurationException>(() => reader.Read(typeof(PrimitiveValues), lines));
		}

		[Test]
		public void ParseQuotatedTypeString_DoubleQuotations_EndInString()
		{
			var lines = new[]
			{
				"Value: \"[value] t\"est"
			};

			var reader = new YamlReader();
			Assert.Throws<InvalidConfigurationException>(() => reader.Read(typeof(PrimitiveValues), lines));
		}

		[Test]
		public void ParseQuotatedTypeString_SingleQuotations_EndInString()
		{
			var lines = new[]
			{
				"Value: '[value] t'est"
			};

			var reader = new YamlReader();
			Assert.Throws<InvalidConfigurationException>(() => reader.Read(typeof(PrimitiveValues), lines));
		}


		[Test]
		public void ParseQuotatedTypeString_DoubleQuotations_Special()
		{
			var lines = new[]
			{
				"Value: \"c: is a drive\""
			};

			var reader = new YamlReader();
			var parsed = reader.Read(typeof(PrimitiveValues), lines) as PrimitiveValues;

			Assert.That("c: is a drive", Is.EqualTo(parsed.Value));
		}

		public class PrimitiveValues
        {
            public string Value { get; set; }

            public List<string> ValueList { get; set; }
        }
    }
}
