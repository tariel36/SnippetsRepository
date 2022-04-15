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
using NutaDev.CsLib.Resources.Text.Exceptions;
using NutaDev.CsLib.Internal.Tests.Utility.Comparers;
using System;
using System.Collections.Generic;

namespace NutaDev.CsLib.Internal.Tests.NutaDev.CsLib.Math.LexerTests
{
    [TestFixture]
    public class LexerTests
    {
        [TestCase("&+2", "&", 0)]
        [TestCase("  &  +  2   ", "&", 2)]
        public void InvalidCharacterException(string input, string character, int position)
        {
            // Arrange
            InvalidOperationException expectedException;
            string expectedErrorMessage;

            Lexer lexer = new Lexer();

            // Act
            expectedErrorMessage = string.Format(Text.InvalidCharacter_0_At0BasedPosition_1_, character, position);
            expectedException = Assert.Throws<InvalidOperationException>(() => lexer.Tokenize(input));

            // Assert
            Assert.That(expectedException.Message, Is.EqualTo(expectedErrorMessage));
        }

        [TestCase("2+2")]
        [TestCase("   2 +  2  ")]
        [TestCase("2+    2")]
        [TestCase("     2+    2")]
        [TestCase("        2+2")]
        [TestCase("        2        +2")]
        public void MathExpressionPlus2Operands(string input)
        {
            // Arrange
            TokenComparer comparer = new TokenComparer();
            ICollection<Token> actual;
            List<Token> expected = new List<Token>()
            {
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "2"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Plus), "+"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "2"),
            };

            Lexer lexer = new Lexer();

            // Act
            actual = lexer.Tokenize(input);

            // Assert
            CollectionAssert.AreEqual(expected, actual, comparer);
        }

        [Test]
        public void AdvancedExpresionNestedFunctions()
        {
            // Arrange
            string input = "bar(foo((2+2)--2+(-2))d(--2--------2), foo((2+2)--2+(-2))d(--2--------2))";
            TokenComparer comparer = new TokenComparer();
            ICollection<Token> actual;
            List<Token> expected = new List<Token>()
            {
                new Token(Lexer.GetDefinitionByType(TokenTypes.Function), "bar"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.LeftParenthesis), "("),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Function), "foo"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.LeftParenthesis), "("),
                new Token(Lexer.GetDefinitionByType(TokenTypes.LeftParenthesis), "("),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "2"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Plus), "+"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "2"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.RightParenthesis), ")"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Minus), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Sign), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "2"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Plus), "+"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.LeftParenthesis), "("),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Sign), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "2"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.RightParenthesis), ")"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.RightParenthesis), ")"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Dice), "d"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.LeftParenthesis), "("),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Sign), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Sign), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "2"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Minus), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Sign), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Sign), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Sign), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Sign), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Sign), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Sign), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Sign), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "2"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.RightParenthesis), ")"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Comma), ","),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Function), "foo"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.LeftParenthesis), "("),
                new Token(Lexer.GetDefinitionByType(TokenTypes.LeftParenthesis), "("),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "2"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Plus), "+"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "2"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.RightParenthesis), ")"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Minus), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Sign), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "2"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Plus), "+"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.LeftParenthesis), "("),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Sign), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "2"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.RightParenthesis), ")"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.RightParenthesis), ")"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Dice), "d"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.LeftParenthesis), "("),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Sign), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Sign), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "2"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Minus), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Sign), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Sign), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Sign), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Sign), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Sign), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Sign), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Sign), "-"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.Number), "2"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.RightParenthesis), ")"),
                new Token(Lexer.GetDefinitionByType(TokenTypes.RightParenthesis), ")"),
            };

            Lexer lexer = new Lexer();

            // Act
            actual = lexer.Tokenize(input);

            // Assert
            CollectionAssert.AreEqual(expected, actual, comparer);
        }
    }
}
