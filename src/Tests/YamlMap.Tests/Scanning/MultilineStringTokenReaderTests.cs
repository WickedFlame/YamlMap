using System;
using System.Text;
using FluentAssertions;
using NUnit.Framework;
using YamlMap.Scanning;

namespace YamlMap.Tests.Scanning
{
    public class MultilineStringTokenReaderTests
    {
        private ITokenReader _reader;

        [SetUp]
        public void Setup()
        {
            _reader = new MultilineStringTokenReader();
        }
        
        [Test]
        public void MultilineStringTokenReader_IndexOfNext()
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
    }
}