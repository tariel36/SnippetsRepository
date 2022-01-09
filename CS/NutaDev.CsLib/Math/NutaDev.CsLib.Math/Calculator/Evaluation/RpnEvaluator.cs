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
using NutaDev.CsLib.Math.Calculator.Functions;
using NutaDev.CsLib.Math.Calculator.Tokens;
using NutaDev.CsLib.Random.Dice.Abstract;
using NutaDev.CsLib.Resources.Text.Exceptions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;

namespace NutaDev.CsLib.Math.Calculator.Evaluation
{
    /// <summary>
    /// Simple RPN evaluator.
    /// </summary>
    public class RpnEvaluator
    {
        /// <summary>
        /// Collection of <see cref="TokenTypes"/> that represent operators.
        /// </summary>
        private static readonly IReadOnlyCollection<TokenTypes> OperatorTokenTypes = new ReadOnlyCollection<TokenTypes>(new[] { TokenTypes.Plus, TokenTypes.Minus, TokenTypes.Multiply, TokenTypes.Divide });

        /// <summary>
        /// Initializes a new instance of the <see cref="RpnEvaluator"/> class.
        /// </summary>
        public RpnEvaluator()
            : this(null)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RpnEvaluator"/> class.
        /// </summary>
        /// <param name="diceEvaluator">Object that evaluates dice rolls.</param>
        public RpnEvaluator(IDiceEvaluator diceEvaluator)
        {
            Functions = new Dictionary<string, Dictionary<string, FunctionDefinition>>();
            Culture = new CultureInfo("en-US");

            DiceEvaluator = diceEvaluator;
        }

        /// <summary>
        /// Gets culture.
        /// </summary>
        public CultureInfo Culture { get; }

        /// <summary>
        /// Gets or sets last error.
        /// </summary>
        public Exception Error { get; private set; }

        /// <summary>
        /// Gets dice evaluation object.
        /// </summary>
        private IDiceEvaluator DiceEvaluator { get; }

        /// <summary>
        /// Gets collection of registered functions.
        /// </summary>
        private Dictionary<string, Dictionary<string, FunctionDefinition>> Functions { get; }

        /// <summary>
        /// Registers <see cref="FunctionDefinition"/>.
        /// </summary>
        /// <param name="funcDef">Definition to register.</param>
        /// <exception cref="InvalidOperationException">Duplicate function declaration.</exception>
        public void RegisterFunction(FunctionDefinition funcDef)
        {
            if (!Functions.ContainsKey(funcDef.Name))
            {
                Functions[funcDef.Name] = new Dictionary<string, FunctionDefinition>();
            }

            string key = CreateFunctionsKey(funcDef);

            if (Functions[funcDef.Name].ContainsKey(key))
            {
                throw ExceptionFactory.Create<InvalidOperationException>(Text.DuplicateFunctionDeclaration, funcDef.Name, key);
            }

            Functions[funcDef.Name][key] = funcDef;
        }

        /// <summary>
        /// Evaluates <see cref="Token"/> RPN queue.
        /// </summary>
        /// <param name="tokensQueue">RPN queue with tokens.</param>
        /// <returns>Expression result.</returns>
        /// <exception cref="InvalidOperationException">Missing right operand, missing left operand, unknown function or invalid expression.</exception>
        public string Evaluate(Queue<Token> tokensQueue)
        {
            Stack<Token> tokensStack = new Stack<Token>();

            while (tokensQueue.Count > 0)
            {
                Token token = tokensQueue.Dequeue();

                if (token.IsOperator)
                {
                    Token resultToken;

                    if (tokensStack.Count == 0)
                    {
                        throw ExceptionFactory.Create<InvalidOperationException>(Text.MissingRightOperand);
                    }

                    Token rightOperand = tokensStack.Pop();

                    if (token.Type == TokenTypes.Sign)
                    {
                        resultToken = EvaluateOperator(token, null, rightOperand);
                    }
                    else
                    {
                        if (tokensStack.Count == 0)
                        {
                            throw ExceptionFactory.Create<InvalidOperationException>(Text.MissingLeftOperand);
                        }

                        Token leftOperand = tokensStack.Pop();

                        resultToken = EvaluateOperator(token, leftOperand, rightOperand);
                    }

                    tokensStack.Push(resultToken);
                }
                else if (token.IsFunction)
                {
                    if (Functions.ContainsKey(token.Value))
                    {
                        FunctionDefinition functionDef = Functions[token.Value].First().Value;
                        Stack<Token> args = new Stack<Token>(Enumerable.Range(0, functionDef.ArgumentCount).Select(x => tokensStack.Pop()));

                        tokensStack.Push(new Token(Lexer.GetDefinitionByType(TokenTypes.Number), functionDef.Evaluate(args).ToString()));
                    }
                    else
                    {
                        throw ExceptionFactory.Create<InvalidOperationException>(Text.UnknownFunction, token.Value);
                    }
                }
                else if (token.Type == TokenTypes.Number)
                {
                    tokensStack.Push(token);
                }
            }

            if (tokensStack.Count != 1)
            {
                throw ExceptionFactory.Create<InvalidOperationException>(Text.FailedToEvaluateExpression);
            }

            return tokensStack.Pop().Value;
        }

