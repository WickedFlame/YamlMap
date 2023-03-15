using System;

namespace YamlMap.Serialization.Mappers
{
    /// <summary>
    /// baseclass for mappers
    /// </summary>
    public abstract class BaseObjectMapper
    {
        protected bool AddValueToken(IToken token, Action<ValueToken> addValue)
        {
            if (token is ValueToken valueToken)
            {
                addValue(valueToken);
                return true;
            }

            return false;
        }

        protected bool AddChildNode(Type type, IToken token, Action<TokenDeserializer> addChild)
        {
            // create a new reader for the list type
            var child = new TokenDeserializer(type, token);
            child.DeserializeChildren();
            
            // add the element to the list
            addChild(child);
            
            return true;
        }
    }
}
