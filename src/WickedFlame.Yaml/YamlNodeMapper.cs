using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace WickedFlame.Yaml
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
            if(token is ValueToken valueToken)
            {
                if(_mapper.TryAppendProperty(valueToken, _item))
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
                        try
                        {
                            Node.GetType().GetMethod("Add").Invoke(Node, new[] {TypeConverter.Convert(childNodeType, valueToken.Value)});
                        }
                        catch (Exception e)
                        {
                            System.Diagnostics.Trace.WriteLine(e);
                        }

                        return;
                    }
                }
                else if (nodeType.IsArray)
                {
                    if(Node is IList arr)
                    {
                        for (int i = 0; i < arr.Count; i++)
                        {
                            // try find the next slot that is empty
                            if (arr[i] != null)
                            {
                                continue;
                            }

                            arr[i] = valueToken.Value;
                            return;
                        }
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
                        var c = new YamlNodeMapper(nodeType, token);

                        // add the element to the list
                        Node.GetType().GetMethod("Add").Invoke(Node, new[] {c.Node});

                        // refactor the line to be parsed as property
                        for (int i = 0; i < token.Count; i++)
                        {
                            c.MapToken(token[i]);
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
                        var c = new YamlNodeMapper(valuetype, token);

                        Node.GetType().GetMethod("Add").Invoke(Node, new[] { TypeConverter.Convert(keytype, tmp.Key), c.Node});

                        // refactor the line to be parsed as property
                        for (int i = 0; i < tmp.Count; i++)
                        {
                            c.MapToken(tmp[i]);
                        }
                    }

                    if (nodeType.IsArray)
                    {
                        if (Node is IList arr)
                        {
                            for (var i = 0; i < arr.Count; i++)
                            {
                                // try find the next slot that is empty
                                if (arr[i] != null)
                                {
                                    continue;
                                }

                                var c = new YamlNodeMapper(property.PropertyType, token);
                                arr[i] = c.Node;
                                
                                for (var i2 = 0; i2 < token.Count; i2++)
                                {
                                    c.MapToken(token[i]);
                                }

                                break;
                            }
                        }
                    }
                }

                return;
            }


            if (property == null)
            {
                throw new InvalidConfigurationException($"The configured Property {token.Key} does not exist in the Type {Node.GetType().FullName}");
            }

            var child = new YamlNodeMapper(property.PropertyType, token);
            property.SetValue(Node, child.Node, null);
            for (int i = 0; i < token.Count; i++)
            {
                child.MapToken(token[i]);
            }

        }

        public object Node => _item;
    }
}
