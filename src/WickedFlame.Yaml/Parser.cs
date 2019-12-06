﻿using System;
using System.Collections.Generic;
using System.Linq;
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
            var root = new Token(null, 0);
            IToken token = root;

            while (line != null)
            {
                while (token.Parent != null && line.Indentation <= token.Indentation)
                {
                    if (token.Parent == null)
                    {
                        break;
                    }

                    token = token.Parent;
                }
                
                if (line.IsListItem && !string.IsNullOrEmpty(line.Property) && !string.IsNullOrEmpty(line.Value))
                {
                    // add new object to the tree
                    var list = new Token(null, line.Indentation, TokenType.ListItem);

                    // list item
                    token.Set(list);
                    token = list;

                    var value = new ValueToken(line.Property, line.Value, line.Indentation + 2);
                    token.Set(value);

                    line = _scanner.ScanNext();
                    continue;
                }

                if (line.IsListItem && !string.IsNullOrEmpty(line.Property))
                {
                    // add new list to the tree
                    var list = new Token(null, line.Indentation, TokenType.ListItem);
                    token.Set(list);
                    token = list;


                    // object node to list
                    var child = new Token(line.Property, line.Indentation + 2);
                    token.Set(child);
                    token = child;

                    line = _scanner.ScanNext();
                    continue;
                }


                // simple list item eg: List<string>
                if (line.IsListItem)
                {
                    var child = new ValueToken(line.Property, line.Value, line.Indentation + 2);
                    token.Set(child);

                    line = _scanner.ScanNext();
                    continue;
                }


                // simple property with value
                if (!string.IsNullOrEmpty(line.Property) && !string.IsNullOrEmpty(line.Value))
                {
                    var child = new ValueToken(line.Property, line.Value, line.Indentation);
                    token.Set(child);

                    line = _scanner.ScanNext();
                    continue;
                }

                if (!string.IsNullOrEmpty(line.Property))
                {
                    // add new object to the tree
                    var child = new Token(line.Property, line.Indentation);

                    token.Set(child);
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

        string Key { get; }

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
            if(!string.IsNullOrEmpty(token.Key))
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

    public enum TokenType
    {
        Value,
        Object,
        ListItem
    }
}
