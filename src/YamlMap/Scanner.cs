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
        private readonly Queue<YamlLine> _scannedTokens;

		/// <summary>
		/// Creates a new instance of a <see cref="IScanner"/>
		/// </summary>
		/// <param name="input"></param>
        public Scanner(string[] input)
        {
            _scannedTokens = new Queue<YamlLine>();
            _input = input;
            _index = -1;
        }

		/// <summary>
		/// Scan the string for the next <see cref="YamlLine"/>
		/// </summary>
		/// <returns></returns>
		/// <exception cref="InvalidConfigurationException"></exception>
		public YamlLine ScanNext()
        {
            if (_scannedTokens.Count > 0)
            {
                return _scannedTokens.Dequeue();
            }

            _index = _index + 1;
            if (_index >= _input.Length)
            {
                return null;
            }

            var input = NextLine(_index);
            var specialReader = NextReader(input);
            if (specialReader != null)
            {
	            switch (specialReader.ReaderType)
	            {
		            case TokenReaderType.Bracket:
			            while (!input.Contains(']'))
			            {
				            _index = _index + 1;
				            if (_index >= _input.Length)
				            {
					            break;
				            }

				            input += NextLine(_index);
			            }

			            var listInput = input.Substring(input.IndexOf('[') + 1);
			            listInput = listInput.Substring(0, listInput.IndexOf(']'));
			            var arr = listInput.Split(',');
			            foreach (var item in arr)
			            {
				            _scannedTokens.Enqueue(new YamlLine($"- {item.TrimStart()}".Indent(2)));
			            }

			            input = input.Substring(0, input.IndexOf(':') + 1);
			            break;

					case TokenReaderType.DoubleQuotation:
					case TokenReaderType.SingleQuoatation:
						var mark = specialReader.ReaderType == TokenReaderType.SingleQuoatation ? '\'' : '"';
						var start = input.IndexOf(mark);
						var end = input.LastIndexOf(mark);
						if (end == start || end < input.Length - 1)
						{
							throw new InvalidConfigurationException($"Found unexpected end of stream while scanning a quoted scalar at line {_index + 1} column {end}");
						}
						break;
	            }
            }

            var line = new YamlLine(input);
            return line;
        }

        private string NextLine(int index)
        {
            var input = _input[index];

            if (input.Contains('#'))
            {
                input = input.Substring(0, input.IndexOf('#'));
            }

            return input;
        }

        private ITokenReader NextReader(string line)
        {
	        var readers = new List<ITokenReader>
	        {
		        new BracketTokenReader(),
		        new SingleQuotationTokenReader(),
		        new DoubleQuotationTokenReader()
	        };

	        var next = new
	        {
		        Reader = (ITokenReader) null,
		        Index = int.MaxValue
	        };

	        foreach (var reader in readers)
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
