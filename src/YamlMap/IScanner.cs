
namespace YamlMap
{
    /// <summary>
    /// Represents a scanner that parses a yaml string to a set of <see cref="YamlLine"/>
    /// </summary>
    public interface IScanner
    {
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
    }
}
