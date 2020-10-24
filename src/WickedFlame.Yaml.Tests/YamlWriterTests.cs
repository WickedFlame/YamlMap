using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace WickedFlame.Yaml.Tests
{
    [TestFixture]
    public class YamlWriterTests
    {
        [Test]
        public void WickedFlame_Yaml_YamlWriter()
        {
            var item = new YamlItem
            {
                Simple = "Simple value"
            };

            var reader = new YamlWriter();
            var data = reader.Write(item);

            Assert.AreEqual("Simple: Simple value", data);
        }

        //TODO: null property
        [Test]
        public void WickedFlame_Yaml_YamlWriter_null()
        {
            var item = new YamlItem
            {
                Simple = null
            };

            var reader = new YamlWriter();
            var data = reader.Write(item);

            Assert.AreEqual("", data);
        }

        //TODO: protected/private property
        [Test]
        public void WickedFlame_Yaml_YamlWriter_Child()
        {
            var item = new YamlItem
            {
                Simple = "root",
                Child = new YamlItem
                {
                    Simple = "not root"
                }
            };

            var reader = new YamlWriter();
            var data = reader.Write(item);

            var sb = new StringBuilder();
            sb.AppendLine("Simple: root");
            sb.AppendLine("Child:");
            sb.Append("  Simple: not root");

            Assert.AreEqual(sb.ToString(), data);
        }

        [Test]
        public void WickedFlame_Yaml_YamlWriter_StringArry()
        {
	        var item = new YamlItem
	        {
		        Simple = "root",
		        StringList = new [] {"one", "2"}
	        };

	        var reader = new YamlWriter();
	        var data = reader.Write(item);

	        var sb = new StringBuilder();
	        sb.AppendLine("Simple: root");
	        sb.AppendLine("StringList:");
	        sb.AppendLine("  - one");
	        sb.Append("  - 2");

			Assert.AreEqual(sb.ToString(), data);
        }

        [Test]
        public void WickedFlame_Yaml_YamlWriter_ObjectList()
        {
	        var item = new YamlItem
	        {
		        Simple = "root",
		        ObjList = new List<YamlItem>
		        {
			        new YamlItem {Simple = "one"},
			        new YamlItem {Child = new YamlItem {Simple = "child"}}
		        }
	        };

	        var reader = new YamlWriter();
	        var data = reader.Write(item);

	        var sb = new StringBuilder();
	        sb.AppendLine("Simple: root");
	        sb.AppendLine("ObjList:");
	        sb.AppendLine("  - Simple: one");
	        sb.AppendLine("  - Child:");
	        sb.Append("      Simple: child");

	        Assert.AreEqual(sb.ToString(), data);
        }

        [Test]
        public void WickedFlame_Yaml_YamlWriter_String_SpecialChars()
        {
	        var reader = new YamlWriter();
	        var data = reader.Write(new { Value = "tes:one" });
	        Assert.AreEqual("Value: 'tes:one'", data);

	        data = reader.Write(new { Value = "tes[one" });
	        Assert.AreEqual("Value: 'tes[one'", data);

	        data = reader.Write(new { Value = "tes]one" });
	        Assert.AreEqual("Value: 'tes]one'", data);
		}

        [Test]
        public void WickedFlame_Yaml_YamlWriter_StringList_SpecialChars()
        {
	        var reader = new YamlWriter();
	        var data = reader.Write(new { Values = new[]
	        {
		        "tes:one" ,
		        "tes[one",
				"tes]one"}
			});

			var sb = new StringBuilder()
				.AppendLine("Values:")
				.AppendLine("  - 'tes:one'")
				.AppendLine("  - 'tes[one'")
				.Append("  - 'tes]one'");

			Assert.AreEqual(sb.ToString(), data);
		}

		[Test]
        public void WickedFlame_Yaml_YamlWriter_Type()
        {
	        var reader = new YamlWriter();
	        var data = reader.Write(new { Type = typeof(YamlWriter) });
	        Assert.AreEqual("Type: WickedFlame.Yaml.YamlWriter, WickedFlame.Yaml", data);
        }

		public class YamlItem
        {
            public string Simple { get; set; }

            public YamlItem Child { get; set; }

			public IEnumerable<string> StringList { get; set; }

			public IEnumerable<YamlItem> ObjList { get; set; }
        }
    }
}
