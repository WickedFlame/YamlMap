
namespace YamlMap
{
    /// <summary>
    /// Interface that represents a token of a yaml string
    /// </summary>
    public interface IToken
    {
        /// <summary>
        /// Gets the <see cref="TokenType"/>
        /// </summary>
        TokenType TokenType { get; }

        /// <summary>
        /// Gets the key
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Gets the count of childrens of the token
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Gets the <see cref="IToken"/> containing the given key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IToken this[string key] { get; }

        /// <summary>
        /// Gets the <see cref="IToken"/> at the given index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        IToken this[int index] { get; }

        /// <summary>
        /// Gets the indentation of the value in the yaml
        /// </summary>
        int Indentation { get; }

        /// <summary>
        /// Gets the parent <see cref="IToken"/>
        /// </summary>
        IToken Parent { get; set; }

        /// <summary>
        /// Add a <see cref="IToken"/> to the children
        /// </summary>
        /// <param name="token"></param>
        void Set(IToken token);
    }
}
