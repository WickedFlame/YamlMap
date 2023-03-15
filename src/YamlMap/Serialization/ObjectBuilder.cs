using System;
using System.Collections;
using System.Collections.Generic;

namespace YamlMap.Serialization
{
    /// <summary>
    /// Factory Class that generates instances of a type
    /// </summary>
    public static class ObjectBuilder
    {
        private static readonly Dictionary<Type, Type> genericInterfaceImplementations = new Dictionary<Type, Type>
        {
            { typeof(IEnumerable<>), typeof(List<>) },
            { typeof(ICollection<>), typeof(List<>) },
            { typeof(IList<>), typeof(List<>) },
            { typeof(IDictionary<,>), typeof(Dictionary<,>) }
        };

        private static readonly Dictionary<Type, Type> nonGenericInterfaceImplementations = new Dictionary<Type, Type>
        {
            { typeof(IEnumerable), typeof(List<object>) },
            { typeof(ICollection), typeof(List<object>) },
            { typeof(IList), typeof(List<object>) },
            { typeof(IDictionary), typeof(Dictionary<object, object>) }
        };

        /// <summary>
        /// Create a instance of the type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        /// <exception cref="InvalidConfigurationException"></exception>
        public static object CreateInstance(this Type type, IToken token)
        {
            if (type.IsArray)
            {
                var arrayType = type.GetElementType();
                if(arrayType != null)
                {
                    return ArrayInstanceFactory.Factory.CreateInstance(arrayType, token).Invoke();
                }
            }

            if (type.IsGenericTypeDefinition)
            {
                return GenericTypeInstanceFactory.Factory.CreateInstance(type, token).Invoke();
            }

            if (type.IsInterface)
            {
                if (type.IsGenericType)
                {
                    if (genericInterfaceImplementations.TryGetValue(type.GetGenericTypeDefinition(), out var implementationType))
                    {
                        type = implementationType.MakeGenericType(type.GetGenericArguments());
                    }
                }
                else
                {
                    if (nonGenericInterfaceImplementations.TryGetValue(type, out var implementationType))
                    {
                        type = implementationType;
                    }
                }
            }

			if (type == typeof(string))
			{
				return string.Empty;
			}

			try
            {
                return InstanceFactory.Factory.CreateInstance(type, token).Invoke();
            }
            catch (Exception e)
            {
	            throw new YamlSerializationException($"Could not create an instance of Type {type.FullName}", e);
			}
        }
    }
}
