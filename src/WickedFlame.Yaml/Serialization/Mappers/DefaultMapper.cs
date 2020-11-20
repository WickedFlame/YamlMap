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

	        var propertyInfo = _properties.FirstOrDefault(p => p.Name.ToLower() == token.Key?.ToLower());
	        if (propertyInfo == null)
	        {
		        return false;
	        }

	        if (ParsePrimitive(propertyInfo, item, ((ValueToken) token).Value, TypeConverter.Convert))
	        {
		        return true;
	        }

	        if (propertyInfo.PropertyType == typeof(Type))
	        {
		        if (ParsePrimitive(propertyInfo, item, ((ValueToken) token).Value, (t, s) => Type.GetType(s)))
		        {
			        return true;
		        }
	        }

	        return false;
        }

        public bool ParsePrimitive(PropertyInfo prop, object entity, object value, Func<Type, string, object> converter)
        {
	        if (!_supportedTypes.Contains(prop.PropertyType))
	        {
		        return false;
	        }

	        var converted = converter(prop.PropertyType, value?.ToString());
	        if (converted == null)
	        {
		        return false;
	        }

	        prop.SetValue(entity, converted, null);
	        return true;
		}
    }
}
