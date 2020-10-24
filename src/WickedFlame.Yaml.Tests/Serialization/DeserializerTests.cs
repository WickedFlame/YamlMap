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

		public class TestlItem
		{
			public string Simple { get; set; }

			public TestlItem Child { get; set; }

			public IEnumerable<string> StringList { get; set; }

			public IEnumerable<TestlItem> ObjList { get; set; }
		}
	}
}
