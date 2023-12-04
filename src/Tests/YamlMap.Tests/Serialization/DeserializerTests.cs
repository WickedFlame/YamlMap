using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Polaroider;

namespace YamlMap.Tests.Serialization
{
	public class DeserializerTests
	{
        [Test]
        public void Serializer_Deserialize()
        {

            var value = new StringBuilder().AppendLine("Simple: root")
                .AppendLine("StringList:")
                .AppendLine("  - one")
                .AppendLine("  - 2")
                .AppendLine("ObjList:")
                .AppendLine("  - Simple: simple")
                .AppendLine("  - Child:")
                .AppendLine("      Simple: child").ToString();

            var item = Serializer.Deserialize<TestlItem>(value);
            item.MatchSnapshot();
        }

        [Test]
        public void Serializer_Deserialize_ByType()
        {

            var value = new StringBuilder().AppendLine("Simple: root")
                .AppendLine("StringList:")
                .AppendLine("  - one")
                .AppendLine("  - 2")
                .AppendLine("ObjList:")
                .AppendLine("  - Simple: simple")
                .AppendLine("  - Child:")
                .AppendLine("      Simple: child").ToString();

            var item = Serializer.Deserialize(typeof(TestlItem), value);
            item.MatchSnapshot();
        }

        [Test]
		public void Deserialize_Type()
		{
			var item = Serializer.Deserialize<DeserializerType>("Type: YamlMap.Tests.Serialization.DeserializerType, YamlMap.Tests");
			Assert.That(typeof(DeserializerType), Is.EqualTo(item.Type));
		}

		[Test]
		public void Deserialize_GenericType()
		{
			var item = Serializer.Deserialize<DeserializerType>("Type: \"YamlMap.Tests.Serialization.GenericType`1[[YamlMap.Tests.Serialization.DeserializerType, YamlMap.Tests]], YamlMap.Tests\"");
			Assert.That(typeof(GenericType<DeserializerType>), Is.EqualTo(item.Type));
		}


		public class TestlItem
		{
			public string Simple { get; set; }

			public TestlItem Child { get; set; }

			public IEnumerable<string> StringList { get; set; }

			public IEnumerable<TestlItem> ObjList { get; set; }
		}
	}

	public class DeserializerType
	{
		public Type Type { get; set; }
	}

	public class GenericType<T>{}
}
