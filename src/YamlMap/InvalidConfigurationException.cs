using System;
using System.Runtime.Serialization;

namespace YamlMap
{
    /// <summary>
    /// Exception that gets thrwon when the configuration is not correct
    /// </summary>
    [Serializable]
    public class InvalidConfigurationException : Exception
    {
        /// <summary>
        /// Creates a new Excetion that can be thrown when the configuration is not correct
        /// </summary>
        /// <param name="message"></param>
        public InvalidConfigurationException(string message) 
            : base(message)
        {
        }

        /// <summary>
        /// Serializer ctor
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected InvalidConfigurationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
