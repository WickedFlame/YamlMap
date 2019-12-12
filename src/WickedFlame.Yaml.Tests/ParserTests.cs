using NUnit.Framework;

namespace WickedFlame.Yaml.Tests
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void WickeFlame_Yaml_Parser()
        {
            var lines = new[]
            {
                "Root:",
                "  - L1:",
                "      Name: second",
                "      L2:",
                "        Name: third",
                "    Name: first",
                "  - L1-2:",
                "      Name: 1.2",
                "",
                "Alternating: one",
                "",
                "Test:",
                "  Outcome: Succeeded",
                "  Message: Succeeded message",
                "List:",
                "  - one",
                "  - two",
                "  - three",
                "  - three"
            };
            var scanner = new Scanner(lines);
            var parser = new Parser(scanner);

            var result = parser.Parse();

            Assert.AreEqual("second", ((ValueToken)result["Root"][0]["L1"]["Name"]).Value);
            Assert.AreEqual("third", ((ValueToken)result["Root"][0]["L1"]["L2"]["Name"]).Value);
            Assert.AreEqual("first", ((ValueToken)result["Root"][0]["Name"]).Value);
            Assert.AreEqual("one", ((ValueToken)result["Alternating"]).Value);
            Assert.AreEqual("Succeeded", ((ValueToken)result["Test"]["Outcome"]).Value);
            Assert.AreEqual("Succeeded message", ((ValueToken)result["Test"]["Message"]).Value);
            Assert.AreEqual("one", ((ValueToken)result["List"][0]).Value);
            Assert.AreEqual("two", ((ValueToken)result["List"][1]).Value);
            Assert.AreEqual("three", ((ValueToken)result["List"][2]).Value);
            Assert.AreEqual("three", ((ValueToken)result["List"][3]).Value);
        }

        [Test]
        public void WickeFlame_Yaml_Parser_Simple()
        {
            var lines = new[]
            {
                "Id: 1"
            };
            var scanner = new Scanner(lines);
            var parser = new Parser(scanner);

            var result = parser.Parse();

            Assert.AreEqual("1", ((ValueToken)result["Id"]).Value);
        }

        [Test]
        public void WickeFlame_Yaml_Parser_Nesting()
        {
            var lines = new[]
            {
                "Child:",
                "  Id: 1"
            };
            var scanner = new Scanner(lines);
            var parser = new Parser(scanner);

            var result = parser.Parse();

            Assert.AreEqual("1", ((ValueToken)result["Child"]["Id"]).Value);
        }

        [Test]
        public void WickeFlame_Yaml_Parser_ExtendedNesting()
        {
            var lines = new[]
            {
                "Child:",
                "  Id: 1",
                "  SubChild:",
                "    Id: 2"
            };
            var scanner = new Scanner(lines);
            var parser = new Parser(scanner);

            var result = parser.Parse();

            Assert.AreEqual("1", ((ValueToken)result["Child"]["Id"]).Value);
            Assert.AreEqual("2", ((ValueToken)result["Child"]["SubChild"]["Id"]).Value);
        }

        [Test]
        public void WickeFlame_Yaml_Parser_MultipleExtendedNesting()
        {
            var lines = new[]
            {
                "Child:",
                "  Id: 1",
                "  SubChild:",
                "    Id: 2",
                "  Name: test",
                "Child2:",
                "  Id: 2"
            };
            var scanner = new Scanner(lines);
            var parser = new Parser(scanner);

            var result = parser.Parse();

            Assert.AreEqual("1", ((ValueToken)result["Child"]["Id"]).Value);
            Assert.AreEqual("test", ((ValueToken)result["Child"]["Name"]).Value);
            Assert.AreEqual("2", ((ValueToken)result["Child2"]["Id"]).Value);
        }

        [Test]
        public void WickeFlame_Yaml_Parser_IntIndexer()
        {
            var lines = new[]
            {
                "Child:",
                "  Id: 1",
                "  SubChild:",
                "    Id: 2"
            };
            var scanner = new Scanner(lines);
            var parser = new Parser(scanner);

            var result = parser.Parse();

            Assert.AreEqual("1", ((ValueToken)result[0][0]).Value);
            Assert.AreEqual("2", ((ValueToken)result[0][1][0]).Value);
        }

        [Test]
        public void WickeFlame_Yaml_Parser_SimpleList()
        {
            var lines = new[]
            {
                "Children:",
                "  - 1",
                "  - 2"
            };
            var scanner = new Scanner(lines);
            var parser = new Parser(scanner);

            var result = parser.Parse();

            Assert.AreEqual("1", ((ValueToken)result["Children"][0]).Value);
            Assert.AreEqual("2", ((ValueToken)result["Children"][1]).Value);
        }

        [Test]
        public void WickeFlame_Yaml_Parser_ObjectList()
        {
            var lines = new[]
            {
                "Children:",
                "  - Id: 1",
                "    Name: One",
                "  - Id: 2",
                "    Name: Two"
            };
            var scanner = new Scanner(lines);
            var parser = new Parser(scanner);

            var result = parser.Parse();

            Assert.AreEqual("1", ((ValueToken)result["Children"][0]["Id"]).Value);
            Assert.AreEqual("One", ((ValueToken)result["Children"][0]["Name"]).Value);
            Assert.AreEqual("2", ((ValueToken)result["Children"][1]["Id"]).Value);
            Assert.AreEqual("Two", ((ValueToken)result["Children"][1]["Name"]).Value);
        }

        [Test]
        public void WickeFlame_Yaml_Parser_PropertyList()
        {
            var lines = new[]
            {
                "Children:",
                "  - one: 1",
                "  - two: 2"
            };
            var scanner = new Scanner(lines);
            var parser = new Parser(scanner);

            var result = parser.Parse();

            Assert.AreEqual("1", ((ValueToken)result["Children"][0]["one"]).Value);
            Assert.AreEqual("2", ((ValueToken)result["Children"][1]["two"]).Value);
        }

        [Test]
        public void WickeFlame_Yaml_Parser_SubObjectList()
        {
            var lines = new[]
            {
                "Root:",
                "  - L1:",
                "      Name: first"
            };

            var scanner = new Scanner(lines);
            var parser = new Parser(scanner);

            var result = parser.Parse();

            Assert.AreEqual("first", ((ValueToken)result["Root"][0]["L1"]["Name"]).Value);
        }

        [Test]
        public void WickeFlame_Yaml_Parser_ExtendedObjectList()
        {
            var lines = new[]
            {
                "Root:",
                "  - L1:",
                "      Name: second",
                "      L2:",
                "        Name: third"
            };
            var scanner = new Scanner(lines);
            var parser = new Parser(scanner);

            var result = parser.Parse();

            Assert.AreEqual("second", ((ValueToken)result["Root"][0]["L1"]["Name"]).Value);
            Assert.AreEqual("third", ((ValueToken)result["Root"][0]["L1"]["L2"]["Name"]).Value);
        }

        [Test]
        public void WickeFlame_Yaml_Parser_ExtendedObjectTree()
        {
            var lines = new[]
            {
                "Root:",
                "  - L1:",
                "      Name: second",
                "      L2:",
                "        Name: third",
                "    Name: first"
            };
            var scanner = new Scanner(lines);
            var parser = new Parser(scanner);

            var result = parser.Parse();

            Assert.AreEqual("second", ((ValueToken)result["Root"][0]["L1"]["Name"]).Value);
            Assert.AreEqual("third", ((ValueToken)result["Root"][0]["L1"]["L2"]["Name"]).Value);
            Assert.AreEqual("first", ((ValueToken)result["Root"][0]["Name"]).Value);
        }

        [Test]
        public void WickeFlame_Yaml_Parser_CommentLine()
        {
            var lines = new[]
            {
                "First: one",
                "# comment: fail",
                "#comment 2",
                "Second: two"
            };
            var scanner = new Scanner(lines);
            var parser = new Parser(scanner);

            var result = parser.Parse();

            Assert.AreEqual("one", ((ValueToken)result[0]).Value);
            Assert.AreEqual("two", ((ValueToken)result[1]).Value);
        }

        [Test]
        public void WickeFlame_Yaml_Parser_CommentInLineLine()
        {
            var lines = new[]
            {
                "First: one",
                "Comment: true#fail"
            };
            var scanner = new Scanner(lines);
            var parser = new Parser(scanner);

            var result = parser.Parse();

            Assert.AreEqual("one", ((ValueToken)result[0]).Value);
            Assert.AreEqual("true", ((ValueToken)result[1]).Value);
        }

        [Test]
        public void WickeFlame_Yaml_Parser_InvalidProperty()
        {
            var lines = new[]
            {
                "First:one",
                "Comment: true"
            };
            var scanner = new Scanner(lines);
            var parser = new Parser(scanner);

            var result = parser.Parse();

            Assert.That(result.Count == 1);
            Assert.AreEqual("true", ((ValueToken)result[0]).Value);
        }
    }
}
