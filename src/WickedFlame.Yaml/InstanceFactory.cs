using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;

namespace WickedFlame.Yaml
{
    public delegate object EmptyConstructor();

    /// <summary>
    /// Factory Class that generates instances of a type
    /// </summary>
    internal static class InstanceFactory
    {
        /// <summary>
        /// Factory Method that creates an instance of type T
        /// </summary>
        /// <typeparam name="T">The type to create an instance of</typeparam>
        /// <returns>An instance of type T</returns>
        public static T CreateInstance<T>()
        {
            return (T)GetConstructorMethod(typeof(T)).Invoke();
        }

        public static object CreateInstance(this Type type, IToken token)
        {
            if (type == null)
            {
                return null;
            }

            try
            {
                return GetConstructorMethod(type, token).Invoke();
            }
            catch (Exception e)
            {
                throw new InvalidConfigurationException($"Could not create an instance of Type {type.FullName}", e);
            }
        }

        private static EmptyConstructor GetConstructorMethod(Type type, IToken token = null)
        {
            if (type.IsInterface)
            {
                if (type.HasGenericType())
                {
                    var genericType = type.GetTypeWithGenericTypeDefinitionOfAny(typeof(IDictionary<,>));

                    if (genericType != null)
                    {
                        var keyType = genericType.GetGenericArguments()[0];
                        var valueType = genericType.GetGenericArguments()[1];
                        return GetConstructorMethod(typeof(Dictionary<,>).MakeGenericType(keyType, valueType));
                    }

                    genericType = type.GetTypeWithGenericTypeDefinitionOfAny(typeof(IEnumerable<>), typeof(ICollection<>), typeof(IList<>));

                    if (genericType != null)
                    {
                        var elementType = genericType.GetGenericArguments()[0];
                        return GetConstructorMethod(typeof(List<>).MakeGenericType(elementType));
                    }
                }
            }
            else if (type.IsArray)
            {
                return () => Array.CreateInstance(type.GetElementType(), token.Count);
            }
            else if (type.IsGenericTypeDefinition)
            {
                var genericArgs = type.GetGenericArguments();
                var typeArgs = new Type[genericArgs.Length];
                for (var i = 0; i < genericArgs.Length; i++)
                {
                    typeArgs[i] = typeof(object);
                }

                var realizedType = type.MakeGenericType(typeArgs);
                //return type1 => realizedType.CreateInstance(type1);
                return () => realizedType.CreateInstance(token);
            }

            //var emptyCtor = type.GetEmptyConstructor();
            //if (emptyCtor != null)
            //{
            //    var dynamicMethod = new DynamicMethod("MyCtor", type, Type.EmptyTypes, type.Module, true);

            //    var ilGenerator = dynamicMethod.GetILGenerator();
            //    ilGenerator.Emit(System.Reflection.Emit.OpCodes.Nop);
            //    ilGenerator.Emit(System.Reflection.Emit.OpCodes.Newobj, emptyCtor);
            //    ilGenerator.Emit(System.Reflection.Emit.OpCodes.Ret);

            //    return (EmptyConstructorDelegate)dynamicMethod.CreateDelegate(typeof(EmptyConstructorDelegate));
            //}

            if (type == typeof(string))
            {
                return () => string.Empty;
            }

            // Anonymous types don't have empty constructors
            //return () => FormatterServices.GetUninitializedObject(type);
            return () => Activator.CreateInstance(type);
        }
    }
}
