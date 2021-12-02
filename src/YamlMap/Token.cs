using System;
using System.Collections.Generic;
using System.Linq;

namespace YamlMap
{
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

        /// <summary>
        /// Gets the <see cref="TokenType"/>
        /// </summary>
        public TokenType TokenType { get; }

        /// <summary>
        /// Gets the key
        /// </summary>
        public string Key { get; }

        /// <summary>
        /// Gets the count of childrens of the token
        /// </summary>
        public int Count => _children.Count;

        /// <summary>
        /// Gets the <see cref="IToken"/> containing the given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IToken this[string key]
        {
            get
            {
                return _children.FirstOrDefault(t => t.Key == key);
            }
        }

        /// <summary>
        /// Gets the <see cref="IToken"/> at the given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public IToken this[int index]
        {
            get
            {
                var value = _children.ElementAt(index);
                return value;
            }
        }

        /// <summary>
        /// Gets the indentation of the value in the yaml
        /// </summary>
        public int Indentation { get; set; }

        /// <summary>
        /// Gets the parent <see cref="IToken"/>
        /// </summary>
        public IToken Parent { get; set; }

        /// <summary>
        /// Add a <see cref="IToken"/> to the children
        /// </summary>
        /// <param name="token"></param>
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
            Value = ParseValue(value);
            Indentation = indentation;
        }

        private string ParseValue(string value)
        {
            var quotations = new List<string>
            {
                "'",
                "\""
            };

            foreach(var c in quotations)
            {
                if (value.StartsWith(c) && value.EndsWith(c))
                {
                    return value.Substring(1, value.Length - 2);
                }
            }

            return value;
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
