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

#ifndef NUTADEV_CPPLIB_FUNCTIONAL_EVENTS_EVENT_HPP
#define NUTADEV_CPPLIB_FUNCTIONAL_EVENTS_EVENT_HPP

#include <functional>
#include <list>

#include "../Delegates/IDelegate.hpp"

namespace NutaDev
{
    namespace CppLib
    {
        namespace Functional
        {
            namespace Events
            {
                /// <summary>
                /// General template for Event. Used to unwind specialized template.
                /// </summary>
                template <typename TRet, typename... TArgs>
                class Event
                {

                };

                /// <summary>
                /// Template for Event class. Event contains delegates of the same type.
                /// This class deletes the registered delegates.
                /// </summary>
                /// <param name="TRet">Return type of the deleagte.</param>
                /// <param name="TArgs">List of argument types.</param>
                template <typename TRet, typename... TArgs>
                class Event<TRet(TArgs...)>
                {
                public:
                    /// <summary>
                    /// Deletes all delegates.
                    /// </summary>
                    ~Event()
                    {
                        for (auto iter = _delegates.begin(); iter != _delegates.end(); ++iter)
                        {
                            delete (*iter);
                        }
                    }

                    /// <summary>
                    /// Registers delegate in the event.
                    /// </summary>
                    /// <param name="del">Pointer to the deleagte.</param>
                    /// <returns>Reference to itself.</returns>
                    Event & Register(IDelegate<TRet(TArgs...)> * del)
                    {
                        _delegates.push_back(del);

                        return *this;
                    }

                    /// <summary>
                    /// Unregisters delegate from the event. The delegate is not deleted.
                    /// </summary>
                    /// <param name="del">Pointer to the deleagte.</param>
                    /// <returns>Reference to itself.</returns>
                    Event & Unregister(IDelegate<TRet(TArgs ...)> * del)
                    {
                        typename std::list<IDelegate<TRet(TArgs ...)>*>::iterator item = Find(del);
                        if (item != _delegates.end())
                        {
                            _delegates.erase(item);
                        }

                        return *this;
                    }

                    /// <summary>
                    /// Calls all registered delegates.
                    /// </summary>
                    /// <param name="args">Arguments passed to delegates.</a>
                    void Call(TArgs... args)
                    {
                        for (IDelegate<TRet(TArgs ...)> * del : _delegates)
                        {
                            del->Call(args...);
                        }
                    }

                    /// <summary>
                    /// Registers delegate in the event.
                    /// </summary>
                    /// <param name="del">Pointer to the deleagte.</param>
                    /// <returns>Reference to itself.</returns>
                    /// <see name="Register" />
                    Event & operator += (IDelegate<TRet(TArgs ...)> * del)
                    {
                        return Register(del);
                    }

                    /// <summary>
                    /// Unregisters delegate from the event. The delegate is not deleted.
                    /// </summary>
                    /// <param name="del">Pointer to the deleagte.</param>
                    /// <returns>Reference to itself.</returns>
                    /// <see name="Unregister" />
                    Event & operator -= (IDelegate<TRet(TArgs ...)> * del)
                    {
                        return Unregister(del);
                    }

                private:
                    /// <summary>Registered delegates.</summary>
                    std::list<IDelegate<TRet(TArgs ...)>*> _delegates;

                    /// <summary>
                    /// Searches for the delegate using its address.
                    /// </summary>
                    /// <param name="del">Pointer to the delegate to find.</param>
                    /// <returns>Iterator to the element or end() if not found.</returns>
                    typename std::list<IDelegate<TRet(TArgs ...)>*>::iterator Find(IDelegate<TRet(TArgs ...)> * del)
                    {
                        for (typename std::list<IDelegate<TRet(TArgs ...)>*>::iterator iter = _delegates.begin(); iter != _delegates.end(); ++iter)
                        {
                            if ((*iter) == del)
                            {
                                return iter;
                            }
                        }

                        return _delegates.end();
                    }
                };

            }
        }
    }
}

#endif
