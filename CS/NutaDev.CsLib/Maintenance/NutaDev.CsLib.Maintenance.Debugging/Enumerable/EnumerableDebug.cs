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
using System.Linq;
using System.Reflection;
using System.Text;

namespace NutaDev.CsLib.Maintenance.Debugging.Enumerable
{
    /// <summary>
    /// Helper methods for <see cref="NutaDev.CsLib.Debugging.IEnumerable"/> objects.
    /// </summary>
    public static class EnumerableDebug
    {
        /// <summary>
        /// Prints <see cref="NutaDev.CsLib.Debugging.IEnumerable"/>.
        /// </summary>
        /// <param name="enumerable"><see cref="NutaDev.CsLib.Debugging.IEnumerable"/> to print.</param>
        /// <returns>Printed <see cref="NutaDev.CsLib.Debugging.IEnumerable"/>.</returns>
        public static string PrintIEnumerable(this System.Collections.IEnumerable enumerable)
        {
            StringBuilder sb = new StringBuilder();
            int idx = 0;

            foreach (object obj in enumerable)
            {
                sb.AppendLine($"[{idx++}]: {obj?.ToString() ?? "<null>"}");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Prints <see cref="NutaDev.CsLib.Debugging.IEnumerable"/>.
        /// </summary>
        /// <param name="enumerable"><see cref="NutaDev.CsLib.Debugging.IEnumerable"/> to print.</param>
        /// <param name="type">Type of objects.</param>
        /// <param name="fieldNames">Name of fields to print.</param>
        /// <returns>Printed <see cref="NutaDev.CsLib.Debugging.IEnumerable"/>.</returns>
        public static string PrintIEnumerableFields(this System.Collections.IEnumerable enumerable, Type type, params string[] fieldNames)
        {
            StringBuilder sb = new StringBuilder();
            int idx = 0;

            FieldInfo[] fields =
                type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                    .Where(x => fieldNames == null || fieldNames.Length <= 0 || fieldNames.Contains(x.Name))
                    .ToArray();

            foreach (object obj in enumerable)
            {
                if (obj == null)
                {
                    sb.AppendLine($"[{idx}]: <null>");
                }
                else
                {
                    sb.AppendLine($"[{idx}]:");
                    foreach (FieldInfo fi in fields)
                    {
                        try
                        {
                            sb.AppendLine($"[{idx}]:     {fi.GetValue(obj)}");
                        }
                        catch (Exception ex)
                        {
                            sb.AppendLine($"[{idx}]:     Exception: {ex.GetType().Name}");
                        }
                    }
                }

                idx++;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Prints <see cref="NutaDev.CsLib.Debugging.IEnumerable"/>.
        /// </summary>
        /// <param name="enumerable"><see cref="NutaDev.CsLib.Debugging.IEnumerable"/> to print.</param>
        /// <param name="type">Type of objects.</param>
        /// <param name="propertyNames">Name of properties to print.</param>
        /// <returns>Printed <see cref="NutaDev.CsLib.Debugging.IEnumerable"/>.</returns>
        public static string PrintIEnumerableProperties(this System.Collections.IEnumerable enumerable, Type type, params string[] propertyNames)
        {
            StringBuilder sb = new StringBuilder();
            int idx = 0;

            PropertyInfo[] properties =
                type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                    .Where(x => propertyNames == null || propertyNames.Length <= 0 || propertyNames.Contains(x.Name))
                    .ToArray();

            foreach (object obj in enumerable)
            {
                if (obj == null)
                {
                    sb.AppendLine($"[{idx}]: <null>");
                }
                else
                {
                    sb.AppendLine($"[{idx}]:");
                    foreach (PropertyInfo pi in properties)
                    {
                        try
                        {
                            sb.AppendLine($"[{idx}]:     {pi.GetValue(obj)}");
                        }
                        catch (Exception ex)
                        {
                            sb.AppendLine($"[{idx}]:     Exception: {ex.GetType().Name}");
                        }
                    }
                }

                idx++;
            }

            return sb.ToString();
        }

        /// <summary>
        /// Prints <see cref="NutaDev.CsLib.Debugging.IEnumerable"/>.
        /// </summary>
        /// <param name="enumerable"><see cref="NutaDev.CsLib.Debugging.IEnumerable"/> to print.</param>
        /// <param name="type">Type of objects.</param>
        /// <param name="names">Names of fields and properties to print.</param>
        /// <returns>Printed <see cref="NutaDev.CsLib.Debugging.IEnumerable"/>.</returns>
        public static string PrintIEnumerableValues(this System.Collections.IEnumerable enumerable, Type type, params string[] names)
        {
            StringBuilder sb = new StringBuilder();

            FieldInfo[] fields =
                type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                    .Where(x => names == null || names.Length <= 0 || names.Contains(x.Name))
                    .ToArray();

            PropertyInfo[] properties =
                type.GetProperties(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
                    .Where(x => names == null || names.Length <= 0 || names.Contains(x.Name))
                    .ToArray();

            int idx = 0;

            foreach (object obj in enumerable)
            {
                if (obj == null)
                {
                    sb.AppendLine($"[{idx}]: <null>");
                }
                else
                {
                    sb.AppendLine($"[{idx}]:");

                    foreach (FieldInfo pi in fields)
                    {
                        try
                        {
                            sb.AppendLine($"[{idx}]:     F: {pi.GetValue(obj)}");
                        }
                        catch (Exception ex)
                        {
                            sb.AppendLine($"[{idx}]:     F: Exception: {ex.GetType().Name}");
                        }
                    }

                    foreach (PropertyInfo pi in properties)
                    {
                        try
                        {
                            sb.AppendLine($"[{idx}]:     P: {pi.GetValue(obj)}");
                        }
                        catch (Exception ex)
                        {
                            sb.AppendLine($"[{idx}]:     P: Exception: {ex.GetType().Name}");
                        }
                    }
                }

                idx++;
            }

            return sb.ToString();
        }
    }
}
