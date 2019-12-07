using System;
using System.Collections.Generic;
using System.Text;

namespace WickedFlame.Yaml
{
    public class YamlNodeMapper
    {
        private readonly PropertyMapper _mapper;
        private readonly object _item;

        public YamlNodeMapper(Type type)
        {
            _mapper = new PropertyMapper(type);
            _item = type.CreateInstance();
        }

        public void MapToken(IToken token)
        {
            if(token is ValueToken)
            {
                if(_mapper.TryAppendProperty(token, _item))
                {
                    return;
                }

                //try add simple value to list
                var nodeType = Node.GetType();
                if (nodeType.IsGenericType)
                {
                    if (nodeType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        var childNodeType = nodeType.GetGenericArguments()[0];
                        Node.GetType().GetMethod("Add").Invoke(Node, new[] {TypeConverter.Convert(childNodeType, ((ValueToken) token).Value)});
                        return;
                    }
                }
            }


            var property = _mapper.GetProperty(token);



            // check if it is a property or just a string
            if (token.TokenType == TokenType.ListItem)
            {
                // get the inner type of the generic list
                var nodeType = Node.GetType();
                if (nodeType.IsGenericType)
                {
                    if (nodeType.GetGenericTypeDefinition() == typeof(List<>))
                    {
                        nodeType = nodeType.GetGenericArguments()[0];

                        // create a new reader for the list type
                        var c = new YamlNodeMapper(nodeType);

                        // add the element to the list
                        Node.GetType().GetMethod("Add").Invoke(Node, new[] {c.Node});

                        // refactor the line to be parsed as property
                        foreach (var t in token)
                        {
                            c.MapToken(t);
                        }
                    }
                    else if (nodeType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                    {
                        var keytype = nodeType.GetGenericArguments()[0];
                        var valuetype = nodeType.GetGenericArguments()[1];

                        if (token[0] is ValueToken subtoken)
                        {
                            Node.GetType().GetMethod("Add").Invoke(Node, new[] { TypeConverter.Convert(keytype, subtoken.Key), TypeConverter.Convert(valuetype, subtoken.Value) });
                            return;
                        }

                        var tmp = token[0];
                        var c = new YamlNodeMapper(valuetype);

                        Node.GetType().GetMethod("Add").Invoke(Node, new[] { TypeConverter.Convert(keytype, tmp.Key), /*TypeConverter.Convert(valuetype, c.Node) */c.Node});

                        // refactor the line to be parsed as property
                        foreach (var t in tmp)
                        {
                            c.MapToken(t);
                        }
                    }
                }

                return;
            }






            if (property == null)
            {
                throw new InvalidConfigurationException($"The configured Property {token.Key} does not exist in the Type {Node.GetType().FullName}");
            }

            //var child = InstanceFactory.CreateInstance(property.PropertyType);
            var child = new YamlNodeMapper(property.PropertyType);
            property.SetValue(Node, child.Node, null);
            foreach (var t in token)
            {
                child.MapToken(t);
            }

        }

        public object Node => _item;
    }
}
