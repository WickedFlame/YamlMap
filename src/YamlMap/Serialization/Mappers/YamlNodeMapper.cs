
namespace YamlMap.Serialization.Mappers
{
    /// <summary>
    /// 
    /// </summary>
    public class YamlNodeMapper : IObjectMapper
    {
        /// <summary>
        /// Map the token to the object
        /// </summary>
        /// <param name="token"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public bool Map(IToken token, object item)
        {
            if (item is not IYamlNode node)
            {
                return false;
            }

            switch (token.TokenType)
            {
                case TokenType.Value when token is ValueToken valueToken:
                {
                    node.SetValue(valueToken.Key, new ValueNode(valueToken.Value));
                    return true;
                }
                
                case TokenType.Object or TokenType.ListItem:
                {
                    var childNode = new YamlNode();

                    foreach (var childToken in token.GetChildTokens())
                    {
                        Map(childToken, childNode);
                    }

                    node.SetValue(token.Key, childNode);
                    return true;
                }
                
                default:
                    return false;
            }
        }
    }
}