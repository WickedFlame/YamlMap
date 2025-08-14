using YamlMap.Scanning;

namespace YamlMap.Tests.Scanning
{
    public class SingleQuotationTokenReaderTests
    {
        private ITokenReader _reader;

        [SetUp]
        public void Setup()
        {
            _reader = new SingleQuotationTokenReader();
        }
        
        [Test]
        public void SingleQuotationTokenReader_IndexOfNext()
        {
            _reader.IndexOfNext("Property: 'test'")
                .Should().BeGreaterThan(0);
        }
        
        [Test]
        public void SingleQuotationTokenReader_IndexOfNext_InText()
        {
            _reader.IndexOfNext("Property: test'test'")
                .Should().BeLessThan(0);
        }
        
        [Test]
        public void SingleQuotationTokenReader_IndexOfNext_Beginning()
        {
            _reader.IndexOfNext("'test'")
                .Should().Be(0);
        }
        
        [Test]
        public void SingleQuotationTokenReader_IndexOfNext_List()
        {
            _reader.IndexOfNext("  - 'test'")
                .Should().BeGreaterThan(0);
        }
        
        [Test]
        public void SingleQuotationTokenReader_IndexOfNext_List_InText()
        {
            _reader.IndexOfNext("  - test'test'")
                .Should().BeLessThan(0);
        }
    }
}