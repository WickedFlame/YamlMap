using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace YamlMap.Serialization.Mappers
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
            //if (type.IsGenericType)
            //{
            //    type = type.GetGenericArguments()[0];
            //}

            _properties = type.GetProperties();
        }

        public bool Map(IToken token, object item)
        {
	        if (!(token is ValueToken valueToken))
	        {
		        return false;
	        }

	        if (string.IsNullOrEmpty(valueToken.Key))
	        {
		        return false;
	        }

	        var propertyInfo = _properties.FirstOrDefault(p => p.Name.ToLower() == valueToken.Key?.ToLower());
	        if (propertyInfo == null)
	        {
		        return false;
	        }

			return ParsePrimitive(propertyInfo, item, valueToken.Value);
        }

        public bool ParsePrimitive(PropertyInfo prop, object item, object value)
        {
	        if (!_supportedTypes.Contains(prop.PropertyType) && !prop.PropertyType.IsEnum)
	        {
		        return false;
	        }

	        var converted = TypeConverter.Convert(prop.PropertyType, value?.ToString());
	        if (converted == null)
	        {
		        return false;
	        }

	        prop.SetValue(item, converted, null);
	        return true;
		}
    }
}
