using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.FileSystemTasks;
using static Nuke.Common.IO.PathConstruction;

public class Build
    : NukeBuild
{
    public static int Main()
    {
        AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

        return Execute<Build>(x => x.Compile);
    }

    private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
    {
        return null;
    }

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    public readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution("CS/NutaDev.CsLib/NutaDev.CsLib.sln", Name = "NutaDev.CsLib")]
    public readonly Solution CsSolution;

    [Solution("CPP/NutaDev.CppLib/NutaDev.CppLib.sln", Name = "NutaDev.CppLib")]
    public readonly Solution CppSolution;

    private Project[] ToBuild;

    Target Clean => _ => _
        .Executes(() =>
        {
        });

    Target Prepare => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            ToBuild = CsSolution.AllProjects.Where(x => !x.Directory.Contains("\\Internal\\")).ToArray();
        });

    Target Restore => _ => _
        .DependsOn(Prepare)
        .Executes(() =>
        {
        });

    Target EnsureGeneratePackageOnBuild => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            foreach (Project prj in ToBuild)
            {
                if (string.IsNullOrWhiteSpace(prj.GetProperty("Deterministic")))
                {
                    throw new InvalidOperationException($"Every project must have `Deterministic` defined. Project: `{prj.Name}`.");
                }

                List<string> fileLines = File.ReadAllLines(prj.Path).ToList();

                if (fileLines.Select(x => x.Trim()).Contains("<GeneratePackageOnBuild>false</GeneratePackageOnBuild>"))
                {
                    int packageIdx = fileLines.Select(x => x.Trim()).ToList().IndexOf("<GeneratePackageOnBuild>false</GeneratePackageOnBuild>");

                    if (packageIdx < 0)
                    {
                        throw new InvalidOperationException($"Failed to find `GeneratePackageOnBuild`. Project: `{prj.Name}`.");
                    }

                    fileLines[packageIdx] = "<GeneratePackageOnBuild>true</GeneratePackageOnBuild>";

                    File.WriteAllLines(prj.Path, fileLines.ToArray());
                }
                else if (!fileLines.Select(x => x.Trim()).Contains("<GeneratePackageOnBuild>true</GeneratePackageOnBuild>"))
                {
                    int deterministicIdx = fileLines.Select(x => x.Trim()).ToList().IndexOf("<Deterministic>true</Deterministic>");
                    if (deterministicIdx < 0)
                    {
                        deterministicIdx = fileLines.Select(x => x.Trim()).ToList().IndexOf("<Deterministic>false</Deterministic>");
                    }

                    if (deterministicIdx < 0)
                    {
                        throw new InvalidOperationException($"Failed to find `Deterministic`. Project: `{prj.Name}`.");
                    }

                    fileLines.Insert(deterministicIdx, "<GeneratePackageOnBuild>true</GeneratePackageOnBuild>");

                    File.WriteAllLines(prj.Path, fileLines.ToArray());
                }
            }
        });

    Target Compile => _ => _
        .DependsOn(EnsureGeneratePackageOnBuild)
        .Executes(() =>
        {
            Console.WriteLine(string.Join("\r\n", CsSolution.AllProjects.Select(x => x.Name)));
            Console.ReadKey();
        });

}
