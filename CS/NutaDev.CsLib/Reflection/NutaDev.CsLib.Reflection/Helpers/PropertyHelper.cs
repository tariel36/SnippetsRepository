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

using NutaDev.CsLib.Reflection.Extensions;
using System.Collections.Generic;

namespace NutaDev.CsLib.Reflection.Helpers
{
    /// <summary>
    /// Provides helper methods to work with properties through reflection mechanism (<see cref="PropertyInfo"/>).
    /// </summary>
    public static class PropertyHelper
    {
        /// <summary>
        /// This method rewrites property values from <paramref name="fromObj"/> to <paramref name="toObj"/>.
        /// </summary>
        /// <param name="fromObj">Object from where values will be taken.</param>
        /// <param name="toObj">Object to which values will be written.</param>
        public static void WritePropertyValueFromTo(object fromObj, object toObj)
        {
            if (fromObj != null && toObj != null && string.Equals(fromObj.GetType().FullName, toObj.GetType().FullName))
            {
                Dictionary<string, object> fromObjProperties = fromObj.ToPropertyNameValueDictionary();

                foreach (KeyValuePair<string, object> property in fromObjProperties)
                {
                    toObj.SetPropertyValue(property.Key, property.Value);
                }
            }
        }
    }
}
