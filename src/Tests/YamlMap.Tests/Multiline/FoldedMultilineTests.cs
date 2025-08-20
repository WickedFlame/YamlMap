
using System;
using System.Text;

namespace YamlMap.Tests.Multiline
{
    public class FoldedMultilineTests
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
        public void FoldedMultiline()
        {
            var str = new StringBuilder()
                .AppendLine("value: >")
                .AppendLine("  line 1")
                .AppendLine("  line 2")
                .AppendLine("  ")
                .AppendLine("     line 3")
                .AppendLine("  ").ToString();

            _reader.Read<FoldedModel>(str).Value.Should().Be(@"line 1 line 2     line 3");
        }
        
        [Test]
        [Ignore("Not implemented")]
        public void FoldedMultiline_SingleNewLineAtEnd()
        {
            var str = @"value: >
  Several lines of text,
  with some ""quotes"" of various 'types',
  and also a blank line:
  
  and some text with
    extra indentation
  on the next line,
  plus another line at the end.
  
  ";

            var result = @"Several lines of text, with some ""quotes"" of various 'types', and also a blank line:
and some text with
  extra indentation
on the next line, plus another line at the end.
";
            _reader.Read<FoldedModel>(str).Should().Be(result);
        }

        [Test]
        [Ignore("Not implemented")]
        public void FoldedMultiline_NoNewLineAtEnd()
        {
            var str = @"value: >-
  Several lines of text,
  with some ""quotes"" of various 'types',
  and also a blank line:
  
  and some text with
    extra indentation
  on the next line,
  plus another line at the end.
  
  ";

            var result = @"Several lines of text, with some ""quotes"" of various 'types', and also a blank line:
and some text with
  extra indentation
on the next line, plus another line at the end.";
            _reader.Read<FoldedModel>(str).Should().Be(result);
        }

        [Test]
        [Ignore("Not implemented")]
        public void FoldedMultiline_AllNewLineAtEnd()
        {
            var str = @"value: >+
  Several lines of text,
  with some ""quotes"" of various 'types',
  and also a blank line:
  
  and some text with
    extra indentation
  on the next line,
  plus another line at the end.
  
  ";

            var result = @"Several lines of text, with some ""quotes"" of various 'types', and also a blank line:
and some text with
  extra indentation
on the next line, plus another line at the end.

";
            _reader.Read<FoldedModel>(str).Should().Be(result);
        }

        public class FoldedModel
        {
            public string Value { get; set; }
        }
    }
}
