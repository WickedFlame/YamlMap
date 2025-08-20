using System;
using System.Collections.Generic;

namespace YamlMap
{
    /// <summary>
    /// A Token representing a yaml value element
    /// </summary>
    public class ValueToken : IToken
    {
        /// <summary>
        /// Creates a new Token representing a yaml value element
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="indentation"></param>
        public ValueToken(string key, string value, int indentation)
        {
            TokenType = TokenType.Value;
            Key = key;
            Value = ParseValue(value);
            Indentation = indentation;
        }

        private static string ParseValue(string value)
        {
            var quotations = new List<string>
            {
                "'",
                "\""
            };

            foreach (var c in quotations)
            {
                if (value.StartsWith(c) && value.EndsWith(c))
                {
                    return value.Substring(1, value.Length - 2);
                }
            }

            return value;
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
        /// Count of children
        /// </summary>
        public int Count => 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public IToken this[string key] => throw new InvalidOperationException("A ValueToken cannot be used with a Indexer");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public IToken this[int index] => throw new InvalidOperationException("A ValueToken cannot be used with a Indexer");

        /// <summary>
        /// Gets the indentation of the value in the yaml
        /// </summary>
        public int Indentation { get; set; }

        /// <summary>
        /// Gets the parent <see cref="IToken"/>
        /// </summary>
        public IToken Parent { get; set; }

        /// <summary>
        /// Gets the value of the yaml element
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Add a <see cref="IToken"/> to the children
        /// </summary>
        /// <param name="value"></param>
        public void Set(IToken value)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[{TokenType}] {Key} : {Value}";
        }
    }
}
