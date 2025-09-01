using System.Collections.Generic;

namespace YamlMap.Tests
{
    public class IntegrationLineBreaks
    {
        [Test]
        public void Integrate_LineBreaks_Deserialize()
        {
            var yml = "List:\r\n  - 'https://yamlmap.io/'";
            var list = YamlMap.Serializer.Deserialize<StringList>(yml);
            
            list.List.Should().HaveCount(1);
        }

        public class StringList
        {
            public List<string> List { get; set; }
        }
    }
}
