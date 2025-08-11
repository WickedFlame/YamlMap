
namespace YamlMap
{
    /// <summary>
    /// Represents a scanner that parses a yaml string to a set of <see cref="YamlLine"/>
    /// </summary>
    public interface IScanner
    {
        /// <summary>
        /// Gets the input string that is scanned
        /// </summary>
        string [] Input { get; }
        
        /// <summary>
        /// Scan the string for the next <see cref="YamlLine"/>
        /// </summary>
        /// <returns></returns>
        YamlLine ScanNext();

        /// <summary>
        /// Get the next line at the index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        string NextLine(int index);

        /// <summary>
        /// Add the given number to the index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        int AddToIndex(int index);
        
        /// <summary>
        /// Add a line that will be parsed in the next round
        /// </summary>
        /// <param name="line"></param>
        void EnqueueLine(YamlLine line);
    }
}
