using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YamlMap;
using NUnit.Framework;

namespace YamlMap.Tests
{
    [TestFixture]
    public class YamlFileReaderFileElementsTests
    {
        [Test]
        public void YamlFileReader_File_SimpleProperty_Root()
        {
            var reader = new YamlFileReader();
            var data = reader.Read<YamlRoot>("SimpleProperty.yml");

            Assert.AreEqual("id", data.Id);
        }

        [Test]
        public void YamlFileReader_File_ObjectProperty_Root()
        {
            var reader = new YamlFileReader();
            var data = reader.Read<YamlRoot>("ObjectProperty.yml");

            Assert.IsNotNull(data.SimpleObject);
            Assert.AreEqual("Object name", data.SimpleObject.Name);
        }

        [Test]
        public void YamlFileReader_File_ObjectProperty_Nested()
        {
            var reader = new YamlFileReader();
            var data = reader.Read<YamlRoot>("Nested.yml");

            Assert.IsNotNull(data.Nested);
            Assert.IsNotNull(data.Nested.Nested);
            Assert.AreEqual("nested object", data.Nested.Nested.Name);
        }

        [Test]
        public void YamlFileReader_File_ObjectProperty_AfterNested()
        {
            var reader = new YamlFileReader();
            var data = reader.Read<YamlRoot>("Nested.yml");

            Assert.AreEqual("first object", data.Nested.Name);
        }

        [Test]
        public void YamlFileReader_File_StringList()
        {
            var reader = new YamlFileReader();
            var data = reader.Read<YamlRoot>("StringList.yml");

            Assert.That(data.StringList.Count == 3);
            Assert.That(data.StringList.Contains("one"));
            Assert.That(data.StringList.Contains("two"));
            Assert.That(data.StringList.Contains("three"));
        }

        [Test]
        public void YamlFileReader_File_EnumerableList()
        {
            var reader = new YamlFileReader();
            var data = reader.Read<YamlRoot>("EnumerableList.yml");

            Assert.That(data.EnumerableList.Count() == 3);
            Assert.That(data.EnumerableList.Contains("one"));
            Assert.That(data.EnumerableList.Contains("two"));
            Assert.That(data.EnumerableList.Contains("three"));
        }

        [Test]
        public void YamlFileReader_File_ObjectList()
        {
            var reader = new YamlFileReader();
            var data = reader.Read<YamlRoot>("ObjectList.yml");

            Assert.That(data.ObjectList.Count() == 2);
            Assert.That(data.ObjectList.First().Name == "one" && data.ObjectList.First().Id == "1");
            Assert.That(data.ObjectList.Last().Name == "two" && data.ObjectList.Last().Id == "2");
        }

        [Test]
        public void YamlFileReader_File_NestedObjectList()
        {
            var reader = new YamlFileReader();
            var data = reader.Read<YamlRoot>("NestedObjectList.yml");

            Assert.That(data.NestedObjectList.First().Name == "first");
            Assert.That(data.NestedObjectList.First().Nested.Name == "second");
            Assert.That(data.NestedObjectList.First().Nested.Nested.Name == "third");
        }
    }

    
}
