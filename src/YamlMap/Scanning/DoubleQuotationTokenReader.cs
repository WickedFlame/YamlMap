
namespace YamlMap.Scanning
{
    /// <summary>
    /// Token reader for double quotes
    /// </summary>
    public class DoubleQuotationTokenReader : QuotationReaderBase, ITokenReader
    {
        /// <summary>
        /// 
        /// </summary>
        public DoubleQuotationTokenReader()
            : base(TokenReaderType.DoubleQuotation, '"')
        {
        }
    }
}
