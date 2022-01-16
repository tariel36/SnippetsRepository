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

using NutaDev.CsLib.Internal.ConsoleTools.Tools;

namespace NutaDev.CsLib.Internal.ConsoleTools
{
    /// <summary>
    /// Entry class for this console application.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// Entry method for this console application.
        /// </summary>
        /// <param name="args">Run arguments.</param>
        public static void Main(string[] args)
        {
            const string author = "tariel36";
            const string rootPath = @"";

            // CS
            //int[] tasks = new int[] { 1, 2, 0, 5, 3 };
            //int[] tasks = new int[] { 4 };
            //int[] tasks = new int[] { 5 };
            //int[] tasks = new int[] { 0 };
            //int[] tasks = new int[] { 6 };
            //int[] tasks = new int[] { 8 };

            // CPP
            int[] tasks = new int[] { 0, 5, 6, 7, 3 };

            foreach (int task in tasks)
            {
                switch (task)
                {
                    case 0:
                    {
                        new Licensor(rootPath, author).Execute();
                        break;
                    }
                    case 1:
                    {
                        new AssemblyInfoLinkerStandard(rootPath, author).Execute();
                        break;
                    }
                    case 2:
                    {
                        new AssemblyInfoLinkerFramework(rootPath, author).Execute();
                        break;
                    }
                    case 3:
                    {
                        new EofAppender(rootPath).Execute();
                        break;
                    }
                    case 4:
                    {
                        new CommentsFinder(rootPath).Execute();
                        break;
                    }
                    case 5:
                    {
                        new RemoveDoubleEmptyLines(rootPath).Execute();
                        break;
                    }
                    case 6:
                    {
                        new TabsToSpaces(rootPath).Execute();
                        break;
                    }
                    case 7:
                    {
                        new NamespaceFix(rootPath).Execute();
                        break;
                    }
                    case 8:
                    {
                        new CodeToSingleFile(rootPath).Execute();
                        break;
                    }
                } 
            }
        }
    }
}
