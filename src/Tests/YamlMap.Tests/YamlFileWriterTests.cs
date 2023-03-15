using System.IO;
using System.Reflection;
using NUnit.Framework;
using Polaroider;

namespace YamlMap.Tests
{
    public class YamlFileWriterTests
    {
        private string _path;

        [SetUp]
        public void Setup()
        {
            var path = Assembly.GetExecutingAssembly().Location;
            _path = Path.Combine(Path.GetDirectoryName(path), "WriterTest.yml");

            if (File.Exists(_path))
            {
                File.Delete(_path);
            }
        }

        [Test]
        public void YamlFileWriter_Write()
        {
            var item = new YamlRoot
            {
                Id = "writer test"
            };

            var writer = new YamlFileWriter();
            writer.Write(_path, item);

            File.ReadAllText(_path).MatchSnapshot();
        }

        [Test]
        public void Serializer_SerializeToFile()
        {
            var item = new YamlRoot
            {
                Id = "writer test"
            };

            Serializer.SerializeToFile(_path, item);

            File.ReadAllText(_path).MatchSnapshot();
        }
    }
}
