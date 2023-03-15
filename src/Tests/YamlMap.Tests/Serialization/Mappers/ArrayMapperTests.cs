using FluentAssertions;
using NUnit.Framework;
using YamlMap.Serialization.Mappers;

namespace YamlMap.Tests.Serialization.Mappers
{
    [TestFixture]
    public class ArrayMapperTests
    {
        [Test]
        public void YamlMap_Serialization_ArrayMapper_Simple()
        {
            var token = new ValueToken("Key", "Value", 0);
            var list = new string[1];

            var mapper = new ArrayMapper();
            var ok = mapper.Map(token, list);

            Assert.IsTrue(ok);
            Assert.AreEqual("Value", list[0]);
        }

        [Test]
        public void YamlMap_Serialization_ArrayMapper()
        {

            var token = new Token("tmp", 0);
            token.Set(new ValueToken("Key", "Value", 0));

            var item = new ArrayMapperItem[1];

            var mapper = new ArrayMapper();
            var ok = mapper.Map(token, item);

            Assert.IsTrue(ok);
            Assert.AreEqual("Value", item[0].Key);
        }

        [Test]
        public void ArrayMapper_Not_Array()
        {
            var token = new ValueToken("Key", "Value", 0);
            var list = "value";

            var mapper = new ArrayMapper();
            mapper.Map(token, list).Should().BeFalse();
        }



        public class ArrayMapperItem
        {
            public string Key { get; set; }
        }
    }
}
