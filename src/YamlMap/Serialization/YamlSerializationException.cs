using System;
using System.Runtime.Serialization;

namespace YamlMap.Serialization
{
    /// <summary>
    /// Exception that gets thrwon when a object cannot be created
    /// </summary>
    [Serializable]
    public class YamlSerializationException : Exception
    {
        /// <summary>
        /// Creates a new Excetion that can be thrown when a object cannot be created
        /// </summary>
        /// <param name="message"></param>
        /// <param name="exception"></param>
        public YamlSerializationException(string message, Exception exception)
            : base(message, exception)
        {
        }

        /// <summary>
        /// Serializer ctor
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected YamlSerializationException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
