using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WickedFlame.Yaml
{
    public interface IToken
    {
        TokenType TokenType { get; }

        string Key { get; }

        int Count { get; }

        IToken this[string key] { get; }

        IToken this[int index] { get; }

        int Indentation { get; }

        IToken Parent { get; set; }

        void Set(IToken value);
    }

    public class Token : IToken
    {
        private readonly List<IToken> _children = new List<IToken>();

        public Token(string key, int indentaiton, TokenType tokenType)
        {
            Key = key;
            TokenType = tokenType;
            Indentation = indentaiton;
        }

        public Token(string key, int indentaiton)
        {
            TokenType = TokenType.Object;
            Key = key;
            Indentation = indentaiton;
        }

        public TokenType TokenType { get; }

        public string Key { get; }

        public int Count => _children.Count;

        public IToken this[string key]
        {
            get
            {
                return _children.FirstOrDefault(t => t.Key == key);
            }
        }

        public IToken this[int index]
        {
            get
            {
                var value = _children.ElementAt(index);
                return value;
            }
        }

        public int Indentation { get; set; }

        public IToken Parent { get; set; }

        public void Set(IToken token)
        {
            if (!string.IsNullOrEmpty(token.Key))
            {
                var item = _children.FirstOrDefault(v => v.Key == token.Key);
                if (item != null)
                {
                    _children.Remove(item);
                }
            }

            token.Parent = this;

            _children.Add(token);
        }

        public override string ToString()
        {
            if (TokenType == TokenType.ListItem || string.IsNullOrEmpty(Key))
            {
                return $"[{TokenType}]";
            }

            return $"[{TokenType}] Key: {Key}";
        }
    }

    public class ValueToken : IToken
    {
        public ValueToken(string key, string value, int indentation)
        {
            TokenType = TokenType.Value;
            Key = key;
            Value = value;
            Indentation = indentation;
        }

        public TokenType TokenType { get; }

        public string Key { get; }

        public int Count => 0;

        public IToken this[string key] => throw new InvalidOperationException("A ValueToken cannot be used with a Indexer");

        public IToken this[int index] => throw new InvalidOperationException("A ValueToken cannot be used with a Indexer");

        public int Indentation { get; set; }

        public IToken Parent { get; set; }

        public string Value { get; }

        public void Set(IToken value)
        {
        }

        public override string ToString()
        {
            return $"[{TokenType}] {Key} : {Value}";
        }
    }
}
