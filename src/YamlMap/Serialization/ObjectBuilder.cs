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
                return Array.CreateInstance(type.GetElementType(), token.Count);
            }

            if (type.IsGenericTypeDefinition)
            {
                var genericArgs = type.GetGenericArguments();
                var typeArgs = new Type[genericArgs.Length];
                for (var i = 0; i < genericArgs.Length; i++)
                {
                    typeArgs[i] = typeof(object);
                }

                var realizedType = type.MakeGenericType(typeArgs);
                return realizedType.CreateInstance(token);
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
                //return Activator.CreateInstance(type);

                return InstanceFactory.Factory.CreateInstance(type, token.GetChildTokens()).Invoke();
            }
            catch (Exception e)
            {
	            throw new InvalidConfigurationException($"Could not create an instance of Type {type.FullName}", e);
			}
        }
    }
}
