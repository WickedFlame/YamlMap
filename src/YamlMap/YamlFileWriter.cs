using System;
using System.IO;

namespace YamlMap
{
    /// <summary>
    /// Write a yaml to a file
    /// </summary>
    public class YamlFileWriter
    {
        /// <summary>
        /// Serializes a object to yaml and saves the yaml in a file
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public string Write<T>(string fileName, T item)
        {
            var serialized = Serializer.Serialize(item);

            // save to file here
            WriteFile(fileName, serialized);

            return serialized;
        }

        private void WriteFile(string fileName, string yaml)
        {
            File.WriteAllText(fileName, yaml);
        }
    }
}
