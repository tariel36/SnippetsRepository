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

using NUnit.Framework;
using NutaDev.CsLib.Math.Calculator.Evaluation;
using NutaDev.CsLib.Math.Calculator.Tokens;
using NutaDev.CsLib.Internal.Tests.Utility.Comparers;
using System.Collections.Generic;

namespace NutaDev.CsLib.Internal.Tests.NutaDev.CsLib.Math.ParserTests
{
    [TestFixture]
    public class ParserTests
    {
        [Test]
        public void ComplexExpressionToRpn()
        {
            // Arrange
            string input = "2+3*(3-3)+sin(3)-min(4,3)";
            TokenComparer comparer = new TokenComparer();

            Queue<Token> actualRpnQueue;
            Queue<Token> expectedRpnQueue = new Queue<Token>(new[]
            {
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "2"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "3"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "3"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "3"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Minus), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Multiply), "*"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Plus), "+"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "3"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Function), "sin"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Plus), "+"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "4"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "3"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Function), "min"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Minus), "-"),
            });

            ICollection<Token> actualTokenList;
            ICollection<Token> expectedTokenList = new List<Token>()
            {
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "2"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Plus), "+"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "3"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Multiply), "*"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.LeftParenthesis), "("),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "3"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Minus), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "3"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.RightParenthesis), ")"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Plus), "+"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Function), "sin"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.LeftParenthesis), "("),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "3"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.RightParenthesis), ")"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Minus), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Function), "min"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.LeftParenthesis), "("),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "4"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Comma), ","),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "3"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.RightParenthesis), ")"),
            };

            Lexer lexer = new Lexer();
            Parser parser = new Parser();

            // Act
            actualTokenList = lexer.Tokenize(input);
            actualRpnQueue = parser.ToRpn(actualTokenList);

            // Assert
            CollectionAssert.AreEqual(expectedTokenList, actualTokenList, comparer);
            CollectionAssert.AreEqual(expectedRpnQueue, actualRpnQueue, comparer);
        }
    }
}
