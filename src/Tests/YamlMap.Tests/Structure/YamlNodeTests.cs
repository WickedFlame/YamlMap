using System.Linq;
using System.Text;

namespace YamlMap.Tests;

public class YamlNodeTests
{
    
    [Test]
    public void YamlNode_IEnumerable_List_String()
    {
        var sb = new StringBuilder()
            .AppendLine("Single:")
            .AppendLine("  - value 1")
            .AppendLine("  - value 2");
        
        var node = Serializer.Deserialize<YamlNode>(sb.ToString());
        node["Single"].Nodes.Count().Should().Be(2);
    }
    
    [Test]
    public void YamlNode_IEnumerable_List_String_Keys()
    {
        var sb = new StringBuilder()
            .AppendLine("Single:")
            .AppendLine("  - value 1")
            .AppendLine("  - value 2");
        
        var node = Serializer.Deserialize<YamlNode>(sb.ToString());
        node["Single"].Nodes.All(n => string.IsNullOrEmpty(n.Key)).Should().BeTrue();
    }
    
    [Test]
    public void YamlNode_IEnumerable_Properites()
    {
        var sb = new StringBuilder()
            .AppendLine("First: one")
            .AppendLine("Second: two");
        
        var node = Serializer.Deserialize<YamlNode>(sb.ToString());
        node.Nodes.Count().Should().Be(2);

        node.Nodes.First().Key.Should().Be("First");
        node.Nodes.Last().Key.Should().Be("Second");
    }
    
    [Test]
    public void YamlNode_IEnumerable_Values()
    {
        var sb = new StringBuilder()
            .AppendLine("First: one")
            .AppendLine("Second: two");
        
        var node = Serializer.Deserialize<YamlNode>(sb.ToString());
        node.Nodes.Count().Should().Be(2);

        node.Nodes.First().Value.Should().Be("one");
        node.Nodes.Last().Value.Should().Be("two");
    }
}