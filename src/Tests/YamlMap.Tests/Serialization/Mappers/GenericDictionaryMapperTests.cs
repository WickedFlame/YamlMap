using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using YamlMap.Serialization.Mappers;
using ArrayMapper = YamlMap.Serialization.Mappers.ArrayMapper;

namespace YamlMap.Tests.Serialization.Mappers
{
    [TestFixture]
    public class GenericDictionaryMapperTests
    {
        [Test]
        public void WickedFlame_Yaml_Serialization_GenericDictionaryMapper_Simple()
        {
            var token = new ValueToken("Key", "Value", 0);
            var list = new Dictionary<string, string>();

            var mapper = new GenericDictionaryMapper();
            var ok = mapper.Map(token, list);

            Assert.IsTrue(ok);
            Assert.AreEqual("Value", list["Key"]);
        }

        [Test]
        public void WickedFlame_Yaml_Serialization_GenericDictionaryMapper()
        {

            var token = new Token("Key", 0);
            token.Set(new ValueToken("Key", "Value", 0));

            var item = new Dictionary<string, ArrayMapperItem>();

            var mapper = new GenericDictionaryMapper();
            var ok = mapper.Map(token, item);

            Assert.IsTrue(ok);
            Assert.AreEqual("Value", item["Key"].Key);
        }

        [Test]
        public void WickedFlame_Yaml_Serialization_GenericDictionaryMapper_NoDictionary()
        {

            var token = new Token("Key", 0);
            token.Set(new ValueToken("Key", "Value", 0));

            var item = new List<ArrayMapperItem>();

            var mapper = new GenericDictionaryMapper();
            var ok = mapper.Map(token, item);

            Assert.IsFalse(ok);
        }

        public class ArrayMapperItem
        {
            public string Key { get; set; }
        }
    }
}
