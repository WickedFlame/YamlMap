using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using YamlMap.Serialization.Mappers;
using ArrayMapper = YamlMap.Serialization.Mappers.ArrayMapper;

namespace YamlMap.Tests.Serialization.Mappers
{
    [TestFixture]
    public class MapperFactoryTests
    {
        [Test]
        public void YamlMap_Serialization_MapperFactory_List()
        {
            var mapper = MapperFactory.GetObjectMapper(new List<MapperItem>(), typeof(MapperItem));
            Assert.That(mapper, Is.InstanceOf<GenericListMapper>());
        }

        [Test]
        public void YamlMap_Serialization_MapperFactory_Dictionary()
        {
            var mapper = MapperFactory.GetObjectMapper(new Dictionary<string, string>(), typeof(MapperItem));
            Assert.That(mapper, Is.InstanceOf<GenericDictionaryMapper>());
        }

        [Test]
        public void YamlMap_Serialization_MapperFactory_Array()
        {
            var mapper = MapperFactory.GetObjectMapper(new string[] {}, typeof(MapperItem));
            Assert.That(mapper, Is.InstanceOf<ArrayMapper>());
        }

        [Test]
        public void YamlMap_Serialization_MapperFactory_Array2()
        {
            var mapper = MapperFactory.GetObjectMapper(new ArrayList(), typeof(IList));
            Assert.That(mapper, Is.InstanceOf<ArrayMapper>());
        }

        [Test]
        public void YamlMap_Serialization_MapperFactory_Default()
        {
            var mapper = MapperFactory.GetObjectMapper(new MapperItem(), typeof(MapperItem));
            Assert.That(mapper, Is.InstanceOf<DefaultMapper>());
        }

        public class MapperItem { }
    }
}
