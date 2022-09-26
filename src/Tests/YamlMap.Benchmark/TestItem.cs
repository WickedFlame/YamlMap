
namespace YamlMap.Benchmark
{
    public class TestlItem
    {
        public string Simple { get; set; }

        public TestlItemChild Child { get; set; }

        public IEnumerable<string> StringList { get; set; }

        public IEnumerable<TestlItemChild> ObjList { get; set; }
    }

    public class TestlItemChild
    {
        public string Simple { get; set; }

        public TestlItem Child { get; set; }

        public IEnumerable<string> StringList { get; set; }

        public IEnumerable<TestlItem> ObjList { get; set; }
    }
}
