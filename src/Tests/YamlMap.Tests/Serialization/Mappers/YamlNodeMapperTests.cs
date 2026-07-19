using YamlMap.Serialization.Mappers;

namespace YamlMap.Tests.Serialization.Mappers;

public class YamlNodeMapperTests
{
    [Test]
    public void YamlNodeMapper_Map()
    {
        var mapper = new YamlNodeMapper();
        mapper.Map(new ValueToken("Value", "String", 0), new YamlNode())
            .Should().BeTrue();
    }
    
    [Test]
    public void YamlNodeMapper_Map_Value()
    {
        var node = new YamlNode();
        var mapper = new YamlNodeMapper();
        mapper.Map(new ValueToken("Value", "String", 0), node);
        
        node["Value"].Value.Should().Be("String");
    }
    
    [Test]
    public void YamlNodeMapper_Map_Token()
    {
        var token = new Token("tmp", 0);
        token.Set(new ValueToken("Key", "Value", 0));
        
        var mapper = new YamlNodeMapper();
        mapper.Map(token, new YamlNode())
            .Should().BeTrue();
    }
    
    [Test]
    public void YamlNodeMapper_Map_Token_Value()
    {
        var node = new YamlNode();
        
        var token = new Token("tmp", 0);
        token.Set(new ValueToken("Key", "Value", 0));
        
        var mapper = new YamlNodeMapper();
        mapper.Map(token, node);
        
        node["tmp"]["Key"].Value.Should().Be("Value");
    }
    
    [Test]
    public void YamlNodeMapper_Map_ListToken_Value()
    {
        var node = new YamlNode();
        var list = new Token(null, 2, TokenType.ListItem);
        list.Set(new ValueToken("Key", "Value", 4));
        var token = new Token("tmp", 0, TokenType.Object);
        token.Set(list);
        
        var mapper = new YamlNodeMapper();
        mapper.Map(token, node);
        
        node["tmp"][0]["Key"].Value.Should().Be("Value");
    }
    
    [Test]
    public void YamlNodeMapper_Map_InvalidTypes()
    {
        var token = new Token("tmp", 0, TokenType.Value);
        token.Set(new ValueToken("Key", "Value", 0));
        
        var mapper = new YamlNodeMapper();
        mapper.Map(token, new YamlNode())
            .Should().BeFalse();
    }
}