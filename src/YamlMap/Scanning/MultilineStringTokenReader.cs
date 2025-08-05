using System;

namespace YamlMap.Scanning
{
    public class MultilineStringTokenReader : ITokenReader
    {
        /// <summary>
        /// Token reader next |
        /// </summary>
        public TokenReaderType ReaderType  => TokenReaderType.MultilineString;
        
        /// <summary>
        /// Get the index of the next line
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public int IndexOfNext(string line)
        {
            return line.IndexOf('|');
        }

        /// <summary>
        /// Parse the next property
        /// </summary>
        /// <param name="scanner"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public string Read(IScanner scanner, string input)
        {
            //
            // read all lines until the next property is reached
						
            var str = string.Empty;
            var indentation = 0;
            var tmp = new YamlLine(str);
            while (string.IsNullOrEmpty(tmp.Property))
            {
                if (string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(tmp.Original) && indentation == 0)
                {
                    indentation = tmp.Indentation;
                }
							
                var prefix = string.IsNullOrEmpty(str) ? "" : Environment.NewLine;
                str += string.IsNullOrEmpty(tmp.Original) ? prefix : $"{prefix}{tmp.Original.Substring(indentation)}";
							
                var index = scanner.AddToIndex(1);
                if (index >= input.Length)
                {
                    break;
                }

                tmp = new YamlLine(scanner.NextLine(index));
            }

            scanner.AddToIndex(-1);
            return input.Substring(0, input.IndexOf('|')) + str;
        }
    }
}