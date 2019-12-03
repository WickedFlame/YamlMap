using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace WickedFlame.Yaml
{
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
            if (string.IsNullOrEmpty(line.Property))
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
            var types = new List<Type>
            {
                typeof(string),
                typeof(bool),
                typeof(bool?),
                typeof(long),
                typeof(long?),
                typeof(int),
                typeof(int?),
                typeof(decimal),
                typeof(double),
                typeof(double?),
                typeof(DateTime),
                typeof(Nullable<DateTime>),
                typeof(Type),
                typeof(Guid)
            };

            if (types.Contains(prop.PropertyType))
            {
                var converted = TypeConverter.Convert(prop.PropertyType, value?.ToString());
                prop.SetValue(entity, converted, null);
                return true;
            }

            return false;
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
}
