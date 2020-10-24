using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Polaroider;

namespace WickedFlame.Yaml.Tests.Serialization
{
	public class SerializerTests
	{
		[Test]
		public void Serializer_Serialize()
		{
			var item = new TestlItem
			{
				Simple = "root",
				ObjList = new List<TestlItem>
				{
					new TestlItem {Simple = "one"},
					new TestlItem {Child = new TestlItem {Simple = "child"}}
				},
				StringList = new[] { "one", "2" }
			};

			var serialized = Serializer.Serialize(item);
			serialized.MatchSnapshot();
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
