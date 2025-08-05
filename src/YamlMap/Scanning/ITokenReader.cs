
namespace YamlMap.Scanning
{
    /// <summary>
    /// Token rader
    /// </summary>
    public interface ITokenReader
    {
        /// <summary>
        /// Type of reader
        /// </summary>
        TokenReaderType ReaderType { get; }

        /// <summary>
        /// Get the index of the next line
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        int IndexOfNext(string line);

        /// <summary>
        /// Parse the next property
        /// </summary>
        /// <param name="scanner"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        string Read(IScanner scanner, string input);
    }
}
