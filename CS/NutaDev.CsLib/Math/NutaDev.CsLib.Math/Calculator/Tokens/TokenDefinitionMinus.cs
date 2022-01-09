// The MIT License (MIT)
//
// Copyright (c) 2022 tariel36
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.

using System.Text.RegularExpressions;

namespace NutaDev.CsLib.Math.Calculator.Tokens
{
    /// <summary>
    /// Token definition class for unary minus.
    /// </summary>
    public class TokenDefinitionMinus
        : TokenDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenDefinitionMinus"/> class.
        /// </summary>
        /// <param name="type">Token type.</param>
        /// <param name="regex">Token regex.</param>
        /// <param name="priority">Token priority.</param>
        /// <param name="associativity">Token associativity.</param>
        /// <param name="isOperator">Determines whether token is an operator.</param>
        /// <param name="isFunction">Determines whether token is a function.</param>
        /// <param name="isParenthesis">Determines whether token is a parenthesis.</param>
        public TokenDefinitionMinus(TokenTypes type, string regex, int priority, TokenAssociativity associativity = TokenAssociativity.None, bool isOperator = false, bool isFunction = false, bool isParenthesis = false) : base(type, regex, priority, associativity, isOperator, isFunction, isParenthesis)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenDefinitionMinus"/> class.
        /// </summary>
        /// <param name="type">Token type.</param>
        /// <param name="regex">Token regex.</param>
        /// <param name="priority">Token priority.</param>
        /// <param name="associativity">Token associativity.</param>
        /// <param name="isOperator">Determines whether token is an operator.</param>
        /// <param name="isFunction">Determines whether token is a function.</param>
        /// <param name="isParenthesis">Determines whether token is a parenthesis.</param>
        public TokenDefinitionMinus(TokenTypes type, Regex regex, int priority, TokenAssociativity associativity = TokenAssociativity.None, bool isOperator = false, bool isFunction = false, bool isParenthesis = false) : base(type, regex, priority, associativity, isOperator, isFunction, isParenthesis)
        {
        }

        /// <summary>
        /// Checks if input matches <see cref="Regex"/>.
        /// </summary>
        /// <param name="sourceInput">Expression to analyze.</param>
        /// <param name="idx">Current index.</param>
        /// <param name="input">Current expression.</param>
        /// <returns><see cref="TokenMatch"/> object with result.</returns>
        public override TokenMatch TryMatch(string sourceInput, int idx, string input)
        {
            Match match = Regex.Match(input);

            string value = null;

            if (!match.Success)
            {
                if (idx - 1 >= 0)
                {
                    string tmpInput = sourceInput.Substring(idx - 1);
                    match = Regex.Match(tmpInput);

                    if (match.Success)
                    {
                        value = match.Groups["grp"].Value;
                    }
                }
            }
            else
            {
                value = match.Value;
            }

            return match.Success
                ? new TokenMatch(true, Type, value, value == null || input.Length == value.Length ? string.Empty : input.Substring(value.Length))
                : new TokenMatch(false, input);
        }
    }
}
