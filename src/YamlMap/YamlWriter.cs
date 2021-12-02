using YamlMap.Serialization;

namespace YamlMap
{
    /// <summary>
    /// Serializer that maps object to a yaml string
    /// </summary>
    public class YamlWriter
    {
        /// <summary>
        /// Write the object to a yaml string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public string Write<T>(T item)
        {
            var tokenSerializer = new TokenSerializer();
            return tokenSerializer.Serialize(item);
        }
    }
}
