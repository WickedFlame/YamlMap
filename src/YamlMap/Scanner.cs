using System.Collections.Generic;
using System.Linq;
using YamlMap.Scanning;

namespace YamlMap
{
	/// <summary>
	/// Scanner that parses a yaml string to a list of <see cref="YamlLine"/>
	/// </summary>
    public class Scanner : IScanner
    {
        private readonly string[] _input;
        private int _index;
        private readonly Queue<YamlLine> _nextLines;
        private readonly List<ITokenReader> _readers;

        /// <summary>
		/// Creates a new instance of a <see cref="IScanner"/>
		/// </summary>
		/// <param name="input"></param>
        public Scanner(string[] input)
        {
	        _nextLines = new Queue<YamlLine>();
            _input = input;
            _index = -1;
            
            _readers = new List<ITokenReader>
            {
	            new BracketTokenReader(),
	            new SingleQuotationTokenReader(),
	            new DoubleQuotationTokenReader(),
	            new LiteralMultilineTokenReader(),
	            new FoldedMultilineTokenReader()
            };
        }
		
        /// <summary>
        /// Gets the input string that is scanned
        /// </summary>
        public string[] Input => _input;

		/// <summary>
		/// Scan the string for the next <see cref="YamlLine"/>
		/// </summary>
		/// <returns></returns>
		/// <exception cref="InvalidConfigurationException"></exception>
		public YamlLine ScanNext()
        {
            if (_nextLines.Count > 0)
            {
                return _nextLines.Dequeue();
            }

            _index = _index + 1;
            if (_index >= _input.Length)
            {
                return null;
            }

            var line = NextLine(_index);
            var specialReader = NextReader(line);
            if (specialReader != null)
            {
	            line = specialReader.Read(this, line);
            }

            return new YamlLine(line);
        }

        /// <summary>
        /// Add a line that will be parsed in the next round
        /// </summary>
        /// <param name="line"></param>
        public void EnqueueLine(YamlLine line)
        {
	        _nextLines.Enqueue(line);
        }

        /// <summary>
        /// Get the next line at the index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public string NextLine(int index)
        {
            var input = _input[index];

            if (input.Contains('#'))
            {
                input = input.Substring(0, input.IndexOf('#'));
            }

            return input;
        }

        /// <summary>
        /// Add the given number to the index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public int AddToIndex(int index)
        {
	        _index = _index + index;
	        return _index;
        }

        private ITokenReader NextReader(string line)
        {
	        var next = new
	        {
		        Reader = (ITokenReader) null,
		        Index = int.MaxValue
	        };

	        foreach (var reader in _readers)
	        {
		        var index = reader.IndexOfNext(line);
		        if (index < 0 || index > next.Index)
		        {
			        continue;
		        }

		        next = new
		        {
			        Reader = reader,
			        Index = index
		        };
	        }

	        return next.Reader;
        }
    }
}
