using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using WickedFlame.Yaml.Serialization;

namespace WickedFlame.Yaml.Tests.Serialization
{
    [TestFixture]
    public class TokenSerializerTests
    {
        [Test]
        public void TokenSerializer_SerializeList()
        {
            var item = new
            {
                List = new List<string> { "value 1", "value 2", "value 3" }
            };

            var serializer = new TokenSerializer();
            var result = serializer.Serialize(item);

            var sb = new StringBuilder();
            sb.AppendLine("List:");
            sb.AppendLine("  - value 1");
            sb.AppendLine("  - value 2");
            sb.Append("  - value 3");

            Assert.AreEqual(sb.ToString(), result);
        }

        [Test]
        public void TokenSerializer_SerializeDictionary()
        {
            var item = new
            {
                Dict = new Dictionary<string, string> 
                {
                    { "key1","value 1"},
                    { "key2","value 2"},
                    { "key3","value 3"}
                }
            };

            var serializer = new TokenSerializer();
            var result = serializer.Serialize(item);

            var sb = new StringBuilder();
            sb.AppendLine("Dict:");
            sb.AppendLine("  key1: value 1");
            sb.AppendLine("  key2: value 2");
            sb.Append("  key3: value 3");

            Assert.AreEqual(sb.ToString(), result);
        }

        [Test]
        public void TokenSerializer_SerializeObjectList()
        {
            var item = new
            {
                List = new List<object>
                {
                    new {Value = "value 1", Name = "test 1"},
                    new {Value = "value 2", Name = "test 2"}, 
                    new {Value = "value 3", Name = "test 3"}
                }
            };
             
            var serializer = new TokenSerializer();
            var result = serializer.Serialize(item);

            var sb = new StringBuilder();
            sb.AppendLine("List:");
            sb.AppendLine("  - Value: value 1");
            sb.AppendLine("    Name: test 1");
            sb.AppendLine("  - Value: value 2");
            sb.AppendLine("    Name: test 2");
            sb.AppendLine("  - Value: value 3");
            sb.Append("    Name: test 3");

            Assert.AreEqual(sb.ToString(), result);
        }

        [Test]
        public void TokenSerializer_SerializeArray()
        {
            var item = new
            {
                List = new [] { "value 1", "value 2", "value 3" }
            };

            var serializer = new TokenSerializer();
            var result = serializer.Serialize(item);

            var sb = new StringBuilder();
            sb.AppendLine("List:");
            sb.AppendLine("  - value 1");
            sb.AppendLine("  - value 2");
            sb.Append("  - value 3");

            Assert.AreEqual(sb.ToString(), result);
        }

        [Test]
        public void TokenSerializer_SerializeObjectArray()
        {
            var item = new
            {
                List = new []
                {
                    new {Value = "value 1", Name = "test 1"},
                    new {Value = "value 2", Name = "test 2"},
                    new {Value = "value 3", Name = "test 3"}
                }
            };

            var serializer = new TokenSerializer();
            var result = serializer.Serialize(item);

            var sb = new StringBuilder();
            sb.AppendLine("List:");
            sb.AppendLine("  - Value: value 1");
            sb.AppendLine("    Name: test 1");
            sb.AppendLine("  - Value: value 2");
            sb.AppendLine("    Name: test 2");
            sb.AppendLine("  - Value: value 3");
            sb.Append("    Name: test 3");

            Assert.AreEqual(sb.ToString(), result);
        }

        [Test]
        public void TokenSerializer_SerializeObjectDictionary()
        {
            var item = new
            {
                Dict = new Dictionary<string, object>
                {
                    { "key1",new {Value = "value 1", Name = "test 1"}},
                    { "key2",new {Value = "value 2", Name = "test 2"}},
                    { "key3",new {Value = "value 3", Name = "test 3"}}
                }
            };

            var serializer = new TokenSerializer();
            var result = serializer.Serialize(item);

            var sb = new StringBuilder();
            sb.AppendLine("Dict:");
            sb.AppendLine("  key1:");
            sb.AppendLine("    Value: value 1");
            sb.AppendLine("    Name: test 1");
            sb.AppendLine("  key2:");
            sb.AppendLine("    Value: value 2");
            sb.AppendLine("    Name: test 2");
            sb.AppendLine("  key3:");
            sb.AppendLine("    Value: value 3");
            sb.Append("    Name: test 3");

            Assert.AreEqual(sb.ToString(), result);
        }

        [Test]
        public void TokenSerializer_SerializeNestedObjects()
        {
            var item = new
            {
                Root = new
                {
                    Name = "root",
                    One = new
                    {
                        Name = "one",
                        Two = new
                        {
                            Name = "two"
                        }
                    }
                }
            };

            var serializer = new TokenSerializer();
            var result = serializer.Serialize(item);

            var sb = new StringBuilder();
            sb.AppendLine("Root:");
            sb.AppendLine("  Name: root");
            sb.AppendLine("  One:");
            sb.AppendLine("    Name: one");
            sb.AppendLine("    Two:");
            sb.Append("      Name: two");

            Assert.AreEqual(sb.ToString(), result);
        }
    }
}
