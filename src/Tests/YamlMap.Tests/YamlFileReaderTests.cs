using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using YamlMap;
using NUnit.Framework;

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

            Assert.AreEqual("id", data.Id);
        }
    }
}
