using Nuke.Common;
using Nuke.Common.ProjectModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

public sealed class NugetWrapper
{
    public void Generate(Project project, string config)
    {
        GenerateNuSpec(project, config);
        Pack(project, config);
    }

    public void Push(string apiKey, string root, string deployDir)
    {
        ExecuteNuget($"setapikey {apiKey}", root);
        ExecuteNuget($"push *.nupkg -Source https://api.nuget.org/v3/index.json", deployDir);
    }

    private void GenerateNuSpec(Project project, string config)
    {
        string[] ignoreTags = new[]
        {
            "<releaseNotes>",
            "<tags>"
        };

        string projectDir = Path.GetDirectoryName(project.Path);
        string sharedProjectPath = Path.GetDirectoryName(project.Solution.AllProjects.First(x => string.Equals(x.Name, "NutaDev.CsLib.Internal.Shared")).Path);

        string sharedPath = Path.Combine(sharedProjectPath, "App", "SharedAssemblyInfo.cs");
        string localPath = Path.Combine(projectDir, "Properties", "AssemblyInfo.cs");

        ProjectMetadata metadata = LoadMetadataFromAssemblyCs(project, config, sharedPath, localPath);

        ExecuteNuget($"spec {project.Name}.csproj", projectDir);

        string nuspecPath = Path.Combine(projectDir, $"{project.Name}.nuspec");
        string[] fileLines = File.ReadAllLines(nuspecPath);

        List<string> finalLines = new List<string>();

        for (int i = 0; i < fileLines.Length; ++i)
        {
            string line = fileLines[i];

            if (!ignoreTags.Any(x => line.Contains(x)))
            {
                foreach (string tag in new [] { "id", "version", "title", "author", "description", "copyright" })
                {
                    if (ReplaceTag(metadata, ref line, tag))
                    {
                        break;
                    }
                }

                if (line.Contains("projectUrl"))
                {
                    line = $"<projectUrl>{metadata.ProjectUrl}</projectUrl>";
                }

                if (line.Contains("version"))
                {
                    line = line.Replace(".*", string.Empty);
                }

                finalLines.Add(line);
            }
        }

        List<string> dependencies = new List<string>();
        dependencies.Add("<dependencies>");

        Regex depRegex = new Regex("Include=\"(((?<nameP>.*?), Version=(?<versionP>.*?),)|(.*\\\\(?<nameR>.*)\\.))");

        var refs = File.ReadAllLines(project.Path)
            .Where(x => x.Contains("<ProjectReference") || (x.Contains("Reference") && x.Contains("Version")))
            .Select(x =>
            {
                Match match = depRegex.Match(x);
                string pName = SelectNonEmpty(match.Groups["nameP"].Value, match.Groups["nameR"].Value);
                string pVer = SelectNonEmpty(match.Groups["versionP"].Value, metadata.Version);

                string val = $"<dependency id=\"{pName}\" version=\"{pVer}\" />";

                return val;
            })
            ;

        dependencies.AddRange(refs);

        dependencies.Add("</dependencies>");

        if (dependencies.Count > 2)
        {
            foreach (string dep in dependencies)
            {
                finalLines.Insert(finalLines.Count - 2, dep);
            }
        }

        File.WriteAllLines(nuspecPath, finalLines);
    }

    private string SelectNonEmpty(string left, string right)
    {
        return string.IsNullOrWhiteSpace(left) ? right : left;
    }

    private void Pack(Project project, string config)
    {
        string baseDir = Path.GetDirectoryName(project.Path);

        ExecuteNuget($"pack -OutputDirectory bin/{config} -Properties Configuration={config}", baseDir);
    }

    private ProjectMetadata LoadMetadataFromAssemblyCs(Project prj, string config, string sharedPath, string localPath)
    {
        Regex assemblyRegex = new Regex(@"\[assembly: (Assembly(?<name>[a-zA-Z]+))\(""(?<value>.*?)""\)\]");

        ProjectMetadata result = new ProjectMetadata();

        void SetData(string path, ProjectMetadata data)
        {
            foreach (string line in File.ReadAllLines(path))
            {
                Match match = assemblyRegex.Match(line);

                if (match.Success)
                {
                    string name = match.Groups["name"].Value;
                    string value = match.Groups["value"].Value;

                    FieldInfo field = data.GetType().GetFields().FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.InvariantCultureIgnoreCase));

                    if (field != null)
                    {
                        field.SetValue(data, value);
                    }
                }
            }
        }

        SetData(sharedPath, result);
        SetData(localPath, result);

        result.Copyright = result.Author;
        result.Id = result.Title;

        if (string.IsNullOrWhiteSpace(result.Description))
        {
            result.Description = result.Title;
        }

        string assemblyPath = Path.Combine(Path.GetDirectoryName(prj.Path), "bin", config, $"{prj.Name}.dll");
        AssemblyName assemblyName = AssemblyName.GetAssemblyName(assemblyPath);
        result.Version = string.Join(".", assemblyName.Version.ToString().Split('.').Take(2)) + ".0";
        result.FileVersion = result.Version;

        return result;
    }

    private bool ReplaceTag(ProjectMetadata metadata, ref string line, string tag)
    {
        FieldInfo field = metadata.GetType().GetFields().First(x => string.Equals(x.Name, tag, StringComparison.InvariantCultureIgnoreCase));
        
        if (field == null || !line.Contains(tag))
        {
            return false;
        }

        string value = field.GetValue(metadata)?.ToString() ?? "";

        line = line.Replace($"${tag}$", value);

        return true;
    }

    private void ExecuteNuget(string args, string baseDir)
    {
        void LoggerHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            string line = outLine?.Data ?? "";

            Serilog.Log.Write(Serilog.Events.LogEventLevel.Information, line);

            if (line.StartsWith("ERROR"))
            {
                throw new InvalidOperationException($"Failed to execute nugget in `{baseDir}`.");
            }
        };

        Process process = new Process();
        process.StartInfo.FileName = @"F:\_DEV\_libs\runnable\nuget.exe";
        process.StartInfo.Arguments = $"{args} -Force";
        process.StartInfo.WorkingDirectory = baseDir;
        process.StartInfo.UseShellExecute = false;
        process.StartInfo.RedirectStandardInput = true;
        process.StartInfo.RedirectStandardOutput = true;
        process.StartInfo.RedirectStandardError = true;
        process.OutputDataReceived += LoggerHandler;
        process.ErrorDataReceived += LoggerHandler;

        process.Start();

        process.BeginOutputReadLine();
        process.BeginErrorReadLine();

        process.WaitForExit();
    }

    private class ProjectMetadata
    {
        public string ProjectUrl = "https://github.com/tariel36/SnippetsRepository";
        public string Author = "tariel36";
        public string Id;
        public string Product;
        public string Configuration;
        public string Description;
        public string Company;
        public string Copyright;
        public string Trademark;
        public string Culture;
        public string Version;
        public string FileVersion;
        public string Title;
    }
}