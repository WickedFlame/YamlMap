using System;
using System.Reflection;

namespace YamlMap.Serialization
{
    /// <summary>
    /// Objectcontract for creating objects
    /// </summary>
    public class ObjectContract
    {
        /// <summary>
        /// Gets the type that the object is of
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// Gets the <see cref="ConstructorInfo"/> for creating the new object
        /// </summary>
        public ConstructorInfo Constructor { get; set; }

        /// <summary>
        /// Gets a array of parameters used to create the object
        /// </summary>
        public object[] Parameters { get; set; }
    }
}
