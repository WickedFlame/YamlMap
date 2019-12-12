using System;
using System.Collections.Generic;
using System.Text;

namespace WickedFlame.Yaml.Serialization
{
    public interface ITokenDeserializer
    {
        void Deserialize(IToken token);
    }
}
