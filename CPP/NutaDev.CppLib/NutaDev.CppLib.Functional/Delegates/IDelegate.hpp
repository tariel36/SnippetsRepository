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

#ifndef NUTADEV_CPPLIB_FUNCTIONAL_DELEGATES_IDELEGATE_HPP
#define NUTADEV_CPPLIB_FUNCTIONAL_DELEGATES_IDELEGATE_HPP

namespace NutaDev
{
    namespace CppLib
    {
        namespace Functional
        {
            namespace Delegates
            {
                /// <summary>
                /// General template for IDelegate. Used to unwind specialized template.
                /// </summary>
                template <typename>
                class IDelegate
                {
                    /// <summary>
                    /// Destructs this object.
                    /// </summary>
                public: virtual ~IDelegate()
                {

                }
                };

                /// <summary>
                /// Specialized template for abstract IDelegate class for delegate class family.
                /// </summary>
                /// <param name="TRet">Return type of the deleagte.</param>
                /// <param name="TArgs">List of argument types.</param>
                template <typename TRet, typename... TArgs>
                class IDelegate<TRet(TArgs ...)>
                {
                public:
                    /// <summary>Wrapper for called function.</summary>
                    /// <param name="args">Function arguments.</param>
                    /// <param name="TRet">Returned types.</param>
                    /// <param name="TArgs">Argument types.</param>
                    /// <returns>Function call result.</returns>
                    virtual TRet Call(TArgs ... args) = 0;

                    /// <summary>
                    /// Destructs this object.
                    /// </summary>
                    virtual ~IDelegate()
                    {

                    }
                };
            }
        }
    }
}

#endif
