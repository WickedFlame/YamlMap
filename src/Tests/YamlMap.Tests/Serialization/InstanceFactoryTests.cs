using System;
using YamlMap.Serialization;

namespace YamlMap.Tests.Serialization
{
    public class InstanceFactoryTests
    {
        [Test]
        public void InstanceFactory_Create_Simple()
        {
            var token = new Token("", 0);
            token.Set(new ValueToken("Id", "1", 0));
            token.Set(new ValueToken("Value", "passed", 0));

            var factory = new InstanceFactory();
            var obj = factory.CreateInstance(typeof(NoCtor), token);

            obj.Invoke().Should().NotBeNull();
        }

        [Test]
        public void InstanceFactory_Create_ConstructorParameters()
        {
            var token = new Token("", 0);
            token.Set(new ValueToken("Id", "1", 0));
            token.Set(new ValueToken("Value", "passed", 0));

            var factory = new InstanceFactory();
            var obj = factory.CreateInstance(typeof(WithCtor), token);

            obj.Invoke().Should().NotBeNull();
        }

        [Test]
        public void InstanceFactory_Create_ConstructorParameters_DefaultFactory()
        {
            var token = new Token("", 0);
            token.Set(new ValueToken("Id", "1", 0));
            token.Set(new ValueToken("Value", "passed", 0));

            InstanceFactory.Factory.CreateInstance(typeof(WithCtor), token).Invoke().Should().NotBeNull();
        }

        [Test]
        public void InstanceFactory_Create_InvalidParameters()
        {
            var token = new Token("", 0);
            token.Set(new ValueToken("Id", "fail", 0));
            token.Set(new ValueToken("Value", "passed", 0));

            var factory = new InstanceFactory();
            var obj = factory.CreateInstance(typeof(WithCtor), token);

            obj.Should().Throw<MissingMethodException>();
        }

        [Test]
        public void InstanceFactory_Create_PrimitiveType()
        {
            var token = new Token("", 0);
            token.Set(new ValueToken("Value", "1", 0));

            InstanceFactory.Factory.CreateInstance(typeof(int), token).Invoke().Should().Be(0);
        }
    }
}
