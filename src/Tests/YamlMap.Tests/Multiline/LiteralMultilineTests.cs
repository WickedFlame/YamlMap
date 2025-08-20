
using System;
using System.Text;

namespace YamlMap.Tests.Multiline
{
    public class LiteralMultilineTests
    {
        private YamlReader _reader;

        //
        // https://yaml-multiline.info/

        [SetUp]
        public void Setup()
        {
            _reader = new YamlReader();
        }

        [Test]
        public void LiteralMultiline_SingleNewLineAtEnd()
        {
            var str = new StringBuilder()
                .AppendLine("value: |")
                .AppendLine("  Several lines of text,")
                .AppendLine("  with some \"quotes\" of various 'types',")
                .AppendLine("  and also a blank line")
                .AppendLine("  ")
                .AppendLine("  and some text with")
                .AppendLine("    extra indentation")
                .AppendLine("  on the next line,")
                .AppendLine("  plus another line at the end.")
                .AppendLine("  ").ToString();

            var result = new StringBuilder()
                .AppendLine("Several lines of text,")
                .AppendLine("with some \"quotes\" of various 'types',")
                .AppendLine("and also a blank line")
                .AppendLine()
                .AppendLine("and some text with")
                .AppendLine("  extra indentation")
                .AppendLine("on the next line,")
                .Append("plus another line at the end.").ToString();

            var tmp = _reader.Read<LiteralModel>(str);
            tmp.Value.Should().Be(result);
        }

        [Test]
        [Ignore("not implemented yet")]
        public void LiteralMultiline_NoNewLineAtEnd()
        {
            var str = @"value: |-
  Several lines of text,
  with some ""quotes"" of various 'types',
  and also a blank line
  
  and some text with
    extra indentation
  on the next line,
  plus another line at the end.
  
  ";

            var result = @"Several lines of text,
with some ""quotes"" of various 'types',
and also a blank line
  
and some text with
  extra indentation
on the next line,
plus another line at the end.";
            _reader.Read<LiteralModel>(str).Should().Be(result);
        }

        [Test]
        [Ignore("not implemented yet")]
        public void LiteralMultiline_AllNewLineAtEnd()
        {
            var str = @"value: |+
  Several lines of text,
  with some ""quotes"" of various 'types',
  and also a blank line:test
  
  and some text with
    extra indentation
  on the next line,
  plus another line at the end.
  
  ";

            var result = @"Several lines of text,
with some ""quotes"" of various 'types',
and also a blank line:test
  
and some text with
  extra indentation
on the next line,
plus another line at the end.

";
            _reader.Read<LiteralModel>(str).Should().Be(result);
        }

        public class LiteralModel
        {
            public string Value { get; set; }
        }
    }
}
