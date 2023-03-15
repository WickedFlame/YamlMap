using System.Collections;

namespace YamlMap.Serialization.Mappers
{
    /// <summary>
    /// Mapper or lists
    /// </summary>
    public class GenericListMapper : BaseObjectMapper, IObjectMapper
    {
        /// <summary>
        /// Map the token to the list
        /// </summary>
        /// <param name="token"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Map(IToken token, object item)
        {
            var list = item as IList;
            if (list == null)
            {
                return false;
            }

            var type = item.GetType();
            var valueType = type.GetGenericArguments()[0];

            if (AddValueToken(token, valueToken => list.Add(TypeConverter.Convert(valueType, valueToken.Value))))
            {
                return true;
            }

            return AddChildNode(valueType, token, child => list.Add(child.Node));
        }
    }
}
