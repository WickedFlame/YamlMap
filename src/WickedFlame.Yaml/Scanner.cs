using System.Collections.Generic;
using System.Linq;

namespace WickedFlame.Yaml
{
    public interface IScanner
    {
        YamlLine ScanNext();
    }

    public enum ScannToken
    {
        Default,
        Object,
        List
    }

    public class Scanner : IScanner
    {
        private readonly string[] _input;
        private int _index;

        private ScannToken _scannToken = ScannToken.Default;

        private Queue<YamlLine> _scannedTokens;

        public Scanner(string[] input)
        {
            _scannedTokens = new Queue<YamlLine>();
            _input = input;
            _index = -1;
        }

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

						input = input.Remove(start, 1)
							.Remove(end - 1, 1);
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

    public enum TokenReaderType
    {
		Bracket,
		SingleQuoatation,
		DoubleQuotation
    }

	public interface ITokenReader
    {
	    TokenReaderType ReaderType { get; }

		int IndexOfNext(string line);
    }

    public class BracketTokenReader : ITokenReader
    {
	    public TokenReaderType ReaderType => TokenReaderType.Bracket;

	    public int IndexOfNext(string line)
	    {
		    return line.IndexOf('[');
	    }
    }

    public class SingleQuotationTokenReader : ITokenReader
    {
	    public TokenReaderType ReaderType => TokenReaderType.SingleQuoatation;

		public int IndexOfNext(string line)
	    {
		    return line.IndexOf('\'');
	    }
    }

    public class DoubleQuotationTokenReader : ITokenReader
    {
	    public TokenReaderType ReaderType => TokenReaderType.DoubleQuotation;

		public int IndexOfNext(string line)
	    {
		    return line.IndexOf('"');
	    }
    }
}
