using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Polaroider;

namespace WickedFlame.Yaml.Tests.Serialization
{
	public class DeserializerTests
	{
		[Test]
		public void Serializer_Deserialize()
		{
			var value = @"Simple: root
StringList:
  - one
  - 2
ObjList:
  - Simple: one
  - Child:
      Simple: child";

			var item = Serializer.Deserialize<TestlItem>(value);
			item.MatchSnapshot();
		}

		[Test]
		public void Deserialize_Type()
		{
			var item = Serializer.Deserialize<DeserializerType>("Type: WickedFlame.Yaml.Tests.Serialization.DeserializerType, WickedFlame.Yaml.Tests");
			Assert.AreEqual(typeof(DeserializerType), item.Type);
		}

		[Test]
		public void Deserialize_GenericType()
		{
			var item = Serializer.Deserialize<DeserializerType>("Type: \"WickedFlame.Yaml.Tests.Serialization.GenericType`1[[WickedFlame.Yaml.Tests.Serialization.DeserializerType, WickedFlame.Yaml.Tests]], WickedFlame.Yaml.Tests\"");
			Assert.AreEqual(typeof(GenericType<DeserializerType>), item.Type);
		}

		[Test]
		[Ignore("not implemented")]
		public void Deserialize_AnonymousType()
		{
			var value = @"Value: simple value";
			var item = Serializer.Deserialize(() => new { Value = "" }, value);
			Assert.AreEqual("simple value", item.Value);
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
