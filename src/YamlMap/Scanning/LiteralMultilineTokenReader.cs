using System;
using System.Collections.Generic;

namespace YamlMap.Scanning
{
    /// <summary>
    /// 
    /// </summary>
    public class LiteralMultilineTokenReader : MultilineReaderBase, ITokenReader
    {
        public LiteralMultilineTokenReader()
            : base(TokenReaderType.LiteralMultiline, '|', Environment.NewLine)
        {
        }
    }
}