        /// <summary>
        /// Evaluates operator.
        /// </summary>
        /// <param name="token">Operator token.</param>
        /// <param name="leftOperand">The left operand.</param>
        /// <param name="rightOperand">The right operand.</param>
        /// <returns><see cref="Token"/> that's a result of the operator.</returns>
        /// <exception cref="InvalidOperationException">Invalid operator or missing dice evaluator</exception>
        private Token EvaluateOperator(Token token, Token leftOperand, Token rightOperand)
        {
            string value;

            if (OperatorTokenTypes.Contains(token.Type))
            {
                double left = TryConvertToNumber<double>(leftOperand);
                double right = TryConvertToNumber<double>(rightOperand);

                switch (token.Type)
                {
                    case TokenTypes.Plus:
                    {
                        value = (left + right).ToString(Culture);
                        break;
                    }
                    case TokenTypes.Minus:
                    {
                        value = (left - right).ToString(Culture);
                        break;
                    }
                    case TokenTypes.Multiply:
                    {
                        value = (left * right).ToString(Culture);
                        break;
                    }
                    case TokenTypes.Divide:
                    {
                        value = (left / right).ToString(Culture);
                        break;
                    }
                    default:
                    {
                        throw ExceptionFactory.Create<ArgumentOutOfRangeException>(Text.OperatorNotSupported, token.Type, token.Value);
                    }
                }
            }
            else if (token.Type == TokenTypes.Dice)
            {
                if (DiceEvaluator == null)
                {
                    throw ExceptionFactory.Create<InvalidOperationException>(Text.MissingDiceEvaluator, token.Type, token.Value);
                }

                int left = TryConvertToNumber<int>(leftOperand);
                int right = TryConvertToNumber<int>(rightOperand);

                value = DiceEvaluator.Evaluate(left, right).Sum().ToString(Culture);
            }
            else if (token.Type == TokenTypes.Sign)
            {
                double right = TryConvertToNumber<double>(rightOperand);

                value = (-right).ToString(Culture);
            }
            else
            {
                throw ExceptionFactory.Create<ArgumentOutOfRangeException>(Text.OperatorNotSupported, token.Type, token.Value);
            }

            return new Token(Lexer.GetDefinitionByType(TokenTypes.Number), value);
        }

        /// <summary>
        /// Converts <paramref name="numberToken"/> value to <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">Value type.</typeparam>
        /// <param name="numberToken">Token with value.</param>
        /// <returns>Token's value.</returns>
        /// <exception cref="InvalidOperationException">On invalid token type or value.</exception>
        private T TryConvertToNumber<T>(Token numberToken)
            where T : struct
        {
            if (numberToken == null || numberToken.Type != TokenTypes.Number)
            {
                throw ExceptionFactory.Create<InvalidOperationException>(Text.InvalidTokenType, TokenTypes.Number, numberToken?.Type.ToString() ?? "<null>");
            }

            T value;

            try
            {
                value = (T)Convert.ChangeType(numberToken.Value, typeof(T), Culture);
            }
            catch (Exception ex)
            {
                Error = ex;
                throw ExceptionFactory.Create<InvalidOperationException>(Text.InvalidTokenValue, "<number>", numberToken.Value ?? "<null>");
            }

            return value;
        }

        /// <summary>
        /// Creates function key.
        /// </summary>
        /// <param name="funcDef">Function definition.</param>
        /// <returns>Function key.</returns>
        private string CreateFunctionsKey(FunctionDefinition funcDef)
        {
            return $"{funcDef}_{string.Join("_", funcDef.Arguments.Select(x => x.TypeName))}";
        }
    }
}
