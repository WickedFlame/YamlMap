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

		/// <summary>
		/// Deserialize a string to a object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="yaml"></param>
		/// <returns></returns>
		public static T Deserialize<T>(string yaml) where T : class, new()
		{
            var reader = new YamlReader();
			return reader.Read<T>(yaml);
        }

		/// <summary>
		/// Deserialize a string to a object
		/// </summary>
		/// <param name="type"></param>
		/// <param name="yaml"></param>
		/// <returns></returns>
		public static object Deserialize(Type type, string yaml)
        {
            var reader = new YamlReader();
            return reader.Read(type, yaml);
		}
    }
}
