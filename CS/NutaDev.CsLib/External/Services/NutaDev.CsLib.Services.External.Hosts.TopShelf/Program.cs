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

using NutaDev.CsLib.Services.Core.Models;
using NutaDev.CsLib.Services.Core.Services.Abstract;
using System;
using Topshelf;

namespace NutaDev.CsLib.Services.External.Hosts.TopShelf
{
    /// <summary>
    /// Entry point for <see cref="TopShelf"/> based services.
    /// </summary>
    public sealed class Program
    {
        /// <summary>
        /// Entry point for the service.
        /// </summary>
        public static void Main()
        {
            NutaDev.CsLib.Services.Hosts.Core.Factories.HostFactory factory = new Services.Hosts.Core.Factories.HostFactory();

            string[] args = factory.GetArguments();

            string serviceTypeName = factory.FindServiceTypeName(args);
            ServiceInitializationContext ctx = factory.CreateInitializationContext(factory.GetStartupPath(args), args, serviceTypeName);

            TopshelfExitCode exitCode = HostFactory.Run(x =>
            {
                x.Service<BaseService>(s =>
                {
                    s.ConstructUsing(name => Activator.CreateInstance(ctx.Type) as BaseService);
                    s.WhenStarted(tc =>
                    {
                        tc.Initialize(ctx);
                        tc.Start();
                    });
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription(ctx.Description);
                x.SetDisplayName(ctx.DisplayName);
                x.SetServiceName(ctx.ServiceName);
            });

            Environment.ExitCode = (int)Convert.ChangeType(exitCode, exitCode.GetTypeCode());
        }
    }
}
