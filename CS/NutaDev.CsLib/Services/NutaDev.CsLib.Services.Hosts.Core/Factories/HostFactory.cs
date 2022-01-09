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

using Microsoft.Extensions.Configuration;
using NutaDev.CsLib.Services.Core.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace NutaDev.CsLib.Services.Hosts.Core.Factories
{
    /// <summary>
    /// Factory that produces service hosts.
    /// </summary>
    public sealed class HostFactory
    {
        /// <summary>
        /// Name of argument that indicates service's name.
        /// </summary>
        private const string ServiceTypeNameArgument = "-st";

        /// <summary>
        /// Service's directory.
        /// </summary>
        private const string ServicesDirectory = "services";

        /// <summary>
        /// Tries to find service's type name.
        /// </summary>
        /// <param name="args">Startup arguments.</param>
        /// <returns>Service type name or null.</returns>
        public string FindServiceTypeName(string[] args)
        {
            string serviceTypeName = null;

            if (args != null)
            {
                int idx = Array.IndexOf(args, ServiceTypeNameArgument);
                int next = idx + 1;

                if (idx >= 0 && next < args.Length)
                {
                    serviceTypeName = args[next];
                }
            }

            return serviceTypeName;
        }

        /// <summary>
        /// Creates <see cref="ServiceInitializationContext"/>.
        /// </summary>
        /// <param name="startupPath">Startup path.</param>
        /// <param name="arguments">Startup arguments.</param>
        /// <param name="serviceTypeName">Service's type name.</param>
        /// <returns></returns>
        public ServiceInitializationContext CreateInitializationContext(string startupPath, string[] arguments, string serviceTypeName)
        {
            string servicesDirectory = Path.Combine(startupPath, ServicesDirectory);

            ServiceInitializationContext ctx = default(ServiceInitializationContext);

            if (Directory.Exists(servicesDirectory))
            {
                string filePath = $"{servicesDirectory}/{serviceTypeName}/{serviceTypeName}.dll";
                filePath = Path.GetFullPath(filePath);
                Assembly assembly = Assembly.LoadFile(filePath);
                Type type = assembly.ExportedTypes.FirstOrDefault(x => string.Equals(x.Name, serviceTypeName));

                ctx = new ServiceInitializationContext(startupPath, arguments, assembly, type);
            }

            return ctx;
        }

        /// <summary>
        /// Returns an array of startup arguments from arguments file.
        /// </summary>
        /// <returns>Startup arguments.</returns>
        public string[] GetArguments()
        {
            const string configFileName = "appsettings.json";
            const string argumentsKey = "arguments";

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .AddJsonFile(configFileName, true, true);

            IConfigurationRoot config = builder.Build();

            return config.GetSection(argumentsKey).Get<string[]>();
        }

        /// <summary>
        /// Returns startup path based on startup arguments.
        /// </summary>
        /// <param name="args">Startup arguments.</param>
        /// <returns>Startup path or null.</returns>
        public string GetStartupPath(string[] args)
        {
            args = args ?? new string[0];
            int idx = Array.IndexOf(args, "-startPath") + 1;
            return idx < args.Length && idx > 0 ? args[idx] : null;
        }
    }
}
