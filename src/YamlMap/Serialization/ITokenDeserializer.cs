﻿
namespace YamlMap.Serialization
{
    public interface ITokenDeserializer
    {
        object Node { get; }

        void Deserialize(IToken token);

        void DeserializeChildren();
    }
}
