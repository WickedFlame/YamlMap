using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace WickedFlame.Yaml.Serialization
{
    internal static class TypeExtensions
    {
        private static Dictionary<Type, IEnumerable<PropertyInfo>> _propertyCache = new Dictionary<Type, IEnumerable<PropertyInfo>>();

        public static PropertyInfo GetProperty(this Type type, IToken token)
        {
            if (type.IsGenericType)
            {
                type = type.GetGenericArguments()[0];
            }

            if (!_propertyCache.ContainsKey(type))
            {
                _propertyCache.Add(type, type.GetProperties());
            }

            var properties = _propertyCache[type];
            return properties.FirstOrDefault(p => p.Name.ToLower() == token.Key?.ToLower());
        }

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
