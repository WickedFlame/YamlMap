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

        public class YamlItem
        {
            public string Simple { get; set; }

            public YamlItem Child { get; set; }
        }
    }
}
