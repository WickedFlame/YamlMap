using System;

namespace YamlMap.Serialization
{
    /// <summary>
    /// Default factory to create instances of arrays
    /// </summary>
    public class ArrayInstanceFactory : IInstanceFactory
    {
        /// <summary>
        /// Singleton instance of the factory
        /// </summary>
        public static IInstanceFactory Factory { get; } = new ArrayInstanceFactory();

        /// <summary>
        /// Create an instance of the type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public Func<object> CreateInstance(Type type, IToken token)
        {
            return () => Array.CreateInstance(type, token.Count);
        }
    }
}
