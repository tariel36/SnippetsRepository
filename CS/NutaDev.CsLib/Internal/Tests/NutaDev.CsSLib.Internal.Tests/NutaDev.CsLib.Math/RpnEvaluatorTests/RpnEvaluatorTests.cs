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
using NutaDev.CsLib.Math.Calculator.Functions;
using NutaDev.CsLib.Math.Calculator.Tokens;
using NutaDev.CsLib.Random.Dice.Abstract;
using NutaDev.CsLib.Random.Dice.Specific;
using NutaDev.CsLib.Random.Services.Specific;
using NutaDev.CsLib.Internal.Tests.Utility.Comparers;
using NutaDev.CsLib.Internal.Tests.Utility.Mocks;
using System;
using System.Collections.Generic;

namespace NutaDev.CsLib.Internal.Tests.NutaDev.CsLib.Math.RpnEvaluatorTests
{
    [TestFixture]
    public class RpnEvaluatorTests
    {
        [TestCase("1d20", "1")]
        [TestCase("2d20", "3")]
        [TestCase("1d6", "1")]
        public void SimpleDiceRollTest(string input, string expectedResult)
        {
            // Arrange
            string actualResult;

            Lexer lexer = new Lexer();
            Parser parser = new Parser();
            IDiceEvaluator diceEvaluator = new DiceEvaluator(new RandomService(new SequenceRandomProvider(1, 2, 3, 4, 5, 6, 7, 8, 9, 10)));
            RpnEvaluator evaluator = new RpnEvaluator(diceEvaluator);

            Queue<Token> actualRpnQueue;
            ICollection<Token> actualTokenList;

            // Act
            actualTokenList = lexer.Tokenize(input);
            actualRpnQueue = parser.ToRpn(actualTokenList);
            actualResult = evaluator.Evaluate(new Queue<Token>(actualRpnQueue));

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase("1d6+1d6", "3")]
        [TestCase("2*1d6", "2")]
        [TestCase("2*1d6+2", "4")]
        [TestCase("2*1d6-2", "0")]
        [TestCase("2*(2*2d6-2)-2", "6")]
        public void AdvancedDiceRollTest(string input, string expectedResult)
        {
            // Arrange
            string actualResult;

            Lexer lexer = new Lexer();
            Parser parser = new Parser();
            IDiceEvaluator diceEvaluator = new DiceEvaluator(new RandomService(new SequenceRandomProvider(1, 2, 3, 4, 5, 6, 7, 8, 9, 10)));
            RpnEvaluator evaluator = new RpnEvaluator(diceEvaluator);

            Queue<Token> actualRpnQueue;
            ICollection<Token> actualTokenList;

            // Act
            actualTokenList = lexer.Tokenize(input);
            actualRpnQueue = parser.ToRpn(actualTokenList);
            actualResult = evaluator.Evaluate(new Queue<Token>(actualRpnQueue));

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [TestCase("2*min(2*(2*2d6-2)-2+2*(2*2d6-2)-2, 5+2*(2*2d6-2)-2+2*(2*2d6-2)-2)", "56")]
        public void ComplexDiceRollTest(string input, string expectedResult)
        {
            // Arrange
            string actualResult;

            Lexer lexer = new Lexer();
            Parser parser = new Parser();
            IDiceEvaluator diceEvaluator = new DiceEvaluator(new RandomService(new SequenceRandomProvider(1, 2, 3, 4, 5, 6, 7, 8, 9, 10)));
            RpnEvaluator evaluator = new RpnEvaluator(diceEvaluator);

            Queue<Token> actualRpnQueue;
            ICollection<Token> actualTokenList;

            // Act
            evaluator.RegisterFunction(new AnonymousFunctionDefinition("min", new[] { new FunctionArgument("a", typeof(int)), new FunctionArgument("b", typeof(int)) }, args => System.Math.Min(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]))));

            actualTokenList = lexer.Tokenize(input);
            actualRpnQueue = parser.ToRpn(actualTokenList);
            actualResult = evaluator.Evaluate(new Queue<Token>(actualRpnQueue));

            // Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ComplexMathExpressionEvaluate()
        {
            // Arrange
            string input = "2+3*(3-3)+sin(3)-min(4,3)";

            string actualResult;
            string expectedResult = "2";

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
            RpnEvaluator evaluator = new RpnEvaluator();

            // Act

            evaluator.RegisterFunction(new AnonymousFunctionDefinition("sin", new[] { new FunctionArgument("x", typeof(int)) }, args => Convert.ToInt32(args[0])));
            evaluator.RegisterFunction(new AnonymousFunctionDefinition("min", new[] { new FunctionArgument("a", typeof(int)), new FunctionArgument("b", typeof(int)) }, args => System.Math.Min(Convert.ToInt32(args[0]), Convert.ToInt32(args[1]))));

            actualTokenList = lexer.Tokenize(input);
            actualRpnQueue = parser.ToRpn(actualTokenList);
            actualResult = evaluator.Evaluate(new Queue<Token>(actualRpnQueue));

            // Assert
            CollectionAssert.AreEqual(expectedTokenList, actualTokenList, comparer);
            CollectionAssert.AreEqual(expectedRpnQueue, actualRpnQueue, comparer);
            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
