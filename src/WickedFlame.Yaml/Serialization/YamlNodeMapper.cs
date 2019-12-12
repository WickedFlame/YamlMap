﻿using System;
using WickedFlame.Yaml.Serialization.Mappers;

namespace WickedFlame.Yaml.Serialization
{
    public class YamlNodeMapper
    {
        private readonly PropertyMapper _mapper;
        private readonly object _item;

        public YamlNodeMapper(Type type, IToken token)
        {
            _mapper = new PropertyMapper(type);
            _item = type.CreateInstance(token);
        }

        public void MapToken(IToken token)
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

            var child = new YamlNodeMapper(property.PropertyType, token);
            property.SetValue(Node, child.Node, null);
            for (var i = 0; i < token.Count; i++)
            {
                child.MapToken(token[i]);
            }

        }

        public object Node => _item;
    }
}
