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
using Nuke.Common.Tools.MSBuild;
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

        return Execute<Build>(x => x.Finish);
    }

    private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
    {
        return null;
    }

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    public readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Parameter("Nuget api key")]
    public readonly string NugetApiKey = "";

    [Solution("CS/NutaDev.CsLib/NutaDev.CsLib.sln", Name = "NutaDev.CsLib")]
    public readonly Solution CsSolution;

    [Solution("CPP/NutaDev.CppLib/NutaDev.CppLib.sln", Name = "NutaDev.CppLib")]
    public readonly Solution CppSolution;

    private Project[] ToBuild;

    Target Clean => _ => _
        .Executes(() =>
        {
            CleanDeploy();
        });

    Target Prepare => _ => _
        .DependsOn(Clean)
        .Executes(() =>
        {
            ToBuild = CsSolution.AllProjects.Where(x => !x.Directory.ToString().Contains("\\Internal\\")).ToArray();
            EnsureGeneratePackageOnBuild();
            EnsureLicense();
        });

    Target Restore => _ => _
        .DependsOn(Prepare)
        .Executes(() =>
        {
            RestoreProjects();
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            CompileProjects();
            GenerateNuggetPackages();
        });

    Target DeployLocal => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            MoveNuggetPackages();
        });

    Target DeployWeb => _ => _
        .DependsOn(DeployLocal)
        .Executes(() =>
        {
            DeployNuggetPackages();
        });

    Target Finish => _ => _
        .DependsOn(DeployWeb)
        .Executes(() =>
        {
            // Ignored
        });

    private void CleanDeploy()
    {
        Paths paths = GetPaths();

        if (Directory.Exists(paths.DeployDirecotry))
        {
            Directory.Delete(paths.DeployDirecotry, true);
        }
    }

    private void MoveNuggetPackages()
    {
        Paths paths = GetPaths();

        if (!Directory.Exists(paths.DeployDirecotry))
        {
            Directory.CreateDirectory(paths.DeployDirecotry);
        }

        foreach (string nuPkgFilePath in Directory.EnumerateFiles(paths.CsDirectory, "*.nupkg", SearchOption.AllDirectories).Where(x => x.Contains($"\\{Configuration}\\")))
        {
            string targetFilePath = Path.Combine(paths.DeployDirecotry, Path.GetFileName(nuPkgFilePath));

            File.Copy(nuPkgFilePath, targetFilePath);
        }
    }

    private void RestoreProjects()
    {
        MSBuildTasks.MSBuild(s => s
            .SetTargetPath(CsSolution)
            .SetConfiguration(Configuration)
            .SetTargets("Restore")
            .SetMaxCpuCount(Environment.ProcessorCount)
            .SetNodeReuse(IsLocalBuild)
        );
    }

    private void CompileProjects()
    {
        MSBuildTasks.MSBuild(s => s
            .SetTargetPath(CsSolution)
            .SetConfiguration(Configuration)
            .SetTargets("Rebuild")
            .SetMaxCpuCount(Environment.ProcessorCount)
            .SetNodeReuse(IsLocalBuild)
        );
    }

    private void DeployNuggetPackages()
    {
        Paths paths = GetPaths();
        new NugetWrapper().Push(NugetApiKey, paths.RootDirectory, paths.DeployDirecotry);
    }

    private void GenerateNuggetPackages()
    {
        foreach (Project prj in ToBuild.Where(x => x.GetTargetFrameworks() == null))
        {
            new NugetWrapper().Generate(prj, Configuration);
        }
    }

    private void EnsureLicense()
    {
        const string LicensePathLine = @"<None Include=""pathToLicense/LICENSE"" Pack=""true"" Visible=""false"" PackagePath="""" />";
        const string LicenseProperties = @"
            <ItemGroup>
                "+LicensePathLine+@"
            </ItemGroup>
        ";

        const string PropertyGroupLicenseLine = @"<PackageLicenseFile>LICENSE</PackageLicenseFile>";
        const string PropertyGroup = @"
            <PropertyGroup>
                "+PropertyGroupLicenseLine+@"
            </PropertyGroup>
        ";

        Paths paths = GetPaths();

        foreach (Project prj in ToBuild)
        {
            string pathToLicense = GetRelativePath(paths.RootDirectory, prj.Path);

            List<string> fileLines = File.ReadAllLines(prj.Path).ToList();

            if (fileLines.All(x => !x.Contains("PackageLicenseFile")))
            {
                int propertyGroupIdx = fileLines.Select(x => x.Trim()).ToList().IndexOf("</PropertyGroup>");

                string license = LicenseProperties.Replace(nameof(pathToLicense), pathToLicense);
                fileLines.Insert(fileLines.LastIndexOf(fileLines.FindLast(x => x.Contains("</Project>"))), license);

                if (propertyGroupIdx < 0)
                {
                    fileLines.Insert(fileLines.LastIndexOf(fileLines.FindLast(x => x.Contains("</Project>"))), PropertyGroup);
                }
                else
                {
                    int startIdx = 0;

                    while ((propertyGroupIdx = fileLines.Select(x => x.Trim()).ToList().IndexOf("</PropertyGroup>", startIdx)) > 0)
                    {
                        fileLines.Insert(propertyGroupIdx, PropertyGroupLicenseLine);
                        startIdx = propertyGroupIdx + 2;
                    }
                }

                File.WriteAllLines(prj.Path, fileLines.ToArray());
            }
            else
            {
                string license = LicensePathLine.Replace(nameof(pathToLicense), pathToLicense);
                string line = fileLines.Find(x => x.EndsWith(@"LICENSE"" Pack=""true"" Visible=""false"" PackagePath="""" />"));

                if (!line.Contains(license))
                {
                    int idx = fileLines.IndexOf(line);
                    fileLines[idx] = license;

                    File.WriteAllLines(prj.Path, fileLines.ToArray());
                }
            }
        }
    }

    private void EnsureGeneratePackageOnBuild()
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
    }


    private Paths GetPaths()
    {
        string rootDirectory = Path.GetFullPath($"{CsSolution.Directory}/../..");
        string deployDirectory = Path.Combine(rootDirectory, Paths.RelativeArtifactsDirectory);

        return new Paths
        {
            RootDirectory = rootDirectory,
            DeployDirecotry = deployDirectory,
            CsDirectory = Path.Combine(rootDirectory, "CS")
        };
    }

    private string GetRelativePath(string toPath, string fromPath)
    {
        string remainingPath = fromPath.Replace(toPath, string.Empty);
        return string.Join("/", remainingPath.Split('\\').Skip(2).Select(x => ".."));
    }

    private class Paths
    {
        public const string RelativeArtifactsDirectory = "local/deploy";
        public string RootDirectory;
        public string DeployDirecotry;
        public string CsDirectory;
    }
}
