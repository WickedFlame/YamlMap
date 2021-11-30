using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace YamlMap.Tests
{
    [TestFixture]
    public class ArrayParserTests
    {
        [Test]
        public void Parser_BracketArray()
        {
            var lines = new[]
            {
                "Ids: ['1', '2', '3', '4']"
            };
            var scanner = new Scanner(lines);
            var parser = new Parser(scanner);

            var result = parser.Parse();

            Assert.AreEqual("1", ((ValueToken)result["Ids"][0]).Value);
            Assert.AreEqual("2", ((ValueToken)result["Ids"][1]).Value);
            Assert.AreEqual("3", ((ValueToken)result["Ids"][2]).Value);
            Assert.AreEqual("4", ((ValueToken)result["Ids"][3]).Value);
        }

        [Test]
        public void Parser_BracketArray_Multiline()
        {
            var lines = new[]
            {
                "Ids: ['1', '2',",
                "'3', '4']"
            };
            var scanner = new Scanner(lines);
            var parser = new Parser(scanner);

            var result = parser.Parse();

            Assert.AreEqual("1", ((ValueToken)result["Ids"][0]).Value);
            Assert.AreEqual("2", ((ValueToken)result["Ids"][1]).Value);
            Assert.AreEqual("3", ((ValueToken)result["Ids"][2]).Value);
            Assert.AreEqual("4", ((ValueToken)result["Ids"][3]).Value);
        }
    }
}
