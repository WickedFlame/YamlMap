using System;
using System.Collections.Generic;
using System.Linq;

namespace YamlMap
{
    /// <summary>
    /// Token representing a yaml element
    /// </summary>
    public class Token : IToken
    {
        private readonly List<IToken> _children = new List<IToken>();

        /// <summary>
        /// Creates a new Token representing a yaml element
        /// </summary>
        /// <param name="key"></param>
        /// <param name="indentaiton"></param>
        /// <param name="tokenType"></param>
        public Token(string key, int indentaiton, TokenType tokenType)
        {
            Key = key;
            TokenType = tokenType;
            Indentation = indentaiton;
        }

        /// <summary>
        /// Creates a new Token representing a yaml element
        /// </summary>
        /// <param name="key"></param>
        /// <param name="indentaiton"></param>
        public Token(string key, int indentaiton) 
            : this(key, indentaiton, TokenType.Object)
        {
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

        /// <summary>
        /// Gets the string value
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            if (TokenType == TokenType.ListItem || string.IsNullOrEmpty(Key))
            {
                return $"[{TokenType}]";
            }

            return $"[{TokenType}] Key: {Key}";
        }
    }
}
