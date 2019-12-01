using System;

namespace WickedFlame.Yaml
{
    public class InvalidConfigurationException : Exception
    {
        public InvalidConfigurationException(string message) 
            : base(message)
        {
        }
    }
}
