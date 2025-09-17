using System.Collections;
using System.Collections.Generic;
using YamlMap.Serialization.Mappers;
using ArrayMapper = YamlMap.Serialization.Mappers.ArrayMapper;

namespace YamlMap.Tests.Serialization.Mappers
{
    [TestFixture]
    public class MapperFactoryTests
    {
        [Test]
        public void MapperFactory_List()
        {
            var mapper = MapperFactory.GetObjectMapper(new List<MapperItem>(), typeof(MapperItem));
            mapper.Should().BeOfType<GenericListMapper>();
        }

        [Test]
        public void MapperFactory_Dictionary()
        {
            var mapper = MapperFactory.GetObjectMapper(new Dictionary<string, string>(), typeof(MapperItem));
            mapper.Should().BeOfType<GenericDictionaryMapper>();
        }

        [Test]
        public void MapperFactory_Array()
        {
            var mapper = MapperFactory.GetObjectMapper(new string[] {}, typeof(MapperItem));
            mapper.Should().BeOfType<ArrayMapper>();
        }

        [Test]
        public void MapperFactory_Array2()
        {
            var mapper = MapperFactory.GetObjectMapper(new ArrayList(), typeof(IList));
            mapper.Should().BeOfType<ArrayMapper>();
        }
        
        [Test]
        public void MapperFactory_YamlNode()
        {
            var mapper = MapperFactory.GetObjectMapper(new YamlNode(), typeof(YamlNode));
            mapper.Should().BeOfType<YamlNodeMapper>();
        }

        [Test]
        public void MapperFactory_Default()
        {
            var mapper = MapperFactory.GetObjectMapper(new MapperItem(), typeof(MapperItem));
            Assert.That(mapper, Is.InstanceOf<DefaultMapper>());
        }
        
        

        public class MapperItem { }
    }
}
