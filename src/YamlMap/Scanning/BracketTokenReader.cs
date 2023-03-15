
namespace YamlMap.Scanning
{
    /// <summary>
    /// Token reader for brackets
    /// </summary>
    public class BracketTokenReader : ITokenReader
    {
        /// <summary>
        /// TokenReaderType.Bracket
        /// </summary>
        public TokenReaderType ReaderType => TokenReaderType.Bracket;

        /// <summary>
        /// Get the index of the next line
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public int IndexOfNext(string line)
        {
            return line.IndexOf('[');
        }
    }
}
