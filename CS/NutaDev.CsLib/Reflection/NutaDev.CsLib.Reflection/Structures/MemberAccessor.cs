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

using System;

namespace NutaDev.CsLib.Reflection.Structures
{
    /// <summary>
    /// Member accessor class.
    /// </summary>
    /// <typeparam name="TValue">Value type.</typeparam>
    public class MemberAccessor<TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberAccessor{TValue}"/> class.
        /// </summary>
        /// <param name="getter">Member getter.</param>
        /// <param name="setter">Member setter.</param>
        public MemberAccessor(Func<TValue> getter, Action<TValue> setter)
        {
            Get = getter;
            Set = setter;
        }

        /// <summary>
        /// Gets value getter.
        /// </summary>
        public Func<TValue> Get { get; }

        /// <summary>
        /// Gets value setter.
        /// </summary>
        public Action<TValue> Set { get; }
    }

    /// <summary>
    /// Member accessor class.
    /// </summary>
    /// <typeparam name="TObject">Object type.</typeparam>
    /// <typeparam name="TValue">Value type.</typeparam>
    public class BaseMemberAccessor<TObject, TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseMemberAccessor{TObject,TValue}"/> class.
        /// </summary>
        /// <param name="getter">Member getter.</param>
        /// <param name="setter">Member setter.</param>
        public BaseMemberAccessor(Func<TObject, TValue> getter, Action<TObject, TValue> setter)
        {
            GetProperty = getter;
            SetProperty = setter;
        }

        /// <summary>
        /// Gets property getter.
        /// </summary>
        public Func<TObject, TValue> GetProperty { get; }

        /// <summary>
        /// Gets property setter.
        /// </summary>
        public Action<TObject, TValue> SetProperty { get; }
    }

    /// <summary>
    /// Member accessor class.
    /// </summary>
    /// <typeparam name="TObject">Object type.</typeparam>
    /// <typeparam name="TValue">Value type.</typeparam>
    public class MemberAccessor<TObject, TValue>
        : BaseMemberAccessor<TObject, TValue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MemberAccessor{TValue}"/> class.
        /// </summary>
        /// <param name="obj">Object reference.</param>
        /// <param name="getter">Member getter.</param>
        /// <param name="setter">Member setter.</param>
        public MemberAccessor(TObject obj, Func<TObject, TValue> getter, Action<TObject, TValue> setter)
            : base(getter, setter)
        {
            ObjectReference = obj;
        }

        /// <summary>
        /// Gets reference to an object.
        /// </summary>
        public TObject ObjectReference { get; }

        /// <summary>
        /// Gets value.
        /// </summary>
        /// <returns></returns>
        public TValue Get()
        {
            return GetProperty(ObjectReference);
        }

        /// <summary>
        /// Sets value.
        /// </summary>
        /// <param name="value">Value to set.</param>
        public void Set(TValue value)
        {
            if (SetProperty == null) { return; }

            SetProperty(ObjectReference, value);
        }
    }
}
