
namespace YamlMap
{
    /// <summary>
    /// Represents a line out of the yaml string
    /// </summary>
    public class YamlLine
    {
        /// <summary>
        /// Initialize the line based on the string
        /// </summary>
        /// <param name="line"></param>
        public YamlLine(string line)
        {
            Original = line;
            Line = line.TrimStart();
            Indentation = line.Length - Line.Length;

            if (line.TrimStart().StartsWith("- "))
            {
                IsListItem = true;
                line = line.TrimStart().Substring(2);
                Value = line;
            }

            var index = line.IndexOf(": ", System.StringComparison.InvariantCultureIgnoreCase);
            if (index < 0)
            {
                var objIndex = line.IndexOf(':');
                if (objIndex > 0 && objIndex == line.Length - 1)
                {
                    index = objIndex;
                }
            }

            if (index > 0)
            {
                Property = line.Substring(0, index).TrimStart();
                Value = line.Substring(index + 1).Trim();
            }
        }

        /// <summary>
        /// The original line
        /// </summary>
        public string Original { get; }

        /// <summary>
        /// Gets the amount of spaces for the indentation of the yaml line
        /// </summary>
        public int Indentation { get; set; }

        /// <summary>
        /// The original line without leading whitespaces
        /// </summary>
        public string Line { get; }

        /// <summary>
        /// The property name of the line
        /// </summary>
        public string Property { get; }

        /// <summary>
        /// The value of the line
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Defines if the line is part of a list
        /// </summary>
        public bool IsListItem { get; }
    }
}
