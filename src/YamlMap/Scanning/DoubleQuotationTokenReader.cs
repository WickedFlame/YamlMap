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
            var index = line.IndexOf('"');
            var property = line.IndexOf(':');
            var list = line.IndexOf('-');

            return index == 0 ||
                property > 0 && index == property + 2 || 
                list > 0 && index == list + 2 
                ? index 
                : -1;
        }
    }
}
