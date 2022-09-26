using System;

namespace YamlMap.Serialization
{
    /// <summary>
    /// Default factory to create instances of objects
    /// </summary>
    public class InstanceFactory : IInstanceFactory
    {
        /// <summary>
        /// Singleton instance of the factory
        /// </summary>
        public static IInstanceFactory Factory { get; } = new InstanceFactory();

        /// <summary>
        /// Create an instance of the type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="tokens"></param>
        /// <returns></returns>
        public Func<object> CreateInstance(Type type, IToken[] tokens)
        {
            var resolver = new ContractResolver();
            var contract = resolver.GetConstructor(type);
            contract.Parameters = resolver.CreateConstructoParameters(contract.Constructor, tokens);

            return () => CreateInstance(contract);
        }

        private object CreateInstance(ObjectContract contract)
        {
            if (contract.Parameters != null && contract.Parameters.Length == 0)
            {
                return Activator.CreateInstance(contract.Type);
            }

            return Activator.CreateInstance(contract.Type, contract.Parameters);
        }
    }
}
