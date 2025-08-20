using System;

namespace YamlMap.Scanning
{
    public class MultilineReaderBase
    {
        private readonly char _mark;
        private readonly string _newLine;

        protected MultilineReaderBase(TokenReaderType readerType, char mark, string newLine)
        {
            ReaderType = readerType;
            _mark = mark;
            _newLine = newLine;
        }
        
        public TokenReaderType ReaderType { get; }
        
        /// <summary>
        /// Get the index of the next line
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        public int IndexOfNext(string line)
        {
            return line.IndexOf($": {_mark}", StringComparison.CurrentCultureIgnoreCase);
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
            while(!IsProperty(next.Line))
            {
                if (string.IsNullOrEmpty(str) && !string.IsNullOrEmpty(next.Original) && indentation == 0)
                {
                    indentation = next.Indentation;
                }
							
                var prefix = string.IsNullOrEmpty(str) ? "" : _newLine;
                str += string.IsNullOrEmpty(next.Original) ? prefix : $"{prefix}{next.Original.Substring(indentation)}";
							
                var index = scanner.AddToIndex(1);
                if (index >= scanner.Input.Length)
                {
                    break;
                }

                next = new YamlLine(scanner.NextLine(index));
            }

            scanner.AddToIndex(-1);
            return line.Substring(0, line.IndexOf(_mark)) + str;
        }
        
        private static bool IsProperty(string line)
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

            return line.Length > colon + 1 && line[colon + 1] == ' ';
        }
    }
}