using System;

namespace YamlMap.Serialization
{
    /// <summary>
    /// Interface to define a factory for creating instances of types
    /// </summary>
    public interface IInstanceFactory
    {
        /// <summary>
        /// Create a instance of the type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        Func<object> CreateInstance(Type type, IToken[] tokens);
    }
}
