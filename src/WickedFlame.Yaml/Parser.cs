using System;
using System.Collections.Generic;
using System.Text;

namespace WickedFlame.Yaml
{
    public class Parser
    {
        private readonly IScanner _scanner;

        public Parser(IScanner scanner)
        {
            _scanner = scanner;
        }

        public IToken Parse()
        {
            var line = _scanner.ScanNext();
            var root = new Token();
            IToken token = root;

            while (line != null)
            {
                while (line.Indentation <= token.Indentation)
                {
                    if (token.Parent == null)
                    {
                        break;
                    }

                    token = token.Parent;
                }


                // simple property with value
                if (!string.IsNullOrEmpty(line.Property) && !string.IsNullOrEmpty(line.Value))
                {
                    token.Set(line.Property, new ValueToken(line.Value));

                    line = _scanner.ScanNext();
                    continue;
                }

                if (!string.IsNullOrEmpty(line.Property))
                {
                    // add new object to the tree
                    var child = new Token
                    {
                        Parent = token,
                        Indentation = line.Indentation
                    };

                    token.Set(line.Property, child);
                    token = child;

                    line = _scanner.ScanNext();
                    continue;
                }

                line = _scanner.ScanNext();
            }

            return root;
        }
    }

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

            var line = new YamlLine(_input[_index]);
            return line;
        }
    }

    public interface IToken
    {
        TokenType TokenType { get; }

        IToken this[string key] { get; }

        int Indentation { get; set; }

        IToken Parent { get; set; }

        void Set(string key, object value);
    }

    public class Token : IToken
    {
        private readonly Dictionary<string, IToken> _children = new Dictionary<string, IToken>();

        public Token()
        {
            TokenType = TokenType.Object;
        }

        public TokenType TokenType { get; }

        public IToken this[string key]
        {
            get
            {
                return _children[key];
            }
        }

        public int Indentation { get; set; }

        public IToken Parent { get; set; }

        public void Set(string key, object value)
        {
            var token = value as IToken ?? new ValueToken(value);
            _children[key] = token;
        }
    }

    public class ValueToken : IToken
    {
        private object _value;

        public ValueToken(object value)
        {
            TokenType = TokenType.Value;
            _value = value;
        }

        public TokenType TokenType { get; }

        public IToken this[string key]
        {
            get
            {
                throw new InvalidOperationException("A ValueToken cannot be used with a Indexer");
            }
        }

        public void Set(string key, object value)
        {
            _value = value;
        }

        public int Indentation { get; set; }
        
        public IToken Parent { get; set; }

        public object Value => _value;
    }

    public enum TokenType
    {
        Value,
        Object
    }
}
