using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentAssertions;
using NUnit.Framework;

namespace YamlMap.Tests.Reader
{
    [TestFixture]
    public class YamlReaderStringTests
    {
        //
        // https://yaml-multiline.info/
        // Block Scalar Style: |
        //   Keep newlines
        // Block Scalar Style: >
        //   Replace newlines with space
        
        [Test]
        public void YamlMap_YamlReader_String_BlockScalar_Literal()
        {
            var lines = new[]
            {
                "Value: |",
                "  some",
                "    multiline ",
                "  value",
                "Other: single line"
            };

            var reader = new YamlReader();
            var data = reader.Read<StringNode>(lines);

            data.Value.Should().Be($"some{Environment.NewLine}  multiline {Environment.NewLine}value");
        }
        
        [Test]
        public void YamlMap_YamlReader_String_BlockScalar_Literal_FurtherProperties()
        {
            var lines = new[]
            {
                "Value: |",
                "  some",
                "    multiline ",
                "  value",
                "Other: single line"
            };

            var reader = new YamlReader();
            var data = reader.Read<StringNode>(lines);

            data.Value.Should().NotBeEmpty();
            data.Other.Should().Be("single line");
        }
        
        [Test]
        public void YamlMap_YamlReader_String_BlockScalar_Literal_BlankLine()
        {
            var lines = new[]
            {
                "Value: |",
                "  some",
                "",
                "    multiline ",
                "  value",
                "Other: single line"
            };

            var reader = new YamlReader();
            var data = reader.Read<StringNode>(lines);

            data.Value.Should().Be($"some{Environment.NewLine}{Environment.NewLine}  multiline {Environment.NewLine}value");
        }


        public class StringNode
        {
            public string Value { get; set; }
            
            public string Other { get; set; }
        }
    }
}
