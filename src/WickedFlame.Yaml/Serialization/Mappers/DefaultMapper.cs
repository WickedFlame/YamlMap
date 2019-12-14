using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WickedFlame.Yaml.Serialization.Mappers
{
    public class DefaultMapper : IObjectMapper
    {
        static readonly IEnumerable<Type> _supportedTypes = new List<Type>
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
            typeof(DateTime?),
            typeof(Type),
            typeof(Guid)
        };

        private readonly PropertyInfo[] _properties;

        public DefaultMapper(Type type)
        {
            if (type.IsGenericType)
            {
                type = type.GetGenericArguments()[0];
            }

            _properties = type.GetProperties();
        }

        public bool Map(IToken token, object item)
        {
            if (!(token is ValueToken))
            {
                return false;
            }

            if (string.IsNullOrEmpty(token.Key))
            {
                return false;
            }

            var propertyInfo = _properties.FirstOrDefault(p => p.Name == token.Key);
            if (propertyInfo == null)
            {
                return false;
            }

            return ParsePrimitive(propertyInfo, item, ((ValueToken)token).Value);
        }

        public bool ParsePrimitive(PropertyInfo prop, object entity, object value)
        {
            if (!_supportedTypes.Contains(prop.PropertyType))
            {
                return false;
            }

            var converted = TypeConverter.Convert(prop.PropertyType, value?.ToString());
            prop.SetValue(entity, converted, null);
            return true;
        }

    }
}
