using System;
using System.Collections.Generic;

namespace YamlMap.Scanning
{
    /// <summary>
    /// 
    /// </summary>
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
        /// <param name="line"></param>
        /// <returns></returns>
        public string Read(IScanner scanner, string line)
        {
            //
            // read all lines until the next property is reached
						
            var str = string.Empty;
            var indentation = 0;
            var next = new YamlLine(str);
            // while (string.IsNullOrEmpty(next.Property))
            while(!IsProperty(next.Line))
            {
                if (string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(next.Original) && indentation == 0)
                {
                    indentation = next.Indentation;
                }
							
                var prefix = string.IsNullOrEmpty(str) ? "" : Environment.NewLine;
                str += string.IsNullOrEmpty(next.Original) ? prefix : $"{prefix}{next.Original.Substring(indentation)}";
							
                var index = scanner.AddToIndex(1);
                if (index >= scanner.Input.Length)
                {
                    break;
                }

                next = new YamlLine(scanner.NextLine(index));
            }

            scanner.AddToIndex(-1);
            return line.Substring(0, line.IndexOf('|')) + str;
        }
        
        private bool IsProperty(string line)
        {
            var colon = line.IndexOf(':');
            if (colon < 0)
            {
                return false;
            }

            if (colon == line.Length - 1)
            {
                return true;
            }

            if (line.Length > colon + 1 && line[colon + 1] == ' ')
            {
                return true;
            }

            return false;
        }
    }
}