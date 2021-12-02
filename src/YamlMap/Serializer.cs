using System;
using YamlMap.Serialization;

namespace YamlMap
{
	/// <summary>
	/// Serializer for yaml strings
	/// </summary>
	public static class Serializer
	{
		/// <summary>
		/// Serialize a object to a yaml string
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="item"></param>
		/// <returns></returns>
		public static string Serialize<T>(T item)
        {
            var writer = new YamlWriter();
			return writer.Write(item);
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
