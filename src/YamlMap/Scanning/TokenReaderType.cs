
namespace YamlMap.Scanning
{
    /// <summary>
    /// Enumeration for Token reading
    /// </summary>
    public enum TokenReaderType
    {
        /// <summary>
        /// Bracket
        /// </summary>
        Bracket,

        /// <summary>
        /// Single quotation
        /// </summary>
        SingleQuoatation,

        /// <summary>
        /// Double quotation
        /// </summary>
        DoubleQuotation,
        
        /// <summary>
        /// Multiline string that respects linebreaks
        /// </summary>
        LiteralMultiline,
        
        /// <summary>
        /// Multiline string that removes linebreaks
        /// </summary>
        FoldedMultiline
    }
}
