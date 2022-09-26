using System;

namespace YamlMap.Serialization
{
    /// <summary>
    /// Default factory to create instances of Generic Types
    /// </summary>
    public class GenericTypeInstanceFactory : IInstanceFactory
    {
        /// <summary>
        /// Singleton instance of the factory
        /// </summary>
        public static IInstanceFactory Factory { get; } = new GenericTypeInstanceFactory();

        /// <summary>
        /// Create an instance of the type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Func<object> CreateInstance(Type type, IToken token)
        {
            var genericArgs = type.GetGenericArguments();
            var typeArgs = new Type[genericArgs.Length];
            for (var i = 0; i < genericArgs.Length; i++)
            {
                typeArgs[i] = typeof(object);
            }

            var realizedType = type.MakeGenericType(typeArgs);
            return () => realizedType.CreateInstance(token);
        }
    }
}
