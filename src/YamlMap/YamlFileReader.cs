using System;
using System.IO;

namespace YamlMap
{
    //https://docs.ansible.com/ansible/latest/reference_appendices/YAMLSyntax.html

    /// <summary>
    /// Read a yaml file and parse it to a object
    /// </summary>
    public class YamlFileReader
    {
        private readonly YamlReader _reader;

        /// <summary>
        /// Creates a new reader to read yaml files
        /// </summary>
        public YamlFileReader()
        {
            _reader = new YamlReader();
        }

        /// <summary>
        /// Read the yaml file and parse it to a object
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="file"></param>
        /// <returns></returns>
        public T Read<T>(string file) where T : class, new()
        {
            return _reader.Read<T>(ReadAllLines(file));
        }

        private string[] ReadAllLines(string file)
        {
            if (!File.Exists(file))
            {
                file = FindFile(file);
            }

            return File.ReadAllLines(file);
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
