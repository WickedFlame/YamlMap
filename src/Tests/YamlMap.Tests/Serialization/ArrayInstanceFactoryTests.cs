using YamlMap.Serialization;

namespace YamlMap.Tests.Serialization
{
    public class ArrayInstanceFactoryTests
    {
        [Test]
        public void ArrayInstanceFactory_Type()
        {
            var token = new Token("", 0);
            token.Set(new ValueToken("Value", "1", 0));

            ArrayInstanceFactory.Factory.CreateInstance(typeof(int[]), token).Invoke().Should().BeAssignableTo<int[][]>();
        }

        [Test]
        public void ArrayInstanceFactory_Length()
        {
            var token = new Token("", 0);
            token.Set(new ValueToken("Value1", "1", 0));
            token.Set(new ValueToken("Value2", "2", 0));

            ((int[][])ArrayInstanceFactory.Factory.CreateInstance(typeof(int[]), token).Invoke()).Should().HaveCount(2);
        }
    }
}
