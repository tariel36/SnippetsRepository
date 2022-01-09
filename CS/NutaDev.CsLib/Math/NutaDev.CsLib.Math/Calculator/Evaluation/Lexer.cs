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

using NutaDev.CsLib.Maintenance.Exceptions.Factories;
using NutaDev.CsLib.Math.Calculator.Tokens;
using NutaDev.CsLib.Resources.Text.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace NutaDev.CsLib.Math.Calculator.Evaluation
{
    /// <summary>
    /// Simple lexer class.
    /// </summary>
    public class Lexer
    {
        /// <summary>
        /// Collection of <see cref="TokenDefinition"/>.
        /// </summary>
        private static readonly IReadOnlyCollection<TokenDefinition> TokenDefinitions = new ReadOnlyCollection<TokenDefinition>(new List<TokenDefinition>()
        {
            new TokenDefinition(TokenTypes.Function, "^(([a-zA-Z][a-zA-Z]+)|([a-ce-zA-CE-Z]))", 6, TokenAssociativity.LeftToRight, isFunction: true),

            new TokenDefinition(TokenTypes.Number, "^[0-9]+(\\.[0-9]+)?", 0),

            new TokenDefinition(TokenTypes.LeftParenthesis, "^\\(", 12, TokenAssociativity.LeftToRight, isParenthesis: true),
            new TokenDefinition(TokenTypes.RightParenthesis, "^\\)", 12, TokenAssociativity.LeftToRight, isParenthesis: true),

            new TokenDefinition(TokenTypes.Dice, "^d", 10, TokenAssociativity.LeftToRight, true),

            new TokenDefinition(TokenTypes.Multiply, "^\\*", 9, TokenAssociativity.LeftToRight, true),
            new TokenDefinition(TokenTypes.Divide, "^\\/", 9, TokenAssociativity.LeftToRight, true),

            new TokenDefinition(TokenTypes.Plus, "^\\+", 8, TokenAssociativity.LeftToRight, true),
            new TokenDefinitionMinus(TokenTypes.Minus, @"^.((?<=.)(?<![d\(\^\-\+\*\\])(?<grp>\-))", 8, TokenAssociativity.LeftToRight, true),

            new TokenDefinition(TokenTypes.Sign, "^\\-", 11, TokenAssociativity.RightToLeft, true),

            new TokenDefinition(TokenTypes.Comma, "^,", 7,  TokenAssociativity.LeftToRight),

            new TokenDefinition(TokenTypes.Whitespace, "^\\s+", 0)
        });

        /// <summary>
        /// Returns specific <see cref="TokenDefinition"/>.
        /// </summary>
        /// <param name="type">Token definition to get.</param>
        /// <returns>Specific <see cref="TokenDefinition"/> or null.</returns>
        public static TokenDefinition GetDefinitionByType(TokenTypes type)
        {
            return TokenDefinitions.FirstOrDefault(x => x.Type == type);
        }

        /// <summary>
        /// Tokenizes <paramref name="input"/>.
        /// </summary>
        /// <param name="input">Expression to tokenize.</param>
        /// <returns>Collection of tokens.</returns>
        /// <exception cref="InvalidOperationException">Invalid character at specific position.</exception>
        public ICollection<Token> Tokenize(string input)
        {
            string sourceInput = input;

            List<Token> result = new List<Token>();

            int idx = 0;

            while (!string.IsNullOrWhiteSpace(input))
            {
                TokenMatch match = FindMatchingTokenDefinition(sourceInput, idx, input);

                if (match == null || !match.IsMatch)
                {
                    throw ExceptionFactory.Create<InvalidOperationException>(Text.InvalidCharacter_0_At0BasedPosition_1_, sourceInput[idx], idx);
                }

                idx += match.Value.Length;
                input = match.Remaining;

                if (match.Type != TokenTypes.Whitespace)
                {
                    result.Add(new Token(GetDefinitionByType(match.Type), match.Value));
                }
            }

            return result;
        }

        /// <summary>
        /// Finds matching token.
        /// </summary>
        /// <param name="sourceInput">Expression to analyze.</param>
        /// <param name="idx">Current index.</param>
        /// <param name="input">Current expression.</param>
        /// <returns>Match result.</returns>
        private TokenMatch FindMatchingTokenDefinition(string sourceInput, int idx, string input)
        {
            foreach (TokenDefinition definition in TokenDefinitions)
            {
                TokenMatch match = definition.TryMatch(sourceInput, idx, input);

                if (match.IsMatch)
                {
                    return match;
                }
            }

            return new TokenMatch(false, input);
        }
    }
}
