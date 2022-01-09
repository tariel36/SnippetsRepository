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

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using NutaDev.CsLib.Maintenance.Exceptions.Factories;
using NutaDev.CsLib.Resources.Text.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace NutaDev.CsLib.Reflection.CodeCompilation.Compilers
{
    /// <summary>
    /// Class that provides dynamic compilation.
    /// </summary>
    public sealed class DynamicCodeCompiler
    {
        /// <summary>
        /// Initializes a new instnace of the <see cref="DynamicCodeCompiler"/> class.
        /// </summary>
        /// <param name="startupPath">Execution path. If not provided then <see cref="Assembly.Location"/> (<see cref="Assembly.GetExecutingAssembly"/> is used.</param>
        public DynamicCodeCompiler(string startupPath = null)
        {
            StartupPath = startupPath ?? Assembly.GetExecutingAssembly().Location;
        }

        /// <summary>
        /// Gets or sets startup path.
        /// </summary>
        private string StartupPath { get; }

        // The MIT License (MIT)
        // 
        // Copyright (c) 2016 Joel Martinez
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
        //
        // https://github.com/joelmartinez/dotnet-core-roslyn-sample
        public Assembly Compile(string[] referenceDlls, string codeToCompile, bool useGlobalResolver = false, bool useDefaultReferences = false)
        {
            if (useGlobalResolver)
            {
                AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;
            }

            try
            {
                SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(codeToCompile);

                IEnumerable<string> refPathsCollection = new string[0];

                if (useDefaultReferences)
                {
                    refPathsCollection = refPathsCollection.Concat(new[]
                    {
                        typeof(Object).Assembly.Location,
                        Path.Combine(Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location), "System.Runtime.dll"),
                    });
                }

                refPathsCollection = refPathsCollection.Concat(referenceDlls);

                string assemblyName = Path.GetRandomFileName();
                string[] refPaths = refPathsCollection.ToArray();

                MetadataReference[] references = refPaths.Select(r => MetadataReference.CreateFromFile(r)).ToArray();

                CSharpCompilation compilation = CSharpCompilation.Create(
                    assemblyName,
                    syntaxTrees: new[] { syntaxTree },
                    references: references,
                    options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

                using (MemoryStream ms = new MemoryStream())
                {
                    EmitResult result = compilation.Emit(ms);

                    if (!result.Success)
                    {
                        IEnumerable<Diagnostic> failures = result.Diagnostics.Where(diagnostic => diagnostic.IsWarningAsError || diagnostic.Severity == DiagnosticSeverity.Error);

                        List<InvalidOperationException> exceptions = new List<InvalidOperationException>();

                        foreach (Diagnostic diagnostic in failures)
                        {
                            exceptions.Add(ExceptionFactory.Create<InvalidOperationException>(Text.CompilationError_0__1_, diagnostic.Id, diagnostic.GetMessage()));
                        }

                        throw ExceptionFactory.AggregateException(exceptions);
                    }
                    else
                    {
                        ms.Seek(0, SeekOrigin.Begin);

                        Assembly assembly = AssemblyLoadContext.Default.LoadFromStream(ms);

                        return assembly;
                    }
                }
            }
            finally
            {
                AppDomain.CurrentDomain.AssemblyResolve -= CurrentDomain_AssemblyResolve;
            }
        }

        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            string assemblyName = args.Name.Split(',').First().Trim();
            string fileName = $"{assemblyName}.dll";
            string fullFilePath = Path.Combine(StartupPath, fileName);

            if (fullFilePath.EndsWith(".resources.dll"))
            {
                return null;
            }

            if (!File.Exists(fullFilePath))
            {
                throw ExceptionFactory.FileNotFoundException(fullFilePath);
            }

            Assembly assembly = Assembly.LoadFile(fullFilePath);

            if (assembly == null)
            {
                throw ExceptionFactory.InvalidOperationException(Text.FailedToLoad_0_Assembly, fullFilePath);
            }

            return assembly;
        }
    }
}
