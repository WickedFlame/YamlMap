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
        [SetUp]
        public void Setup()
        {
            var path = Path.Combine(Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)), "WriterTest.yml");
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        [Test]
        public void YamlFileWriter_Write()
        {
            var item = new YamlRoot
            {
                Id = "writer test"
            };

            var path = Path.Combine(Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path)), "WriterTest.yml");

            var writer = new YamlFileWriter();
            writer.Write(path, item);

            File.ReadAllText(path).MatchSnapshot();
        }
    }
}
