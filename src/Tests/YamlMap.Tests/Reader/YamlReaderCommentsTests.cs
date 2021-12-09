using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Polaroider;

namespace YamlMap.Tests.Reader
{
    public class YamlReaderCommentsTests
    {
        [Test]
        public void YamlReader_Comments_InList()
        {
            var lines = new[]
            {
                "Items:",
                "#  - first",
                "  - second",
                " # - third",
                "  - fourth",
                "Id: test"
            };

            var reader = new YamlReader();
            reader.Read<CommentsList>(lines).MatchSnapshot();
        }

        public class CommentsList
        {
            public  List<string> Items { get; set; }

            public string Id{ get; set; }
        }
    }
}
