using System;

namespace WickedFlame.Yaml.Serialization.Mappers
{
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

            // add the element to the list
            addChild(child);

            // refactor the line to be parsed as property
            for (var i = 0; i < token.Count; i++)
            {
                child.Deserialize(token[i]);
            }

            return true;
        }
    }
}
