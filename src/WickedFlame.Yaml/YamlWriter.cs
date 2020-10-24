using System;
using System.Collections.Generic;
using System.Text;
using WickedFlame.Yaml.Serialization;

namespace WickedFlame.Yaml
{
    public class YamlWriter
    {
        public string Write<T>(T item)
        {
	        var serialized = Serializer.Serialize(item);

			// save to file here

	        return serialized;
        }
    }
}
