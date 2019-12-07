using System.Linq;

namespace WickedFlame.Yaml
{
    public interface IScanner
    {
        YamlLine ScanNext();
    }

    public class Scanner : IScanner
    {
        private readonly string[] _input;
        private int _index;

        public Scanner(string[] input)
        {
            _input = input;
            _index = -1;
        }

        public YamlLine ScanNext()
        {
            _index = _index + 1;
            if (_index >= _input.Length)
            {
                return null;
            }

            var input = _input[_index];

            if (input.Contains('#'))
            {
                input = input.Substring(0, input.IndexOf('#'));
            }

            var line = new YamlLine(input);
            return line;
        }
    }
}
