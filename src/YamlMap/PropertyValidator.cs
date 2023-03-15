using System;
using System.Reflection;
using System.Text;

namespace YamlMap
{
    /// <summary>
    /// Validate properties. Throws a <see cref="InvalidConfigurationException"/> if the property is null
    /// </summary>
    public class PropertyValidator
    {
        /// <summary>
        /// 
        /// </summary>
        protected PropertyValidator() { }

        /// <summary>
        /// Validate the property. Throws a <see cref="InvalidConfigurationException"/> if the property is null
        /// </summary>
        /// <param name="property"></param>
        /// <param name="token"></param>
        /// <param name="type"></param>
        public static void Assert(PropertyInfo property, IToken token, Type type)
        {
            if (property == null)
            {
                Throw(token, type);
            }
        }

        private static void Throw(IToken token, Type type)
        {
            var property = token.Key ?? token.Parent?.Key;
            var msg = new StringBuilder();

            switch (token.TokenType)
            {
                case TokenType.ListItem:
                {
                    if (property == null)
                    {
                        msg.Append($"The configured ListItem could not be mapped to Type '{type.FullName}'. The Type '{type.FullName}' is not a List.");
                    }
                    else
                    {
                        msg.Append($"The configured ListItem could not be mapped to the Property '{property}'. Expected Type for Property '{property}' is '{type.FullName}'");
                    }

                }
                    break;

                case TokenType.Value:
                {
                    var value = (token as ValueToken)?.Value ?? "null";
                    var node = string.IsNullOrEmpty(property) ? $"The configured ListItem '{value}'" : $"The configured Node '{property}: {value}'";
                    msg.Append($"{node} could not be mapped to Type '{type.FullName}'. This is an indication that the configured Node does not match the expected Type on the Object");
                }
                    break;

                case TokenType.Object:
                default:
                {
                    msg.Append($"The configured Node '{property}' could not be mapped to Type '{type.FullName}'. No Property found for '{property}'");
                }
                    break;
            }

            throw new InvalidConfigurationException(msg.ToString());
        }
    }
}
