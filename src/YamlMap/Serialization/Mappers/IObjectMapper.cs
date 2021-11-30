
namespace YamlMap.Serialization.Mappers
{
    public interface IObjectMapper
    {
        bool Map(IToken token, object item);
    }
}
