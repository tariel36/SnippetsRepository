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

#ifndef NUTADEV_CPPLIB_FUNCTIONAL_DELEGATES_FUNCTIONDELEGATE_HPP
#define NUTADEV_CPPLIB_FUNCTIONAL_DELEGATES_FUNCTIONDELEGATE_HPP

#include <functional>

#include "IDelegate.hpp"

namespace NutaDev
{
    namespace CppLib
    {
        namespace Functional
        {
            namespace Delegates
            {
                /// <summary>
                /// General template for FunctionDelegate. Used to unwind specialized template.
                /// </summary>
                template <typename>
                class FunctionDelegate
                {

                };

                /// <summary>
                /// Specialized template which wrapps nonmember function.
                /// </summary>
                /// <param name="TRet">Returned type..</param>
                /// <param name="TArgs">Function arguments.</param>
                template <typename TRet, typename... TArgs>
                class FunctionDelegate<TRet(TArgs ...)>
                    : public IDelegate<TRet(TArgs ...)>
                {
                public:
                    /// <summary>Pointer to wrapped function.</summary>
                    std::function<TRet(TArgs...)> Foo;

                    /// <summary>
                    /// Initializes a new instance of this class.
                    /// </summary>
                    FunctionDelegate()
                    {

                    }

                    /// <summary>
                    /// Initializes a new instance of this class.
                    /// </summary>
                    /// <param name="foo">Function pointer.</param>
                    FunctionDelegate(std::function<TRet(TArgs...)> foo)
                        : Foo(foo)
                    {

                    }

                    /// <summary>Wrapper for called function.</summary>
                    /// <param name="args">Function arguments.</param>
                    /// <param name="TRet">Returned types.</param>
                    /// <param name="TArgs">Argument types.</param>
                    /// <returns>Function call result.</returns>
                    TRet Call(TArgs... args) override
                    {
                        return Foo(args...);
                    }
                };

            }
        }
    }
}

#endif
