using System;
using WickedFlame.Yaml.Serialization;

namespace WickedFlame.Yaml
{
	public class Serializer
	{
		public static string Serialize<T>(T item)
		{
			var tokenSerializer = new TokenSerializer();
			return tokenSerializer.Serialize(item);
		}

		public static T Deserialize<T>(string yaml)
		{
			throw new NotImplementedException();
		}
	}
}
