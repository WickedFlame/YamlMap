using System;
using YamlMap.Scanning;

namespace YamlMap.Tests.Scanning
{
    public class LiteralMultilineTokenReaderTests
    {
        private ITokenReader _reader;

        [SetUp]
        public void Setup()
        {
            _reader = new LiteralMultilineTokenReader();
        }
        
        [Test]
        public void MultilineStringTokenReader_IndexOfNext()
        {
            _reader.IndexOfNext("  Property: |")
                .Should().BeGreaterThan(0);
        }
        
        [Test]
        public void MultilineStringTokenReader_IndexOfNext_Ext()
        {
            _reader.IndexOfNext("  Property: |2")
                .Should().BeGreaterThan(0);
        }
        
        [Test]
        public void MultilineStringTokenReader_IndexOfNext_None()
        {
            _reader.IndexOfNext("  Property: test")
                .Should().Be(-1);
        }
        
        [Test]
        public void MultilineStringTokenReader_Read()
        {
            var lines = new[]
            {
                "First: single line",
                "Value: |",
                "  some",
                "    multiline ",
                "  value",
                "Other: single line"
            };
            
            var scanner = new Scanner(lines);
            scanner.AddToIndex(2);
            
            _reader.Read(scanner, "Value: |")
                 .Should().Be($"Value: some{Environment.NewLine}  multiline {Environment.NewLine}value");
        }
        
        [Test]
        public void MultilineStringTokenReader_Read_EmptyLine()
        {
            var lines = new[]
            {
                "First: single line",
                "Value: |",
                "  some",
                "",
                "    multiline ",
                "  value",
                "Other: single line"
            };
            
            var scanner = new Scanner(lines);
            scanner.AddToIndex(2);
            
            _reader.Read(scanner, "Value: |")
                .Should().Be($"Value: some{Environment.NewLine}{Environment.NewLine}  multiline {Environment.NewLine}value");
        }
        
        [Test]
        public void MultilineStringTokenReader_Read_SingleProperty()
        {
            var lines = new[]
            {
                "Value: |",
                "  some",
                "    multiline ",
                "  value"
            };
            
            var scanner = new Scanner(lines);
            scanner.AddToIndex(1);
            
            _reader.Read(scanner, "Value: |")
                .Should().Be($"Value: some{Environment.NewLine}  multiline {Environment.NewLine}value");
        }
        
        [Test]
        public void MultilineStringTokenReader_Read_LastProperty()
        {
            var lines = new[]
            {
                "First: single line",
                "Value: |",
                "  some",
                "    multiline ",
                "  value"
            };
            
            var scanner = new Scanner(lines);
            scanner.AddToIndex(2);
            
            _reader.Read(scanner, "Value: |")
                .Should().Be($"Value: some{Environment.NewLine}  multiline {Environment.NewLine}value");
        }
        
        [Test]
        public void MultilineStringTokenReader_Read_Colon()
        {
            var lines = new[]
            {
                "First: single line",
                "Value: |",
                "  some:test",
                "    multiline ",
                "  value"
            };
            
            var scanner = new Scanner(lines);
            scanner.AddToIndex(2);
            
            _reader.Read(scanner, "Value: |")
                .Should().Be($"Value: some:test{Environment.NewLine}  multiline {Environment.NewLine}value");
        }
        
        [Test]
        public void MultilineStringTokenReader_Read_Colon_Space()
        {
            var lines = new[]
            {
                "First: single line",
                "Value: |",
                "  value",
                "  some: ",
                "  more text"
            };
            
            var scanner = new Scanner(lines);
            scanner.AddToIndex(2);
            
            _reader.Read(scanner, "Value: |")
                .Should().Be($"Value: value");
        }
        
        [Test]
        public void MultilineStringTokenReader_Read_Colon_Space_InText()
        {
            var lines = new[]
            {
                "First: single line",
                "Value: |",
                "  value",
                "  some: text",
                "  more text"
            };
            
            var scanner = new Scanner(lines);
            scanner.AddToIndex(2);
            
            _reader.Read(scanner, "Value: |")
                .Should().Be($"Value: value");
        }
        
        [Test]
        public void MultilineStringTokenReader_Read_Colon_Newline()
        {
            var lines = new[]
            {
                "First: single line",
                "Value: |",
                "  value",
                "  some:",
                "  more text"
            };
            
            var scanner = new Scanner(lines);
            scanner.AddToIndex(2);
            
            _reader.Read(scanner, "Value: |")
                .Should().Be($"Value: value");
        }
        
        [Test]
        public void MultilineStringTokenReader_Read_Plus()
        {
            var lines = new[]
            {
                "Value: |+",
                "  line 1",
                "  line 2",
                "  line 3"
            };
            
            var scanner = new Scanner(lines);
            scanner.AddToIndex(1);
            
            _reader.Read(scanner, "Value: |+")
                .Should().Be($"Value: line 1{Environment.NewLine}line 2{Environment.NewLine}line 3");
        }
    }
}