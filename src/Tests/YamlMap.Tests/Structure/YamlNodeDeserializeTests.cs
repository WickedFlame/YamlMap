using System.Text;

namespace YamlMap.Tests;

public class YamlNodeDeserializeTests
{
    [Test]
    public void YamlNode_Deserialize_ValueType()
    {
        var node = Serializer.Deserialize<YamlNode>("Single: single");
        node["Single"].Value.Should().Be("single");
    }
    
    [Test]
    public void YamlNode_Deserialize_ValueType_ItemArray()
    {
        var node = Serializer.Deserialize<YamlNode>("Single: single");
        node["Single"]["Single"].Should().BeNull();
    }
    
    [Test]
    public void YamlNode_Deserialize_Object_ValueType()
    {
        var sb = new StringBuilder()
            .AppendLine("Single:")
            .AppendLine("  Name: single");

        var node = Serializer.Deserialize<YamlNode>(sb.ToString());
        node["Single"]["Name"].Value.Should().Be("single");
    }
    
    [Test]
    public void YamlNode_Deserialize_Object_ValueType_Sub()
    {
        var sb = new StringBuilder()
            .AppendLine("Single:")
            .AppendLine("  value:")
            .AppendLine("    Name: single")
            .AppendLine("    Title: single title");
        
        var node = Serializer.Deserialize<YamlNode>(sb.ToString());
        node["Single"]["value"]["Name"].Value.Should().Be("single");
        node["Single"]["value"]["Title"].Value.Should().Be("single title");
    }
    
    [Test]
    public void YamlNode_Deserialize_Object_ValueType_Sub_NotValid()
    {
        var sb = new StringBuilder()
            .AppendLine("Single:")
            .AppendLine("  value:")
            .AppendLine("    Name: single")
            .AppendLine("    Title: single title");
        
        var node = Serializer.Deserialize<YamlNode>(sb.ToString());
        node["Single"]["value"]["Name"]["Sub"].Should().BeNull();
    }
    
    [Test]
    public void YamlNode_Deserialize_List_Object()
    {
        var sb = new StringBuilder()
            .AppendLine("Single:")
            .AppendLine("  - Name: single")
            .AppendLine("    Title: single title");
        
        var node = Serializer.Deserialize<YamlNode>(sb.ToString());
        node["Single"][0]["Name"].Value.Should().Be("single");
        node["Single"][0]["Title"].Value.Should().Be("single title");
    }
    
    [Test]
    public void YamlNode_Deserialize_Single_List_Sub()
    {
        var sb = new StringBuilder()
            .AppendLine("Single:")
            .AppendLine("  values:")
            .AppendLine("    - Name: single")
            .AppendLine("      Title: single title");
        
        var node = Serializer.Deserialize<YamlNode>(sb.ToString());
        node["Single"]["values"][0]["Name"].Value.Should().Be("single");
        node["Single"]["values"][0]["Title"].Value.Should().Be("single title");
    }
    
    [Test]
    public void YamlNode_Deserialize_List_String()
    {
        var sb = new StringBuilder()
            .AppendLine("Single:")
            .AppendLine("  - value 1")
            .AppendLine("  - value 2");
        
        var node = Serializer.Deserialize<YamlNode>(sb.ToString());
        node["Single"][0].Value.Should().Be("value 1");
        node["Single"][1].Value.Should().Be("value 2");
    }
}