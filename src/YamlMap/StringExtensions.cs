
namespace YamlMap
{
    /// <summary>
    /// Extensions for strings
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Indent the string with the amount of spaces
        /// </summary>
        /// <param name="value"></param>
        /// <param name="indentation"></param>
        /// <returns></returns>
        public static string Indent(this string value, int indentation)
        {
            return "".PadLeft(indentation) + value;
        }
    }
}
