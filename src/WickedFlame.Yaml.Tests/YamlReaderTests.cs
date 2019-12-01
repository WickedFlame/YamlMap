using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WickedFlame.Yaml;
using NUnit.Framework;

namespace WickedFlame.Yaml.Tests
{
    [TestFixture]
    public class YamlReaderTests
    {
        [Test]
        public void WickedFlame_Yaml_YamlReader_SimpleProperty_Root()
        {
            var reader = new YamlReader();
            var data = reader.Read<YamlRoot>("YamlTest.yml");

            Assert.AreEqual("id", data.Id);
        }

        [Test]
        public void WickedFlame_Yaml_YamlReader_NoProperty()
        {
            var lines = new[]
            {
                "Id: InvalidProperty",
                "InexistentProperty: fail"
            };
            var reader = new YamlReader();

            Assert.Throws<InvalidConfigurationException>(() => reader.Read<YamlRoot>(lines));
        }
    }
}
