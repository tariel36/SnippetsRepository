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
using NutaDev.CsLib.Resources.Text.Exceptions;
using NutaDev.CsLib.Services.Core.Attributes;
using System;
using System.Reflection;

namespace NutaDev.CsLib.Services.Core.Models
{
    /// <summary>
    /// Context used on service initialization.
    /// </summary>
    public class ServiceInitializationContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceInitializationContext"/> class.
        /// </summary>
        /// <param name="startupPath">Executable path.</param>
        /// <param name="arguments">Startup arguments.</param>
        /// <param name="assembly">Service assembly.</param>
        /// <param name="type">Service type.</param>
        public ServiceInitializationContext(string startupPath, string[] arguments, Assembly assembly, Type type)
        {
            StartupPath = startupPath ?? throw ExceptionFactory.ArgumentNullException(nameof(startupPath));
            Arguments = arguments;
            Assembly = assembly ?? throw ExceptionFactory.ArgumentNullException(nameof(assembly));
            Type = type ?? throw ExceptionFactory.ArgumentNullException(nameof(type));

            ServiceDescription = type.GetCustomAttribute<ServiceDescriptionAttribute>() ?? throw ExceptionFactory.InvalidOperationException(Text.ServiceDescriptionIsRequired);
        }

        /// <summary>
        /// Gets executable path.
        /// </summary>
        public string StartupPath { get; }

        /// <summary>
        /// Gets startup arguments.
        /// </summary>
        public string[] Arguments { get; }

        /// <summary>
        /// Gets service's assembly.
        /// </summary>
        public Assembly Assembly { get; }

        /// <summary>
        /// Gets service's type.
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// Gets service's description.
        /// </summary>
        public string Description { get { return ServiceDescription.Description; } }

        /// <summary>
        /// Gets service's name.
        /// </summary>
        public string ServiceName { get { return ServiceDescription.ServiceName; } }

        /// <summary>
        /// Gets service's display name.
        /// </summary>
        public string DisplayName { get { return ServiceDescription.DisplayName; } }

        /// <summary>
        /// Gets service's metadata.
        /// </summary>
        private ServiceDescriptionAttribute ServiceDescription { get; }
    }
}
