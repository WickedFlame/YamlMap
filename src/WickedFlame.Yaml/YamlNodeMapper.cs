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
                        try
                        {
                            Node.GetType().GetMethod("Add").Invoke(Node,
                                new[] {TypeConverter.Convert(childNodeType, ((ValueToken) token).Value)});
                        }
                        catch (Exception e)
                        {
                            System.Diagnostics.Trace.WriteLine(e);
                        }

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
                        var c = new YamlNodeMapper(valuetype);

                        Node.GetType().GetMethod("Add").Invoke(Node, new[] { TypeConverter.Convert(keytype, tmp.Key), c.Node});

                        // refactor the line to be parsed as property
                        for (int i = 0; i < tmp.Count; i++)
                        {
                            c.MapToken(tmp[i]);
                        }
                    }

                    //if (nodeType.IsArray)
                    //{
                    //    Array.Resize(ref Node, 10);
                    //}
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
            for (int i = 0; i < token.Count; i++)
            {
                child.MapToken(token[i]);
            }

        }

        public object Node => _item;
    }
}
