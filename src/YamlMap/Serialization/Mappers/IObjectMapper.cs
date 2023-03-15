
namespace YamlMap.Serialization.Mappers
{
    /// <summary>
    /// Object mapper
    /// </summary>
    public interface IObjectMapper
    {
        /// <summary>
        /// Map the token to the object
        /// </summary>
        /// <param name="token"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        bool Map(IToken token, object item);
    }
}
