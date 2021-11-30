using System;
using System.Collections.Generic;
using System.Text;

namespace YamlMap.Tests
{
    public class YamlRoot
    {
        public string Id { get; set; }

        public SimpleObject SimpleObject { get; set; }

        public NestedObject Nested { get; set; }

        public List<string> StringList { get; set; }

        public IEnumerable<string> EnumerableList { get; set; }

        public Dictionary<string, string> Dictionary { get; set; }

        public IEnumerable<SimpleObject> ObjectList { get; set; }

        public IEnumerable<NestedObject> NestedObjectList { get; set; }
    }

    public class SimpleObject
    {
        public string Name { get; set; }

        public string Id { get; set; }
    }

    public class NestedObject
    {
        public string Name { get; set; }

        public NestedObject Nested { get; set; }
    }
}
