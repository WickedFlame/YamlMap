using YamlMap.Scanning;

namespace YamlMap.Tests.Scanning;

public class FoldedMultilineReaderTests
{
    private ITokenReader _reader;

    [SetUp]
    public void Setup()
    {
        _reader = new FoldedMultilineTokenReader();
    }
    
    [Test]
    public void FoldedMultilineTokenReader_IndexOfNext()
    {
        _reader.IndexOfNext("  Property: >")
            .Should().BeGreaterThan(0);
    }
        
    [Test]
    public void FoldedMultilineTokenReader_IndexOfNext_Ext()
    {
        _reader.IndexOfNext("  Property: >2")
            .Should().BeGreaterThan(0);
    }
        
    [Test]
    public void FoldedMultilineTokenReader_IndexOfNext_None()
    {
        _reader.IndexOfNext("  Property: test")
            .Should().Be(-1);
    }
    
    [Test]
    public void FoldedMultilineTokenReader_Read()
    {
        var lines = new[]
        {
            "First: single line",
            "Value: >",
            "  line 1",
            "  line 2",
            "  line 3"
        };
            
        var scanner = new Scanner(lines);
        scanner.AddToIndex(2);
            
        _reader.Read(scanner, "Value: >")
            .Should().Be($"Value: line 1 line 2 line 3");
    }
    
    [Test]
    public void FoldedMultilineTokenReader_Read_Indented()
    {
        var lines = new[]
        {
            "First: single line",
            "Value: >",
            "  line 1",
            "   line 2",
            "    line 3"
        };
            
        var scanner = new Scanner(lines);
        scanner.AddToIndex(2);
            
        _reader.Read(scanner, "Value: >")
            .Should().Be($"Value: line 1  line 2   line 3");
    }
    
    [Test]
    public void FoldedMultilineTokenReader_Read_EmptyLine()
    {
        var lines = new[]
        {
            "First: single line",
            "Value: >",
            "  line 1",
            "  line 2",
            "  ",
            "  line 3"
        };
            
        var scanner = new Scanner(lines);
        scanner.AddToIndex(2);
            
        //
        // adds a space before line 3
        _reader.Read(scanner, "Value: >")
            .Should().Be($"Value: line 1 line 2  line 3");
    }
    
    [Test]
    public void FoldedMultilineTokenReader_Read_MultipleProperties()
    {
        var lines = new[]
        {
            "First: single line",
            "Value: >",
            "  line 1",
            "  line 2",
            "  line 3",
            "Last: single line"
        };
            
        var scanner = new Scanner(lines);
        scanner.AddToIndex(2);
            
        _reader.Read(scanner, "Value: >")
            .Should().Be($"Value: line 1 line 2 line 3");
    }
    
    [Test]
    public void FoldedMultilineTokenReader_Read_Plus()
    {
        var lines = new[]
        {
            "First: single line",
            "Value: >+",
            "  line 1",
            "  line 2",
            "  line 3"
        };
            
        var scanner = new Scanner(lines);
        scanner.AddToIndex(2);
            
        _reader.Read(scanner, "Value: >+")
            .Should().Be($"Value: line 1 line 2 line 3");
    }
}