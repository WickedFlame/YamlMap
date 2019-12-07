using System;

namespace WickedFlame.Yaml
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
