using System;
using System.Collections;
using System.Collections.Generic;

namespace WickedFlame.Yaml
{
    public class YamlNodeReader : INodeReader
    {
        private readonly PropertyMapper _mapper;
        private readonly object _item;

        private INodeReader _child;

        private int _indentation = -1;

        public YamlNodeReader(Type type)
        {
            _mapper = new PropertyMapper(type);
            _item = type.CreateInstance();
        }

        public object Node => _item;

        public void ReadLine(YamlLine line)
        {
            if (_indentation < 0)
            {
                // setup for current indentation
                _indentation = line.Indentation;
            }

            if (_indentation < line.Indentation)
            {
                _child.ReadLine(line);
                return;
            }

            // no further indentation so assue the property is on the same line
            _child = null;

            if (line.IsListItem)
            {
                // check if it is a property or just a string
                var property = _mapper.GetProperty(line);
                if (property != null)
                {
                    // get the inner type of the generic list
                    var nodeType = Node.GetType();
                    if (nodeType.IsGenericType)
                    {
                        nodeType = nodeType.GetGenericArguments()[0];
                    }

                    // create a new reader for the list type
                    _child = new YamlNodeReader(nodeType);

                    // add the element to the list
                    Node.GetType().GetMethod("Add").Invoke(Node, new[] { _child.Node });

                    // refactor the line to be parsed as property
                    var node = new YamlLine(line.Original.Replace("- ", "  "));
                    _child.ReadLine(node);
                }
                else
                {
                    // string value

                    //Node.GetType().GetMethod("Add").Invoke(Node, new[] { line.Value });
                    var nodeType = Node.GetType();
                    if (nodeType.IsGenericType)
                    {
                        if (nodeType.GetGenericTypeDefinition() == typeof(List<>))
                        {
                            var childNodeType = nodeType.GetGenericArguments()[0];
                            Node.GetType().GetMethod("Add").Invoke(Node, new[] { TypeConverter.Convert(childNodeType, line.Value) });
                        }
                        else if (nodeType.GetGenericTypeDefinition() == typeof(Dictionary<,>))
                        {
                            var keytype = nodeType.GetGenericArguments()[0];
                            var valuetype = nodeType.GetGenericArguments()[1];
                            Node.GetType().GetMethod("Add").Invoke(Node, new[] { TypeConverter.Convert(keytype, line.Property), TypeConverter.Convert(valuetype, line.Value) });
                        }
                    }
                    else if (nodeType.IsArray)
                    {
                        if (Node.GetType().GetInterface("IList") != null)
                        {


                            var a = (IList) Node;
                            //for (int i = 0; i < a.Length; i++)
                            //{
                            //    object o = a.GetValue(i);
                            //}
                            //Array.Resize(ref a, a.Length + 1);
                            a.Add(line.Value);
                        }
                    }
                    else
                    {
                        throw new InvalidConfigurationException($"The configured Property {line.Property} does not implement a list type");
                    }

                    
                }

                return;
            }

            if (_mapper.TryAppendProperty(line, _item))
            {
                return;
            }

            if (!string.IsNullOrEmpty(line.Property))
            {
                var property = _mapper.GetProperty(line);
                if (property == null)
                {
                    throw new InvalidConfigurationException($"The configured Property {line.Property} does not exist in the Type {Node.GetType().FullName}");
                }

                _child = new YamlNodeReader(property.PropertyType);
                property.SetValue(Node, _child.Node, null);
            }
        }
    }
}
