using YamlMap.Scanning;

namespace YamlMap.Tests.Scanning
{
    public class BracketTokenReaderTests
    {
        private ITokenReader _reader;

        [SetUp]
        public void Setup()
        {
            _reader = new BracketTokenReader();
        }
        
        [Test]
        public void BracketTokenReader_IndexOfNext()
        {
            _reader.IndexOfNext("  Property: [test]")
                .Should().BeGreaterThan(0);
        }
        
        [Test]
        public void BracketTokenReader_IndexOfNext_InvalidArray()
        {
            _reader.IndexOfNext("  Property: test [test]")
                .Should().BeLessThan(0);
        }
        
        [Test]
        public void BracketTokenReader_Read()
        {
            var lines = new[]
            {
                "Ids: ['1', '2', '3', '4']"
            };
            var scanner = new Scanner(lines);
            _reader.Read(scanner, "Ids: ['1', '2', '3', '4']")
                .Should().Be("Ids:");
        }
        
        [Test]
        public void BracketTokenReader_Read_Multiline()
        {
            var lines = new[]
            {
                "Ids: ['1', '2',",
                "'3', '4']"
            };
            var scanner = new Scanner(lines);
            _reader.Read(scanner, "Ids: ['1', '2',")
                .Should().Be("Ids:");
        }
        
        [Test]
        public void BracketTokenReader_Read_Multiline_NotLast()
        {
            var lines = new[]
            {
                "Ids: ['1', '2',",
                "'3', '4']",
                "Property2: value"
            };
            var scanner = new Scanner(lines);
            _reader.Read(scanner, "Ids: ['1', '2',")
                .Should().Be("Ids:");
            
        }
    }
}