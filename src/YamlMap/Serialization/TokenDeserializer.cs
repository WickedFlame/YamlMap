using System;
using YamlMap.Serialization.Mappers;

namespace YamlMap.Serialization
{
    /// <summary>
    /// Deserializer
    /// </summary>
    public class TokenDeserializer : ITokenDeserializer
    {
        private readonly object _item;
        private readonly IToken _token;
        private readonly Type _type;

        /// <summary>
        /// Creates a new deserializer
        /// </summary>
        /// <param name="type"></param>
        /// <param name="token"></param>
        public TokenDeserializer(Type type, IToken token)
        {
            _item = type.CreateInstance(token);
            _token = token;
            _type = type;
        }

        /// <summary>
        /// Gets the instance that is created
        /// </summary>
        public object Node => _item;

        /// <summary>
        /// Deserialize a <see cref="IToken"/>
        /// </summary>
        /// <param name="token"></param>
        /// <exception cref="InvalidConfigurationException"></exception>
        public void Deserialize(IToken token)
        {
            var mapper = MapperFactory.GetObjectMapper(Node, _type);
            if (mapper != null)
            {
                if (mapper.Map(token, Node))
                {
                    return;
                }
            }

            var property = _type.GetProperty(token);
            PropertyValidator.Assert(property, token, Node.GetType());

            var child = new TokenDeserializer(property.PropertyType, token);
            child.DeserializeChildren();

            property.SetValue(Node, child.Node, null);
        }

        /// <summary>
        /// Deserialize all child tokens
        /// </summary>
        public void DeserializeChildren()
        {
            // refactor the line to be parsed as property
            for (var i = 0; i < _token.Count; i++)
            {
                Deserialize(_token[i]);
            }
        }
    }
}
