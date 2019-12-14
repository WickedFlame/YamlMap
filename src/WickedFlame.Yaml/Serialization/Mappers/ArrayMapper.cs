using System.Collections;

namespace WickedFlame.Yaml.Serialization.Mappers
{
    public class ArrayMapper : BaseObjectMapper, IObjectMapper
    {
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
