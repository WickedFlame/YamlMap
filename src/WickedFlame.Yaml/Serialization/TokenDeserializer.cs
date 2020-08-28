using System;
using WickedFlame.Yaml.Serialization.Mappers;

namespace WickedFlame.Yaml.Serialization
{
    public class TokenDeserializer : ITokenDeserializer
    {
        //private readonly PropertyMapper _mapper;
        private readonly object _item;
        private readonly IToken _token;
        private readonly Type _type;

        public TokenDeserializer(Type type, IToken token)
        {
            //_mapper = new PropertyMapper(type);
            _item = type.CreateInstance(token);
            _token = token;
            _type = type;
        }

        public object Node => _item;

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
            if (property == null)
            {
                throw new InvalidConfigurationException($"The configured token {token} could not be mapped to the Type {Node.GetType().FullName}");
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
