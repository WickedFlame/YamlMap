using System;

namespace YamlMap
{
    public class InvalidConfigurationException : Exception
    {
        public InvalidConfigurationException(string message) 
            : base(message)
        {
        }

        public InvalidConfigurationException(string message, Exception exception)
            : base(message, exception)
        {
        }
    }
}
