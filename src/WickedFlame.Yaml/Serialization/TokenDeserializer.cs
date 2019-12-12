using System;
using WickedFlame.Yaml.Serialization.Mappers;

namespace WickedFlame.Yaml.Serialization
{
    public class TokenDeserializer : ITokenDeserializer
    {
        private readonly PropertyMapper _mapper;
        private readonly object _item;
        private readonly IToken _token;

        public TokenDeserializer(Type type, IToken token)
        {
            _mapper = new PropertyMapper(type);
            _item = type.CreateInstance(token);
            _token = token;
        }

        public object Node => _item;

        public void Deserialize(IToken token)
        {
            var mapper = MapperFactory.GetObjectMapper(Node);
            if (mapper != null)
            {
                if (mapper.Map(token, Node))
                {
                    return;
                }
            }

            if (token is ValueToken valueToken)
            {
                if (_mapper.TryAppendProperty(valueToken, Node))
                {
                    return;
                }
            }

            var property = _mapper.GetProperty(token);
            if (property == null)
            {
                throw new InvalidConfigurationException($"The configured Property {token.Key} does not exist in the Type {Node.GetType().FullName}");
            }

            var child = new TokenDeserializer(property.PropertyType, token);
            child.DeserializeChildren();

            property.SetValue(Node, child.Node, null);
        }

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
