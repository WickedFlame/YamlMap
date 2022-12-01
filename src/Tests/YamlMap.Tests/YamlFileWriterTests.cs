using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
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
            var codeBase = Assembly.GetExecutingAssembly().Location;
            var uri = new UriBuilder(codeBase);
            var path = Uri.UnescapeDataString(uri.Path);
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
    }
}
