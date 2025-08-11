using System.Collections.Generic;
using Polaroider;

namespace YamlMap.Tests.Serialization
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

        [Test]
        public void Serializer_Null()
		{
			var item = new
			{
				Name = "item",
				Value = (string) null
			};

			Serializer.Serialize(item).Should().Be("Name: item");
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
