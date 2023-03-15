using System;

namespace YamlMap.Serialization.Mappers
{
    /// <summary>
    /// baseclass for mappers
    /// </summary>
    public abstract class BaseObjectMapper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="token"></param>
        /// <param name="addValue"></param>
        /// <returns></returns>
        protected bool AddValueToken(IToken token, Action<ValueToken> addValue)
        {
            if (token is ValueToken valueToken)
            {
                addValue(valueToken);
                return true;
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="token"></param>
        /// <param name="addChild"></param>
        /// <returns></returns>
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
