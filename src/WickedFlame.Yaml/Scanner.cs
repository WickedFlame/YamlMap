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
            if (input.Contains('['))
            {
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
    }
}
