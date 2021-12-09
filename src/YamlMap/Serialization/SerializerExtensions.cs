using System.Linq;

namespace YamlMap.Serialization
{
	internal static class SerializerExtensions
    {
        private static readonly char[] SpecialChars = new[] { ':', '[', ']' };

        /// <summary>
        /// Convert a object to a string that is used for serialization
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string ToSerializeableString(this object value)
        {
            if (value == null)
            {
                return null;
            }

            if (value is string s && SpecialChars.Any(c => s.Contains(c)))
            {
                value = $"'{s}'";
            }

            return value.ToString();
        }
    }
}
