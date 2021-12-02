
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
    }
}
