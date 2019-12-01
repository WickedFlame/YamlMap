using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace WickedFlame.Yaml
{
    //https://docs.ansible.com/ansible/latest/reference_appendices/YAMLSyntax.html
    public class YamlReader
    {
        public T Read<T>(string file) where T : class, new()
        {
            var reader = new YamlNodeReader(typeof(T));

            foreach (var line in ReadAllLines(file))
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                if (line.StartsWith("#"))
                {
                    continue;
                }

                var meta = new YamlLine(line);

                reader.ReadLine(meta);
            }

            return (T)reader.Node;
        }

        public T Read<T>(string[] lines) where T : class, new()
        {
            var reader = new YamlNodeReader(typeof(T));

            foreach (var line in lines)
            {
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }

                if (line.StartsWith("#"))
                {
                    continue;
                }

                var meta = new YamlLine(line);

                reader.ReadLine(meta);
            }

            return (T)reader.Node;
        }

        public string[] ReadAllLines(string file)
        {
            if (!File.Exists(file))
            {
                file = FindFile(file);
            }

            return File.ReadAllLines(file);
        }

        private string FindFile(string file)
        {
            if (!file.Contains("/") && !file.Contains("\\"))
            {
                var hostingRoot = AppDomain.CurrentDomain.BaseDirectory;
                file = LoadPath(file, hostingRoot);
            }

            return file;
        }

        private string LoadPath(string file, string root)
        {
            var path = Path.Combine(root, file);
            foreach (var f in Directory.GetFiles(root))
            {
                if (path == f)
                {
                    return f;
                }
            }

            foreach (var dir in Directory.GetDirectories(root))
            {
                var f = LoadPath(file, dir);
                if (!string.IsNullOrEmpty(f))
                {
                    return f;
                }
            }

            return null;
        }
    }

    

    public interface INodeReader
    {
        object Node { get; }

        void ReadLine(YamlLine line);
    }

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
                var property = _mapper.GetProperty(line);
                if(property != null)
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
                    Node.GetType().GetMethod("Add").Invoke(Node, new[] {line.Value});
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
                _child = new YamlNodeReader(property.PropertyType);
                property.SetValue(Node, _child.Node, null);
            }
        }
    }

    public class PropertyMapper
    {
        private readonly PropertyInfo[] _properties;

        public PropertyMapper(Type type)
        {
            if (type.IsGenericType)
            {
                type = type.GetGenericArguments()[0];
            }

            _properties = type.GetProperties();
        }

        public bool TryAppendProperty(YamlLine line, object item)
        {
            if(string.IsNullOrEmpty(line.Property))
            {
                return false;
            }

            var propertyInfo = _properties.FirstOrDefault(p => p.Name == line.Property);
            if (propertyInfo == null)
            {
                return false;
            }

            return PropertyMapper.ParsePrimitive(propertyInfo, item, line.Value);
        }

        public PropertyInfo GetProperty(YamlLine line)
        {
            return _properties.FirstOrDefault(p => p.Name == line.Property);
        }

        public static bool ParsePrimitive(PropertyInfo prop, object entity, object value)
        {
            if (prop.PropertyType == typeof(string))
            {
                prop.SetValue(entity, value.ToString().Trim(), null);
            }
            else if (prop.PropertyType == typeof(bool) || prop.PropertyType == typeof(bool?))
            {
                if (value == null)
                {
                    prop.SetValue(entity, null, null);
                }
                else
                {
                    prop.SetValue(entity, ParseBoolean(value.ToString()), null);
                }
            }
            else if (prop.PropertyType == typeof(long))
            {
                prop.SetValue(entity, long.Parse(value.ToString()), null);
            }
            else if (prop.PropertyType == typeof(int) || prop.PropertyType == typeof(int?))
            {
                if (value == null)
                {
                    prop.SetValue(entity, null, null);
                }
                else
                {
                    prop.SetValue(entity, int.Parse(value.ToString()), null);
                }
            }
            else if (prop.PropertyType == typeof(decimal))
            {
                prop.SetValue(entity, decimal.Parse(value.ToString()), null);
            }
            else if (prop.PropertyType == typeof(double) || prop.PropertyType == typeof(double?))
            {
                var isValid = double.TryParse(value.ToString(), out _);
                if (isValid)
                {
                    prop.SetValue(entity, double.Parse(value.ToString()), null);
                }
            }
            else if (prop.PropertyType == typeof(DateTime) || prop.PropertyType == typeof(Nullable<DateTime>))
            {
                var isValid = DateTime.TryParse(value.ToString(), out var date);
                if (isValid)
                {
                    prop.SetValue(entity, date, null);
                }
                else
                {
                    isValid = DateTime.TryParseExact(value.ToString(), "yyyyMMdd", new CultureInfo("de-CH"), DateTimeStyles.AssumeLocal, out date);
                    if (isValid)
                    {
                        prop.SetValue(entity, date, null);
                    }
                }
            }
            else if (prop.PropertyType == typeof(Guid))
            {
                var isValid = Guid.TryParse(value.ToString(), out var guid);
                if (isValid)
                {
                    prop.SetValue(entity, guid, null);
                }
                else
                {
                    isValid = Guid.TryParseExact(value.ToString(), "B", out guid);
                    if (isValid)
                    {
                        prop.SetValue(entity, guid, null);
                    }
                }
            }
            else if (prop.PropertyType == typeof(Type))
            {
                var type = Type.GetType(value.ToString());
                prop.SetValue(entity, type, null);
            }
            else
            {
                return false;
            }

            return true;
        }

        public static bool ParseBoolean(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return false;
            }

            switch (value.ToString().ToLowerInvariant())
            {
                case "1":
                case "y":
                case "yes":
                case "true":
                    return true;

                case "0":
                case "n":
                case "no":
                case "false":
                default:
                    return false;
            }
        }
    }


    internal static class TypeExtensions
    {
        //public static ConstructorInfo GetEmptyConstructor(this Type type)
        //{
        //    return type.GetConstructor(Type.EmptyTypes);
        //}

        public static bool HasGenericType(this Type type)
        {
            while (type != null)
            {
                if (type.IsGenericType)
                {
                    return true;
                }

                type = type.BaseType;
            }

            return false;
        }

        public static Type GetTypeWithGenericTypeDefinitionOfAny(this Type type, params Type[] genericTypeDefinitions)
        {
            foreach (var genericTypeDefinition in genericTypeDefinitions)
            {
                var genericType = type.GetTypeWithGenericTypeDefinitionOf(genericTypeDefinition);
                if (genericType == null && type == genericTypeDefinition)
                {
                    genericType = type;
                }

                if (genericType != null)
                {
                    return genericType;
                }
            }

            return null;
        }

        public static Type GetTypeWithGenericTypeDefinitionOf(this Type type, Type genericTypeDefinition)
        {
            foreach (var t in type.GetInterfaces())
            {
                if (t.IsGenericType && t.GetGenericTypeDefinition() == genericTypeDefinition)
                {
                    return t;
                }
            }

            var genericType = type.GetGenericType();
            if (genericType != null && genericType.GetGenericTypeDefinition() == genericTypeDefinition)
            {
                return genericType;
            }

            return null;
        }

        public static Type GetGenericType(this Type type)
        {
            while (type != null)
            {
                if (type.IsGenericType)
                {
                    return type;
                }

                type = type.BaseType;
            }

            return null;
        }
    }
}
