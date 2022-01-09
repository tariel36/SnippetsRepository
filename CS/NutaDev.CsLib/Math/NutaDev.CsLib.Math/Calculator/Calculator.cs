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

using NutaDev.CsLib.Math.Calculator.Attributes;
using NutaDev.CsLib.Math.Calculator.Evaluation;
using NutaDev.CsLib.Math.Calculator.Functions;
using System;
using System.Collections.Generic;
using System.Text;

namespace NutaDev.CsLib.Math.Calculator
{
    /// <summary>
    /// Simple calculator implemntation.
    /// </summary>
    public class Calculator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Calculator"/> class.
        /// </summary>
        public Calculator()
        {
            Lexer = new Lexer();
            Parser = new Parser();
            Evaluator = new RpnEvaluator();
        }

        /// <summary>
        /// Gets lexer instance.
        /// </summary>
        private Lexer Lexer { get; }

        /// <summary>
        /// Gets parser instance.
        /// </summary>
        private Parser Parser { get; }

        /// <summary>
        /// Gets evaluator instance.
        /// </summary>
        private RpnEvaluator Evaluator { get; }

        /// <summary>
        /// Determines whether <paramref name="expression"/> is a valid expression.
        /// </summary>
        /// <param name="expression">Expression to validate.</param>
        /// <returns>Ture if <see cref="expression"/> is valid, otherwise false.</returns>
        public bool IsValid(string expression)
        {
            try
            {
                Evaluate(expression);
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Evaluates an <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">Expression to evaluate.</param>
        /// <returns>Expression result.</returns>
        public string Evaluate(string expression)
        {
            return Evaluator.Evaluate(Parser.ToRpn(Lexer.Tokenize(expression)));
        }

        /// <summary>
        /// Evaluates an <paramref name="expression"/>.
        /// </summary>
        /// <param name="expression">Expression to evaluate.</param>
        /// <returns>Expression result.</returns>
        public double Calculate(string expression)
        {
            return Convert.ToDouble(Evaluate(expression));
        }

        /// <summary>
        /// Registers basic functions.
        /// </summary>
        private void RegisterFunctions()
        {
            Evaluator.RegisterFunction(AnonymousFunctionDefinition.CreateDefinition(this, nameof(Min)));
            Evaluator.RegisterFunction(AnonymousFunctionDefinition.CreateDefinition(this, nameof(Max)));
        }

        /// <summary>
        /// Returns the smaller of two double-precision floating-point numbers.
        /// </summary>
        /// <param name="args">Function arguments.</param>
        /// <returns>Function result.</returns>
        [Argument("a", typeof(double))]
        [Argument("b", typeof(double))]
        private string Min(object[] args)
        {
            double a = Convert.ToDouble(args[0], Evaluator.Culture);
            double b = Convert.ToDouble(args[1], Evaluator.Culture);

            return System.Math.Min(a, b).ToString(Evaluator.Culture);
        }

        /// <summary>
        /// Returns the larger of two double-precision floating-point numbers.
        /// </summary>
        /// <param name="args">Function arguments.</param>
        /// <returns>Function result.</returns>
        [Argument("a", typeof(double))]
        [Argument("b", typeof(double))]
        private string Max(object[] args)
        {
            double a = Convert.ToDouble(args[0], Evaluator.Culture);
            double b = Convert.ToDouble(args[1], Evaluator.Culture);

            return System.Math.Max(a, b).ToString(Evaluator.Culture);
        }
    }
}
