using System;

namespace YamlMap.Scanning
{
    public class FoldedMultilineTokenReader : MultilineReaderBase, ITokenReader
    {
        public FoldedMultilineTokenReader() 
            : base(TokenReaderType.FoldedMultiline, '>', " ")
        {
        }
    }
}