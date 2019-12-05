using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;
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
                "Child:",
                "  Id: 1"
            };
            var scanner = new Scanner(lines);
            var parser = new Parser(scanner);

            var result = parser.Parse();
            
            Assert.AreEqual("1", ((ValueToken)result["Child"]["Id"]).Value);






            //var json = "";

            //JObject rss = JObject.Parse(json);

            //string rssTitle = (string)rss["channel"]["title"];
            //// James Newton-King

            //int itemTitle = (int)rss["channel"]["item"][0]["title"];
            //// Json.NET 1.3 + New license + Now on CodePlex

            //JArray categories = (JArray)rss["channel"]["item"][0]["categories"];
            //// ["Json.NET", "CodePlex"]

            //IList<string> categoriesText = categories.Select(c => (string)c).ToList();
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
    }
}
