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

using NutaDev.CsLib.Core.Interfaces;
using System.Diagnostics;

namespace NutaDev.CsLib.Math.Calculator.Tokens
{
    /// <summary>
    /// Token class.
    /// </summary>
    [DebuggerDisplay("{" + nameof(Type) + "} {" + nameof(Value) + "}")]
    public class Token
        : ICloneable<Token>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> class. This is copy constructor.
        /// </summary>
        /// <param name="other">Source object.</param>
        public Token(Token other)
        {
            Definition = other.Definition;
            Value = other.Value;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Token"/> class..
        /// </summary>
        /// <param name="definition">Token definition.</param>
        /// <param name="value">Current value.</param>
        public Token(TokenDefinition definition, string value)
        {
            Definition = definition;
            Value = value;
        }

        /// <summary>
        /// Gets token value.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Gets <see cref="TokenDefinition"/>.
        /// </summary>
        public TokenDefinition Definition { get; }

        /// <summary>
        /// Gets token type.
        /// </summary>
        public TokenTypes Type { get { return Definition.Type; } }

        /// <summary>
        /// Indicates whether token is a function.
        /// </summary>
        public bool IsFunction { get { return Definition.IsFunction; } }

        /// <summary>
        /// Indicates whether token is an operator.
        /// </summary>
        public bool IsOperator { get { return Definition.IsOperator; } }

        /// <summary>
        /// Indicates whether token is a parenthesis.
        /// </summary>
        public bool IsParenthesis { get { return Definition.IsParenthesis; } }

        /// <summary>
        /// Gets token associativity.
        /// </summary>
        public TokenAssociativity Associativity { get { return Definition.Associativity; } }

        /// <summary>
        /// Gets token priority.
        /// </summary>
        public int Priority { get { return Definition.Priority; } }

        /// <summary>
        /// Creates new instance of the current token.
        /// </summary>
        /// <returns>Copy of the current instance.</returns>
        public Token Clone()
        {
            return new Token(this);
        }
    }
}
