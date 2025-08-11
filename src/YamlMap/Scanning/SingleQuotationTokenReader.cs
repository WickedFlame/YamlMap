
namespace YamlMap.Scanning
{
    /// <summary>
    /// Token reader for single quotes
    /// </summary>
    public class SingleQuotationTokenReader : QuotationReaderBase, ITokenReader
    {
        /// <summary>
        /// 
        /// </summary>
        public SingleQuotationTokenReader()
            : base(TokenReaderType.SingleQuoatation, '\'')
        {
        }
    }
}
