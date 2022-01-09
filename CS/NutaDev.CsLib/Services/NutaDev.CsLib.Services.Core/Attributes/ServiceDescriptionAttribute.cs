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

namespace NutaDev.CsLib.Services.Core.Attributes
{
    /// <summary>
    /// An attribute that holds basic information about service.
    /// </summary>
    public class ServiceDescriptionAttribute
        : Attribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceDescriptionAttribute"/> class.
        /// </summary>
        /// <param name="serviceName">Name of service.</param>
        /// <param name="displayName">Display name of service.</param>
        public ServiceDescriptionAttribute(string serviceName, string displayName)
            : this(serviceName, displayName, string.Empty)
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceDescriptionAttribute"/> class.
        /// </summary>
        /// <param name="serviceName">Name of service.</param>
        /// <param name="displayName">Display name of service.</param>
        /// <param name="description">Service description</param>
        public ServiceDescriptionAttribute(string serviceName, string displayName, string description)
        {
            ServiceName = serviceName;
            DisplayName = displayName;
            Description = description;
        }

        /// <summary>
        /// Gets service name.
        /// </summary>
        public string ServiceName { get; }

        /// <summary>
        /// Gets service display name.
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// Gets service description.
        /// </summary>
        public string Description { get; }
    }
}
