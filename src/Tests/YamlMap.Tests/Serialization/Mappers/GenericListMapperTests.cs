using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using YamlMap.Serialization.Mappers;
using ArrayMapper = YamlMap.Serialization.Mappers.ArrayMapper;

namespace YamlMap.Tests.Serialization.Mappers
{
    [TestFixture]
    public class GenericListMapperTests
    {
        [Test]
        public void YamlMap_Serialization_GenericListMapper_Simple()
        {
            var token = new ValueToken("Key", "Value", 0);
            var list = new List<string>();

            var mapper = new GenericListMapper();
            var ok = mapper.Map(token, list);

            Assert.IsTrue(ok);
            Assert.AreEqual("Value", list[0]);
        }

        [Test]
        public void YamlMap_Serialization_GenericListMapper()
        {

            var token = new Token("Key", 0);
            token.Set(new ValueToken("Key", "Value", 0));

            var item = new List<ArrayMapperItem>();

            var mapper = new GenericListMapper();
            var ok = mapper.Map(token, item);

            Assert.IsTrue(ok);
            Assert.AreEqual("Value", item[0].Key);
        }

        [Test]
        public void YamlMap_Serialization_GenericListMapper_NoList()
        {

            var token = new Token("Key", 0);
            token.Set(new ValueToken("Key", "Value", 0));

            var item = new ArrayMapperItem();

            var mapper = new GenericListMapper();
            var ok = mapper.Map(token, item);

            Assert.IsFalse(ok);
        }

        public class ArrayMapperItem
        {
            public string Key { get; set; }
        }
    }
}
