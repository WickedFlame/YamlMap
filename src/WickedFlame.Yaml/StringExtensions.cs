
namespace WickedFlame.Yaml
{
    public static class StringExtensions
    {
        public static string Indent(this string value, int indentation)
        {
            return "".PadLeft(indentation) + value;
        }
    }
}
