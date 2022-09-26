using System;
using System.Collections.Generic;
using System.Linq;

namespace YamlMap
{
    /// <summary>
    /// Extensions for <see cref="IToken"/>
    /// </summary>
    public static class TokenExtensions
    {
        /// <summary>
        /// Gets all the childtokens from th <see cref="IToken"/>
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static IToken[] GetChildTokens(this IToken token)
        {
            if (token.Count == 0)
            {
                return Array.Empty<IToken>();
            }

            var tokens = new IToken[token.Count];

            for (var i = 0; i < token.Count; i++)
            {
                tokens[i] = token[i];
            }

            return tokens;
        }
    }
}
