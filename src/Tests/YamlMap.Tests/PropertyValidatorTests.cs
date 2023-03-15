using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace YamlMap.Tests
{
    public class PropertyValidatorTests
    {
        [Test]
        public void PropertyValidator_Assert()
        {
            var property = typeof(PropertyModel).GetProperties().First();
            Action act = () => PropertyValidator.Assert(property, new ValueToken("Name", "value", 0), typeof(string));
            act.Should().NotThrow();
        }

        [Test]
        public void PropertyValidator_Assert_ValueToken()
        {
            Action act = () => PropertyValidator.Assert(null, new ValueToken("Name", "value", 0), typeof(PropertyModel));
            act.Should().Throw<InvalidConfigurationException>();
        }

        [Test]
        public void PropertyValidator_Assert_ValueToken_Message()
        {
            try
            {
                PropertyValidator.Assert(null, new ValueToken("Name", "value", 0), typeof(string));
            }
            catch (InvalidConfigurationException e)
            {
                e.Message.Should().Be("The configured Node 'Name: value' could not be mapped to Type 'System.String'. This is an indication that the configured Node does not match the expected Type on the Object");
            }
        }

        [Test]
        public void PropertyValidator_Assert_ValueToken_Parent_Message()
        {
            var token = new ValueToken(null, "value", 2);
            var parent = new Token("parent", 0);
            parent.Set(token);

            try
            {
                PropertyValidator.Assert(null, token, typeof(PropertyModel));
            }
            catch (InvalidConfigurationException e)
            {
                e.Message.Should().Be("The configured Node 'parent: value' could not be mapped to Type 'YamlMap.Tests.PropertyValidatorTests+PropertyModel'. This is an indication that the configured Node does not match the expected Type on the Object");
            }
        }

        [Test]
        public void PropertyValidator_Assert_ListItem()
        {
            Action act = () => PropertyValidator.Assert(null, new Token("list", 2, TokenType.ListItem), typeof(PropertyModel));
            act.Should().Throw<InvalidConfigurationException>();
        }

        [Test]
        public void PropertyValidator_Assert_ListItem_Message()
        {
            try
            {
                PropertyValidator.Assert(null, new Token("list", 2, TokenType.ListItem), typeof(List<string>));
            }
            catch (InvalidConfigurationException e)
            {
                e.Message.Should().StartWith("The configured ListItem could not be mapped to the Property 'list'. Expected Type for Property 'list' is 'System.Collections.Generic.List`1[[System.String");
            }
        }

        [Test]
        public void PropertyValidator_Assert_ListItem_Parent_Message()
        {
            var token = new Token(null, 2, TokenType.ListItem);
            var parent = new Token("parent", 0);
            parent.Set(token);

            try
            {
                PropertyValidator.Assert(null, token, typeof(PropertyModel));
            }
            catch (InvalidConfigurationException e)
            {
                e.Message.Should().Be("The configured ListItem could not be mapped to the Property 'parent'. Expected Type for Property 'parent' is 'YamlMap.Tests.PropertyValidatorTests+PropertyModel'");
            }
        }

        [Test]
        public void PropertyValidator_Assert_Object()
        {
            Action act = () => PropertyValidator.Assert(null, new Token("list", 2), typeof(PropertyModel));
            act.Should().Throw<InvalidConfigurationException>();
        }

        [Test]
        public void PropertyValidator_Assert_Object_Message()
        {
            try
            {
                PropertyValidator.Assert(null, new Token("list", 2), typeof(string));
            }
            catch (InvalidConfigurationException e)
            {
                e.Message.Should().StartWith("The configured Node 'list' could not be mapped to Type 'System.String'. No Property found for 'list'");
            }
        }

        [Test]
        public void PropertyValidator_Assert_Object_Parent_Message()
        {
            var token = new Token(null, 2);
            var parent = new Token("parent", 0);
            parent.Set(token);

            try
            {
                PropertyValidator.Assert(null, token, typeof(PropertyModel));
            }
            catch (InvalidConfigurationException e)
            {
                e.Message.Should().Be("The configured Node 'parent' could not be mapped to Type 'YamlMap.Tests.PropertyValidatorTests+PropertyModel'. No Property found for 'parent'");
            }
        }

        [Test]
        public void PropertyValidator_Assert_List_InvalidConfig()
        {
            // simple valuetoken in list item is configured without spaces before -
            //

            var token = new ValueToken(null, "value", 0);
            var list = new Token(null, 0, TokenType.ListItem);
            list.Set(token);
            var parent = new Token("parent", 0);
            parent.Set(list);

            try
            {
                PropertyValidator.Assert(null, token, typeof(PropertyModel));
            }
            catch (InvalidConfigurationException e)
            {
                e.Message.Should().Be("The configured ListItem 'value' could not be mapped to Type 'YamlMap.Tests.PropertyValidatorTests+PropertyModel'. This is an indication that the configured Node does not match the expected Type on the Object");
            }
        }


        [Test]
        public void PropertyValidator_Assert_List_InvalidConfig_2()
        {
            // simple valuetoken in list item is configured without spaces before -
            //

            var list = new Token(null, 0, TokenType.ListItem);
            var parent = new Token(null, 0, TokenType.ListItem);
            parent.Set(list);

            try
            {
                PropertyValidator.Assert(null, list, typeof(PropertyModel));
            }
            catch (InvalidConfigurationException e)
            {
                e.Message.Should().Be("The configured ListItem could not be mapped to Type 'YamlMap.Tests.PropertyValidatorTests+PropertyModel'. The Type 'YamlMap.Tests.PropertyValidatorTests+PropertyModel' is not a List.");
            }
        }


        private class PropertyModel
        {
            public string Name { get; set; }
        }
    }
}
