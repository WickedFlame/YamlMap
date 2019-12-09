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

        public PropertyInfo GetProperty(IToken token)
        {
            return _properties.FirstOrDefault(p => p.Name == token.Key);
        }

        public bool TryAppendProperty(IToken token, object item)
        {
            if (string.IsNullOrEmpty(token.Key))
            {
                return false;
            }

            var propertyInfo = _properties.FirstOrDefault(p => p.Name == token.Key);
            if (propertyInfo == null)
            {
                return false;
            }

            return PropertyMapper.ParsePrimitive(propertyInfo, item, ((ValueToken)token).Value);
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
                typeof(DateTime?),
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

    }
}
