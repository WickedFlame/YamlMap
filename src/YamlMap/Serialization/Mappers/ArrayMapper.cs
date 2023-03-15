using System.Collections;

namespace YamlMap.Serialization.Mappers
{
    /// <summary>
    /// Mapper for arrays
    /// </summary>
    public class ArrayMapper : BaseObjectMapper, IObjectMapper
    {
        /// <summary>
        /// Map the token to the object
        /// </summary>
        /// <param name="token"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Map(IToken token, object item)
        {
            if (!(item is IList array))
            {
                return false;
            }

            var index = GetFirstFreeIndex(array);

            if (AddValueToken(token, valueToken => array[index] = valueToken.Value))
            {
                return true;
            }

            var type = item.GetType();
            return AddChildNode(type.GetElementType(), token, child => array[index] = child.Node);
        }

        private int GetFirstFreeIndex(IList array)
        {
            for (var i = 0; i < array.Count; i++)
            {
                if (array[i] == null)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}
