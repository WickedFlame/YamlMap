using FluentAssertions;
using NUnit.Framework;

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
            var str = @"value: |
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
            _reader.Read<LiteralModel>(str).Value.Should().Be(result);
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
