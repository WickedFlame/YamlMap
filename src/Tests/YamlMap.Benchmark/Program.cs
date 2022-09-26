// See https://aka.ms/new-console-template for more information

using MeasureMap;
using System.Text;
using YamlMap;
using YamlMap.Benchmark;

var value = new StringBuilder().AppendLine("Simple: root")
    .AppendLine("StringList:")
    .AppendLine("  - one")
    .AppendLine("  - 2")
    .AppendLine("ObjList:")
    .AppendLine("  - Simple: simple")
    .AppendLine("  - Child:")
    .AppendLine("      Simple: child").ToString();


ProfilerSession.StartSession()
    .SetIterations(10)
    .SetThreads(10)
    .Task(c =>
    {
        var reader = new YamlReader();
        reader.Read<TestlItem>(value);
    })
    .RunSession()
    .Trace();


//ProfilerSession.StartSession()
//    .SetIterations(10)
//    .Task(c =>
//    {
//        var reader = new YamlReader();
//        reader.Read<TestlItem>(value);
//    })
//    .RunSession()
//    .Trace();