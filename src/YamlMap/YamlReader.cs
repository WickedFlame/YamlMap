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
            var lines = yaml.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

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
            var lines = yaml.Split(new[] { Environment.NewLine }, StringSplitOptions.None);

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

        private string FindFile(string file)
        {
            if (!file.Contains("/") && !file.Contains("\\"))
            {
                var hostingRoot = AppDomain.CurrentDomain.BaseDirectory;
                file = LoadPath(file, hostingRoot);
            }

            return file;
        }

        private string LoadPath(string file, string root)
        {
            var path = Path.Combine(root, file);
            foreach (var f in Directory.GetFiles(root))
            {
                if (path == f)
                {
                    return f;
                }
            }

            foreach (var dir in Directory.GetDirectories(root))
            {
                var f = LoadPath(file, dir);
                if (!string.IsNullOrEmpty(f))
                {
                    return f;
                }
            }

            return null;
        }
    }
}
