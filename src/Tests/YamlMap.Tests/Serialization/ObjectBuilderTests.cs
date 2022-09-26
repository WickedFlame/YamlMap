using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using YamlMap.Serialization;

namespace YamlMap.Tests.Serialization
{
    [TestFixture]
    public class ObjectBuilderTests
    {
        [TestCase(typeof(IEnumerable<string>), typeof(List<string>))]
        [TestCase(typeof(IEnumerable<TestObject>), typeof(List<TestObject>))]
        [TestCase(typeof(IList<string>), typeof(List<string>))]
        [TestCase(typeof(IList<TestObject>), typeof(List<TestObject>))]
        [TestCase(typeof(ICollection<string>), typeof(List<string>))]
        [TestCase(typeof(ICollection<TestObject>), typeof(List<TestObject>))]
        [TestCase(typeof(IDictionary<string, string>), typeof(Dictionary<string, string>))]
        [TestCase(typeof(IDictionary<string, TestObject>), typeof(Dictionary<string, TestObject>))]
        public void ObjectBuilder_GenericLists(Type input, Type expected)
        {
            var token = new Token("test", 0);
            var obj = input.CreateInstance(token);

            Assert.IsInstanceOf(expected, obj);
        }

        [TestCase(typeof(IEnumerable), typeof(List<object>))]
        [TestCase(typeof(IList), typeof(List<object>))]
        [TestCase(typeof(ICollection), typeof(List<object>))]
        [TestCase(typeof(IDictionary), typeof(Dictionary<object, object>))]
        public void ObjectBuilder_NonGenericLists(Type input, Type expected)
        {
            var token = new Token("test", 0);
            var obj = input.CreateInstance(token);

            Assert.IsInstanceOf(expected, obj);
        }

        [TestCase(typeof(TestObject), typeof(TestObject))]
        public void ObjectBuilder_BasicObject(Type input, Type expected)
        {
            var token = new Token("test", 0);
            var obj = input.CreateInstance(token);

            Assert.IsInstanceOf(expected, obj);
        }

        [Test]
        public void ObjectBuilder_Array()
        {
            var type = typeof(string[]);
            var token = new Token("test", 0);
            token.Set(new Token("val1", 1));
            token.Set(new Token("va21", 1));
            token.Set(new Token("val3", 1));

            var obj = type.CreateInstance(token);

            Assert.IsInstanceOf<IList>(obj);
            Assert.AreEqual(3, ((IList) obj).Count);
        }

        public class TestObject { }
    }
}
