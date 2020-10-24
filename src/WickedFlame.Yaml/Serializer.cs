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
			var deserializer = new TokenDeserializer(typeof(T), null);

			var lines = yaml.Split(new[] {Environment.NewLine}, StringSplitOptions.None);

			var scanner = new Scanner(lines);
			var parser = new Parser(scanner);
			var tokens = parser.Parse();

			for (var i = 0; i < tokens.Count; i++)
			{
				deserializer.Deserialize(tokens[i]);
			}

			return (T)deserializer.Node;
		}
	}
}
