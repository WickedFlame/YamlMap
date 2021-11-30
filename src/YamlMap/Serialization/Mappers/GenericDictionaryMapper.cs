using System;
using System.Collections;

namespace YamlMap.Serialization.Mappers
{
    public class GenericDictionaryMapper : BaseObjectMapper, IObjectMapper
    {
        public bool Map(IToken token, object item)
        {
            var dictionary = item as IDictionary;
            if (dictionary == null)
            {
                return false;
            }

            var type = item.GetType();
            var keytype = type.GetGenericArguments()[0];
            var valuetype = type.GetGenericArguments()[1];

            if (AddValueToken(token, valueToken => dictionary.Add(TypeConverter.Convert(keytype, valueToken.Key), TypeConverter.Convert(valuetype, valueToken.Value))))
            {
                return true;
            }

            return AddChildNode(valuetype, token, child => dictionary.Add(TypeConverter.Convert(keytype, token.Key), child.Node));
        }
    }
}
