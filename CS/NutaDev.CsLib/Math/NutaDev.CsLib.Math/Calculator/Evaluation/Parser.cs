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

namespace NutaDev.CsLib.Math.Calculator.Evaluation
{
    /// <summary>
    /// Simple parser implementation.
    /// </summary>
    public class Parser
    {
        /// <summary>
        /// Converts <see cref="tokens"/> collection to RPN queue.
        /// </summary>
        /// <param name="tokens">Collection of tokens.</param>
        /// <returns>RPN queue.</returns>
        /// <exception cref="InvalidOperationException">Misplaced parenthesis or argument separator.</exception>
        public Queue<Token> ToRpn(ICollection<Token> tokens)
        {
            Queue<Token> tokenQueue = new Queue<Token>();
            Stack<Token> operatorStack = new Stack<Token>();

            foreach (Token token in tokens)
            {
                if (token.Type == TokenTypes.Number)
                {
                    tokenQueue.Enqueue(token);
                }
                else if (token.IsFunction)
                {
                    operatorStack.Push(token);
                }
                else if (token.Type == TokenTypes.Comma)
                {
                    while (operatorStack.Count > 0 && operatorStack.Peek().Type != TokenTypes.LeftParenthesis)
                    {
                        tokenQueue.Enqueue(operatorStack.Pop());
                    }

                    if (operatorStack.Count > 0 && operatorStack.Peek().Type != TokenTypes.LeftParenthesis)
                    {
                        throw ExceptionFactory.Create<InvalidOperationException>(Text.MismatchedParentheses);
                    }
                }
                else if (token.IsOperator)
                {
                    while (operatorStack.Count > 0 && ((operatorStack.Peek().IsFunction)
                            || (operatorStack.Peek().IsOperator && operatorStack.Peek().Priority > token.Priority)
                            || (operatorStack.Peek().IsOperator && operatorStack.Peek().Priority == token.Priority && operatorStack.Peek().Associativity == TokenAssociativity.LeftToRight)
                        ) && operatorStack.Peek().Type != TokenTypes.LeftParenthesis)
                    {
                        tokenQueue.Enqueue(operatorStack.Pop());
                    }

                    operatorStack.Push(token);
                }
                else if (token.Type == TokenTypes.LeftParenthesis)
                {
                    operatorStack.Push(token);
                }
                else if (token.Type == TokenTypes.RightParenthesis)
                {
                    while (operatorStack.Count > 0 && operatorStack.Peek().Type != TokenTypes.LeftParenthesis)
                    {
                        tokenQueue.Enqueue(operatorStack.Pop());
                    }

                    if (operatorStack.Count == 0)
                    {
                        throw ExceptionFactory.Create<InvalidOperationException>(Text.MisplacedParenthesisOrArgumentSeparator);
                    }

                    operatorStack.Pop();
                }
            }

            while (operatorStack.Count > 0)
            {
                if (operatorStack.Peek().IsParenthesis)
                {
                    throw ExceptionFactory.Create<InvalidOperationException>(Text.MismatchedParentheses);
                }

                tokenQueue.Enqueue(operatorStack.Pop());
            }

            return tokenQueue;
        }
    }
}
