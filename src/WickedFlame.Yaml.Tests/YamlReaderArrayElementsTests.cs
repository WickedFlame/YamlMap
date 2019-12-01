using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WickedFlame.Yaml;
using NUnit.Framework;

namespace WickedFlame.Yaml.Tests
{
    [TestFixture]
    public class YamlReaderArrayElementsTests
    {
        [Test]
        public void WickedFlame_Yaml_YamlReader_Array_SimpleProperty_Root()
        {
            var lines = new[]
            {
                "Id: id"
            };

            var reader = new YamlReader();
            var data = reader.Read<YamlRoot>(lines);

            Assert.AreEqual("id", data.Id);
        }

        [Test]
        public void WickedFlame_Yaml_YamlReader_Array_ObjectProperty_Root()
        {
            var lines = new[]
            {
                "Id: id",
                "SimpleObject:",
                "  Name: Object name"
            };

            var reader = new YamlReader();
            var data = reader.Read<YamlRoot>(lines);

            Assert.IsNotNull(data.SimpleObject);
            Assert.AreEqual("Object name", data.SimpleObject.Name);
        }

        [Test]
        public void WickedFlame_Yaml_YamlReader_Array_ObjectProperty_Nested()
        {
            var lines = new[]
            {
                "Id: id",
                "Nested:",
                "  Nested:",
                "     Name: nested object",
                "  Name: first object"
            };

            var reader = new YamlReader();
            var data = reader.Read<YamlRoot>(lines);

            Assert.IsNotNull(data.Nested);
            Assert.IsNotNull(data.Nested.Nested);
            Assert.AreEqual("nested object", data.Nested.Nested.Name);
        }

        [Test]
        public void WickedFlame_Yaml_YamlReader_Array_ObjectProperty_AfterNested()
        {
            var lines = new[]
            {
                "Id: id",
                "Nested:",
                "  Nested:",
                "     Name: nested object",
                "  Name: first object"
            };

            var reader = new YamlReader();
            var data = reader.Read<YamlRoot>(lines);

            Assert.AreEqual("first object", data.Nested.Name);
        }

        [Test]
        public void WickedFlame_Yaml_YamlReader_Array_StringList()
        {
            var lines = new[]
            {
                "Id: stringlist",
                "StringList:",
                "  - one",
                "  - two",
                "  - three"
            };

            var reader = new YamlReader();
            var data = reader.Read<YamlRoot>(lines);

            Assert.That(data.StringList.Count == 3);
            Assert.That(data.StringList.Contains("one"));
            Assert.That(data.StringList.Contains("two"));
            Assert.That(data.StringList.Contains("three"));
        }

        [Test]
        public void WickedFlame_Yaml_YamlReader_Array_EnumerableList()
        {
            var lines = new[]
            {
                "Id: EnumerableList",
                "EnumerableList:",
                "  - one",
                "  - two",
                "  - three"
            };

            var reader = new YamlReader();
            var data = reader.Read<YamlRoot>(lines);

            Assert.That(data.EnumerableList.Count() == 3);
            Assert.That(data.EnumerableList.Contains("one"));
            Assert.That(data.EnumerableList.Contains("two"));
            Assert.That(data.EnumerableList.Contains("three"));
        }

        [Test]
        public void WickedFlame_Yaml_YamlReader_Array_ObjectList()
        {
            var lines = new[]
            {
                "Id: id",
                "ObjectList:",
                "  - Name: one",
                "    Id: 1",
                "  - Name: two",
                "    Id: 2"
            };

            var reader = new YamlReader();
            var data = reader.Read<YamlRoot>(lines);

            Assert.That(data.ObjectList.Count() == 2);
            Assert.That(data.ObjectList.First().Name == "one" && data.ObjectList.First().Id == "1");
            Assert.That(data.ObjectList.Last().Name == "two" && data.ObjectList.Last().Id == "2");
        }

        [Test]
        public void WickedFlame_Yaml_YamlReader_Array_NestedObjectList()
        {
            var lines = new[]
            {
                "Id: id",
                "NestedObjectList:",
                "  - Nested:",
                "      Name: second",
                "      Nested:",
                "        Name: third",
                "    Name: first"
            };

            var reader = new YamlReader();
            var data = reader.Read<YamlRoot>(lines);

            Assert.That(data.NestedObjectList.First().Name == "first");
            Assert.That(data.NestedObjectList.First().Nested.Name == "second");
            Assert.That(data.NestedObjectList.First().Nested.Nested.Name == "third");
        }
    }


}
