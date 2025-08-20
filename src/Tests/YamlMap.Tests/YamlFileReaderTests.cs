using NUnit.Framework;
using Polaroider;

namespace YamlMap.Tests
{
    [TestFixture]
    public class YamlFileReaderTests
    {
        [Test]
        public void YamlMap_YamlReader_SimpleProperty_Root()
        {
            var reader = new YamlFileReader();
            var data = reader.Read<YamlRoot>("YamlTest.yml");

            Assert.That("id", Is.EqualTo(data.Id));
        }

        [Test]
        public void Serializer_DeserializeFromFile()
        {
            Serializer.DeserializeFromFile<YamlRoot>("NestedObjectList.yml").MatchSnapshot();
        }
    }
}
