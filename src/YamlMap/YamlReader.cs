using System;
using System.IO;
using YamlMap.Serialization;

namespace YamlMap
{
    //https://docs.ansible.com/ansible/latest/reference_appendices/YAMLSyntax.html

    /// <summary>
    /// Deserialize a yaml string to a object
    /// </summary>
    public class YamlReader
    {
        /// <summary>
        /// Read a yaml string and map it to a object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="yaml"></param>
        /// <returns></returns>
        public T Read<T>(string yaml) where T : class, new()
        {
            var lines = yaml.SplitToLines();

            return Read<T>(lines);
        }

        /// <summary>
        /// Read a yaml string amd map to a object of the given type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="yaml"></param>
        /// <returns></returns>
        public object Read(Type type, string yaml)
        {
            var lines = yaml.SplitToLines();

            return Read(type, lines);
        }

        /// <summary>
        /// Read a array of yaml string and map it to a object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lines"></param>
        /// <returns></returns>
        public T Read<T>(string[] lines) where T : class, new()
        {
            return (T)Read(typeof(T), lines);
        }

        /// <summary>
        /// Read a array of yaml string amd map to a object of the given type
        /// </summary>
        /// <param name="type"></param>
        /// <param name="lines"></param>
        /// <returns></returns>
        public object Read(Type type, string[] lines)
        {
            var scanner = new Scanner(lines);
            var parser = new Parser(scanner);
            var tokens = parser.Parse();

            var deserializer = new TokenDeserializer(type, tokens);

            for (var i = 0; i < tokens.Count; i++)
            {
                deserializer.Deserialize(tokens[i]);
            }

            return deserializer.Node;
        }
    }
}
