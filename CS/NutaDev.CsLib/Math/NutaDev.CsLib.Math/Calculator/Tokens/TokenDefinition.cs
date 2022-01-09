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
    /// Token definition class.
    /// </summary>
    public class TokenDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenDefinition"/> class.
        /// </summary>
        /// <param name="type">Token type.</param>
        /// <param name="regex">Token regex.</param>
        /// <param name="priority">Token priority.</param>
        /// <param name="associativity">Token associativity.</param>
        /// <param name="isOperator">Determines whether token is an operator.</param>
        /// <param name="isFunction">Determines whether token is a function.</param>
        /// <param name="isParenthesis">Determines whether token is a parenthesis.</param>
        public TokenDefinition(TokenTypes type, string regex, int priority, TokenAssociativity associativity = TokenAssociativity.None, bool isOperator = false, bool isFunction = false, bool isParenthesis = false)
            : this(type, new Regex(regex, RegexOptions.Compiled), priority, associativity, isOperator, isFunction, isParenthesis)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TokenDefinition"/> class.
        /// </summary>
        /// <param name="type">Token type.</param>
        /// <param name="regex">Token regex.</param>
        /// <param name="priority">Token priority.</param>
        /// <param name="associativity">Token associativity.</param>
        /// <param name="isOperator">Determines whether token is an operator.</param>
        /// <param name="isFunction">Determines whether token is a function.</param>
        /// <param name="isParenthesis">Determines whether token is a parenthesis.</param>
        public TokenDefinition(TokenTypes type, Regex regex, int priority, TokenAssociativity associativity = TokenAssociativity.None, bool isOperator = false, bool isFunction = false, bool isParenthesis = false)
        {
            Regex = regex;
            Type = type;

            Priority = priority;
            Associativity = associativity;

            IsOperator = isOperator;
            IsFunction = isFunction;
            IsParenthesis = isParenthesis;
        }

        /// <summary>
        /// Gets token type.
        /// </summary>
        public TokenTypes Type { get; }

        /// <summary>
        /// Gets token associativity.
        /// </summary>
        public TokenAssociativity Associativity { get; }

        /// <summary>
        /// Gets token priority.
        /// </summary>
        public int Priority { get; }

        /// <summary>
        /// Gets token regular expression.
        /// </summary>
        public Regex Regex { get; }

        /// <summary>
        /// Indicates whether token is an operator.
        /// </summary>
        public bool IsOperator { get; }

        /// <summary>
        /// Indicates whether token is a function.
        /// </summary>
        public bool IsFunction { get; }

        /// <summary>
        /// Indicates whether token is a parenthesis.
        /// </summary>
        public bool IsParenthesis { get; }

        /// <summary>
        /// Checks if input matches <see cref="Regex"/>.
        /// </summary>
        /// <param name="input">Input to check.</param>
        /// <returns><see cref="TokenMatch"/> object with result.</returns>
        public TokenMatch TryMatch(string input)
        {
            Match match = Regex.Match(input);

            return match.Success
                ? new TokenMatch(true, Type, match.Value, input.Length == match.Value.Length ? string.Empty : input.Substring(match.Value.Length))
                : new TokenMatch(false, input);
        }

        /// <summary>
        /// Checks if input matches <see cref="Regex"/>.
        /// </summary>
        /// <param name="sourceInput">Expression to analyze.</param>
        /// <param name="idx">Current index.</param>
        /// <param name="input">Current expression.</param>
        /// <returns><see cref="TokenMatch"/> object with result.</returns>
        public virtual TokenMatch TryMatch(string sourceInput, int idx, string input)
        {
            return TryMatch(input);
        }
    }
}
