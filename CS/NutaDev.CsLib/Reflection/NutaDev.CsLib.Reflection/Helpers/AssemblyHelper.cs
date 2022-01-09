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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace NutaDev.CsLib.Reflection.Helpers
{
    /// <summary>
    /// This class provides helper methods for <see cref="Assembly"/> class.
    /// </summary>
    public static class AssemblyHelper
    {
        /// <summary>
        /// Returns executable path to file for provided <see cref="Assembly"/>. If no <see cref="Assembly"/> provided,
        /// the <see cref="Assembly.GetEntryAssembly"/> is used.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> to use.</param>
        /// <returns>Path to executable file.</returns>
        public static string GetExecutableFilePath(Assembly assembly = null)
        {
            if (assembly == null)
            {
                assembly = Assembly.GetEntryAssembly();
            }

            return assembly?.Location;
        }

        /// <summary>
        /// Returns the executable's file name in the given <see cref="Assembly"/> <paramref name="assembly"/>. If no <see cref="Assembly"/> provided,
        /// the <see cref="Assembly.GetEntryAssembly"/> is used.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> to use.</param>
        /// <returns>Executable's filename.</returns>
        public static string GetExecutableFileName(Assembly assembly = null)
        {
            if (assembly == null)
            {
                assembly = Assembly.GetEntryAssembly();
            }

            return assembly?.Location.Substring(assembly.Location.LastIndexOf(Path.DirectorySeparatorChar) + 1);
        }

        /// <summary>
        /// Returns build time of the <paramref name="assembly"/>.
        /// It works only if <paramref name="assembly"/> has been built with auto version (x.x.*.* in AssemblyInfo.cs).
        /// </summary>
        /// <param name="assembly"><see cref="Assembly"/> to get build time from.</param>
        /// <returns>Build time of the <paramref name="assembly"/>.</returns>
        public static DateTime GetBuildDateTime(Assembly assembly = null)
        {
            Version version = GetAssemblyVersion(assembly);

            return new DateTime(2000, 1, 1).Add(new TimeSpan(TimeSpan.TicksPerDay * version.Build + TimeSpan.TicksPerSecond * 2 * version.Revision));
        }

        /// <summary>
        /// Returns the value of the <see cref="AssemblyTitleAttribute"/> from provided <see cref="Assembly"/> <paramref name="assembly"/>.
        /// If no <see cref="Assembly"/> provided, the <see cref="Assembly.GetEntryAssembly"/> is used.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> to use.</param>
        /// <returns>Value of the <see cref="AssemblyTitleAttribute"/> attribute.</returns>
        public static string GetAssemblyTitle(Assembly assembly = null)
        {
            return GetCustomAttributeAs<AssemblyTitleAttribute>(typeof(AssemblyTitleAttribute), assembly)?.Title;
        }

        /// <summary>
        /// Returns the value of the <see cref="AssemblyDescriptionAttribute"/> from provided <see cref="Assembly"/> <paramref name="assembly"/>.
        /// If no <see cref="Assembly"/> provided, the <see cref="Assembly.GetEntryAssembly"/> is used.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> to use.</param>
        /// <returns>Value of the <see cref="AssemblyDescriptionAttribute"/> attribute.</returns>
        public static string GetAssemblyDescription(Assembly assembly = null)
        {
            return GetCustomAttributeAs<AssemblyDescriptionAttribute>(typeof(AssemblyDescriptionAttribute), assembly)?.Description;
        }

        /// <summary>
        /// Returns the value of the <see cref="AssemblyConfigurationAttribute"/> from provided <see cref="Assembly"/> <paramref name="assembly"/>.
        /// If no <see cref="Assembly"/> provided, the <see cref="Assembly.GetEntryAssembly"/> is used.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> to use.</param>
        /// <returns>Value of the <see cref="AssemblyConfigurationAttribute"/> attribute.</returns>
        public static string GetAssemblyConfiguration(Assembly assembly = null)
        {
            return GetCustomAttributeAs<AssemblyConfigurationAttribute>(typeof(AssemblyConfigurationAttribute), assembly)?.Configuration;
        }

        /// <summary>
        /// Returns the value of the <see cref="AssemblyCompanyAttribute"/> from provided <see cref="Assembly"/> <paramref name="assembly"/>.
        /// If no <see cref="Assembly"/> provided, the <see cref="Assembly.GetEntryAssembly"/> is used.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> to use.</param>
        /// <returns>Value of the <see cref="AssemblyCompanyAttribute"/> attribute.</returns>
        public static string GetAssemblyCompany(Assembly assembly = null)
        {
            return GetCustomAttributeAs<AssemblyCompanyAttribute>(typeof(AssemblyCompanyAttribute), assembly)?.Company;
        }

        /// <summary>
        /// Returns the value of the <see cref="AssemblyProductAttribute"/> from provided <see cref="Assembly"/> <paramref name="assembly"/>.
        /// If no <see cref="Assembly"/> provided, the <see cref="Assembly.GetEntryAssembly"/> is used.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> to use.</param>
        /// <returns>Value of the <see cref="AssemblyProductAttribute"/> attribute.</returns>
        public static string GetAssemblyProduct(Assembly assembly = null)
        {
            return GetCustomAttributeAs<AssemblyProductAttribute>(typeof(AssemblyProductAttribute), assembly)?.Product;
        }

        /// <summary>
        /// Returns the value of the <see cref="AssemblyCopyrightAttribute"/> from provided <see cref="Assembly"/> <paramref name="assembly"/>.
        /// If no <see cref="Assembly"/> provided, the <see cref="Assembly.GetEntryAssembly"/> is used.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> to use.</param>
        /// <returns>Value of the <see cref="AssemblyCopyrightAttribute"/> attribute.</returns>
        public static string GetAssemblyCopyright(Assembly assembly = null)
        {
            return GetCustomAttributeAs<AssemblyCopyrightAttribute>(typeof(AssemblyCopyrightAttribute), assembly)?.Copyright;
        }

        /// <summary>
        /// Returns the value of the <see cref="AssemblyTrademarkAttribute"/> from provided <see cref="Assembly"/> <paramref name="assembly"/>.
        /// If no <see cref="Assembly"/> provided, the <see cref="Assembly.GetEntryAssembly"/> is used.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> to use.</param>
        /// <returns>Value of the <see cref="AssemblyTrademarkAttribute"/> attribute.</returns>
        public static string GetAssemblyTrademark(Assembly assembly = null)
        {
            return GetCustomAttributeAs<AssemblyTrademarkAttribute>(typeof(AssemblyTrademarkAttribute), assembly)?.Trademark;
        }

        /// <summary>
        /// Returns the value of the <see cref="AssemblyCultureAttribute"/> from provided <see cref="Assembly"/> <paramref name="assembly"/>.
        /// If no <see cref="Assembly"/> provided, the <see cref="Assembly.GetEntryAssembly"/> is used.
        /// </summary>
        /// <param name="assembly">The <see cref="Assembly"/> to use.</param>
        /// <returns>Value of the <see cref="AssemblyCultureAttribute"/> attribute.</returns>
        public static string GetAssemblyCulture(Assembly assembly = null)
        {
            return GetCustomAttributeAs<AssemblyCultureAttribute>(typeof(AssemblyCultureAttribute), assembly)?.Culture;
        }

        /// <summary>
        /// Returns the <see cref="System.Version"/> of given assembly (or <see cref="Assembly.GetEntryAssembly()"/> if <paramref name="assembly"/> is null).
        /// </summary>
        /// <param name="assembly">Assembly to use.</param>
        /// <returns>The <see cref="System.Version"/> of <paramref name="assembly"/>.</returns>
        public static Version GetAssemblyVersion(Assembly assembly = null)
        {
            if (assembly == null)
            {
                assembly = Assembly.GetEntryAssembly();
            }

            return assembly?.GetName().Version;
        }

        /// <summary>
        /// Returns version string (<see cref="System.Version"/> of given <see cref="Assembly"/> <paramref name="assembly"/> (or <see cref="Assembly.GetEntryAssembly()"/> if <paramref name="assembly"/> is null).
        /// </summary>
        /// <param name="assembly">Assembly to use.</param>
        /// <returns>The version string.</returns>
        public static string GetVersionString(Assembly assembly = null)
        {
            return GetAssemblyVersion(assembly).ToString();
        }

        /// <summary>
        /// Returns array of custom attribute from provided <see cref="Assembly"/> (or <see cref="Assembly.GetEntryAssembly()"/> if <paramref name="assembly"/> is null).
        /// </summary>
        /// <param name="attributeType">Attribute type.</param>
        /// <param name="assembly">Assembly to use.</param>
        /// <returns><see cref="Attribute"/> of type <paramref name="attributeType"/>.</returns>
        public static object[] GetCustomAttributes(Type attributeType, Assembly assembly = null)
        {
            if (assembly == null)
            {
                assembly = Assembly.GetEntryAssembly();
            }

            return assembly?.GetCustomAttributes(attributeType, false);
        }

        /// <summary>
        /// Returns custom attribute from provided <see cref="Assembly"/> (or <see cref="Assembly.GetEntryAssembly()"/> if <paramref name="assembly"/> is null).
        /// </summary>
        /// <param name="attributeType">Attribute type.</param>
        /// <param name="assembly">Assembly to use.</param>
        /// <returns><see cref="Attribute"/> of type <paramref name="attributeType"/>.</returns>
        public static object GetCustomAttribute(Type attributeType, Assembly assembly = null)
        {
            return GetCustomAttributes(attributeType, assembly).FirstOrDefault();
        }

        /// <summary>
        /// Returns array of custom attributes from provided <see cref="Assembly"/> (or <see cref="Assembly.GetEntryAssembly()"/> if <paramref name="assembly"/> is null)
        /// of provided type <paramref name="{T}"/>.
        /// </summary>
        /// <typeparam name="T">Attribute type.</typeparam>
        /// <param name="attributeType">Attribute type.</param>
        /// <param name="assembly">Assembly to use.</param>
        /// <returns>Array of <see cref="Attribute"/> of type <paramref name="{T}"/>.</returns>
        public static T[] GetCustomAttributesAs<T>(Type attributeType, Assembly assembly = null)
        {
            return GetCustomAttributes(attributeType, assembly).Cast<T>().ToArray();
        }

        /// <summary>
        /// Returns custom attribute from provided <see cref="Assembly"/> (or <see cref="Assembly.GetEntryAssembly()"/> if <paramref name="assembly"/> is null)
        /// of provided type <paramref name="{T}"/>.
        /// </summary>
        /// <typeparam name="T">Attribute type.</typeparam>
        /// <param name="attributeType">Attribute type.</param>
        /// <param name="assembly">Assembly to use.</param>
        /// <returns><see cref="Attribute"/> of type <paramref name="{T}"/>.</returns>
        public static T GetCustomAttributeAs<T>(Type attributeType, Assembly assembly = null)
        {
            return GetCustomAttributesAs<T>(attributeType, assembly).FirstOrDefault();
        }
    }
}
