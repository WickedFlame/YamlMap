
namespace YamlMap.Serialization
{
    /// <summary>
    /// Token deserializer
    /// </summary>
    public interface ITokenDeserializer
    {
        /// <summary>
        /// Gets the object node
        /// </summary>
        object Node { get; }

        /// <summary>
        /// Deserialize the node to the token
        /// </summary>
        /// <param name="token"></param>
        void Deserialize(IToken token);

        /// <summary>
        /// Deserialize all children
        /// </summary>
        void DeserializeChildren();
    }
}
