using System;
using System.Collections.Generic;
using System.Text;
using YamlMap.Serialization;

namespace YamlMap
{
    public class YamlWriter
    {
        public string Write<T>(T item)
        {
	        var serialized = Serializer.Serialize(item);

	        return serialized;
        }
    }
}
