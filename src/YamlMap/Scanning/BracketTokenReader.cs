using System.Linq;

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
            var colon = line.IndexOf(':');
            var bracket = line.IndexOf('[');
            return colon > 0 && colon + 2 == bracket ? bracket : -1;
        }

        /// <summary>
        /// Parse the next property
        /// </summary>
        /// <param name="scanner"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Read(IScanner scanner, string input)
        {
            while (!input.Contains(']'))
            {
                var index = scanner.AddToIndex(1);
                if (index >= scanner.Input.Length)
                {
                    break;
                }

                input += scanner.NextLine(index);
            }

            var listInput = input.Substring(input.IndexOf('[') + 1);
            listInput = listInput.Substring(0, listInput.IndexOf(']'));
            var arr = listInput.Split(',');
            foreach (var item in arr)
            {
                scanner.EnqueueLine(new YamlLine($"- {item.TrimStart()}".Indent(2)));
            }

            return input.Substring(0, input.IndexOf(':') + 1);
        }
    }
}
