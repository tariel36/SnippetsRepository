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
using NutaDev.CsLib.Math.Calculator.Attributes;
using NutaDev.CsLib.Math.Calculator.Tokens;
using NutaDev.CsLib.Resources.Text.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NutaDev.CsLib.Math.Calculator.Functions
{
    /// <summary>
    /// Represents the anonymous function definition.
    /// </summary>
    public class AnonymousFunctionDefinition
        : FunctionDefinition
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AnonymousFunctionDefinition"/> class.
        /// </summary>
        /// <param name="name">Function name.</param>
        /// <param name="args">Function arguments.</param>
        /// <param name="func">Delegate to the function that evaluates expression.</param>
        public AnonymousFunctionDefinition(string name, IEnumerable<FunctionArgument> args, Func<object[], object> func)
            : base(name, args)
        {
            Func = func;
        }

        /// <summary>
        /// Gets delegate to the function that evaluates expression.
        /// </summary>
        public Func<object[], object> Func { get; }

        /// <summary>
        /// Evaluates <see cref="Func"/>.
        /// </summary>
        /// <param name="args">Stack with function arguments.</param>
        /// <returns>Function result.</returns>
        public override object Evaluate(Stack<Token> args)
        {
            return Func(args.Select(x => x.Value).Cast<object>().ToArray());
        }

        /// <summary>
        /// Creates <see cref="AnonymousFunctionDefinition"/> instance based on caller and name. Function must be an instance function, but may be public or non public.
        /// </summary>
        /// <param name="caller">Function caller.</param>
        /// <param name="name">Function name.</param>
        /// <returns>Instance of the <see cref="AnonymousFunctionDefinition"/>.</returns>
        /// <exception cref="InvalidOperationException">Method with <paramref name="name"/> not found on <paramref name="caller"/> object.</exception>
        public static AnonymousFunctionDefinition CreateDefinition(object caller, string name)
        {
            MethodInfo method = caller.GetType().GetMethod(name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (method == null)
            {
                throw ExceptionFactory.Create<InvalidOperationException>(Text.Function_0_NotFound, name);
            }

            FunctionArgument[] args = method.GetCustomAttributes<ArgumentAttribute>().Select(x => new FunctionArgument(x.Name, x.Type)).ToArray();
            Func<object[], string> fun = (Func<object[], string>)method.CreateDelegate(typeof(Func<object[], string>), caller);

            return new AnonymousFunctionDefinition(name.ToLower(), args, fun);
        }
    }
}
