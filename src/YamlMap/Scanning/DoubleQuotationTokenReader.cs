
namespace YamlMap.Scanning
{
    /// <summary>
    /// Token reader for double quotes
    /// </summary>
    public class DoubleQuotationTokenReader : ITokenReader
    {
        /// <summary>
        /// TokenReaderType.DoubleQuotation
        /// </summary>
        public TokenReaderType ReaderType => TokenReaderType.DoubleQuotation;

        /// <summary>
        /// Get the index of the next line
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public int IndexOfNext(string line)
        {
            return line.IndexOf('"');
        }
    }
}
