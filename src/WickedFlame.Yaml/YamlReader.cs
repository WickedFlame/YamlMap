using System;
using System.IO;
using WickedFlame.Yaml.Serialization;

namespace WickedFlame.Yaml
{
    //https://docs.ansible.com/ansible/latest/reference_appendices/YAMLSyntax.html
    public class YamlReader
    {
        public T Read<T>(string file) where T : class, new()
        {
            return Read<T>(ReadAllLines(file));
        }

        public T Read<T>(string[] lines) where T : class, new()
        {
            var reader = new YamlNodeMapper(typeof(T), null);

            var scanner = new Scanner(lines);
            var parser = new Parser(scanner);
            var tokens = parser.Parse();

            for (var i = 0; i < tokens.Count; i++)
            {
                reader.MapToken(tokens[i]);
            }

            return (T)reader.Node;
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
