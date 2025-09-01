using System;
using System.IO;
using System.Linq;
using Nuke.Common;
using Nuke.Common.CI;
using Nuke.Common.Execution;
using Nuke.Common.Git;
using Nuke.Common.IO;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Nuke.Common.Utilities.Collections;
using static Nuke.Common.EnvironmentInfo;
using static Nuke.Common.IO.PathConstruction;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

[ShutdownDotNetAfterServerBuild]
class Build : NukeBuild
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main () => Execute<Build>(x => x.Compile);

    [Parameter("Configuration to build - Default is 'Debug' (local) or 'Release' (server)")]
    readonly Configuration Configuration = IsLocalBuild ? Configuration.Debug : Configuration.Release;

    [Solution] readonly Solution Solution;
    [GitRepository] readonly GitRepository GitRepository;

    AbsolutePath SourceDirectory => RootDirectory / "src";

    AbsolutePath ArtifactsDirectory => RootDirectory / "Artifacts";
    
    AbsolutePath TestsDirectory => RootDirectory / "Tests";

    AbsolutePath DeployPath => (AbsolutePath)"C:" / "Projects" / "NuGet Store";

    [Parameter("Version to be injected in the Build")]
    public string Version { get; set; } = $"1.4.2";

    [Parameter("The Buildnumber provided by the CI")]
    public int BuildNo = 1;

    [Parameter("Is RC Version")]
    public bool IsRc = false;
    
    Target Clean => _ => _
        .Before(Restore)
        .Executes(() =>
        {
            SourceDirectory.GlobDirectories("**/bin", "**/obj").ForEach(f => f.DeleteDirectory());
            TestsDirectory.GlobDirectories("**/bin", "**/obj").ForEach(f => f.DeleteDirectory());
            ArtifactsDirectory.CreateOrCleanDirectory();
        });

    Target Restore => _ => _
        .Executes(() =>
        {
            DotNetRestore(s => s
                .SetProjectFile(Solution));
        });

    Target Compile => _ => _
        .DependsOn(Restore)
        .Executes(() =>
        {
            DotNetBuild(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetVersion($"{Version}.{BuildNo}")
                .SetAssemblyVersion($"{Version}.{BuildNo}")
                .SetFileVersion(Version)
                .SetInformationalVersion($"{Version}.{BuildNo}")
                .AddProperty("PackageVersion", PackageVersion)
                .EnableNoRestore());
        });

    Target Test => _ => _
        .DependsOn(Compile)
        .Executes(() =>
        {
            DotNetTest(s => s
                .SetProjectFile(Solution)
                .SetConfiguration(Configuration)
                .SetNoBuild(true)
                //.SetFilter("FullyQualifiedName!~Integration.Tests")
                .EnableNoRestore());
        });

    Target FullBuild => _ => _
        .DependsOn(Compile)
        .DependsOn(Test);
    
    Target Deploy => _ => _
        .DependsOn(FullBuild)
        .Executes(() =>
        {
            // copy to artifacts folder
            foreach (var file in Directory.GetFiles(RootDirectory, $"*.{PackageVersion}.nupkg", SearchOption.AllDirectories))
            {
                ((AbsolutePath)file).Copy(ArtifactsDirectory / Path.GetFileName(file), ExistsPolicy.FileOverwrite);
            }

            foreach (var file in Directory.GetFiles(RootDirectory, $"*.{PackageVersion}.snupkg", SearchOption.AllDirectories))
            {
                ((AbsolutePath)file).Copy(ArtifactsDirectory / Path.GetFileName(file), ExistsPolicy.FileOverwrite);
            }

            // copy to local store
            foreach (var file in Directory.GetFiles(ArtifactsDirectory, $"*.{PackageVersion}.nupkg", SearchOption.AllDirectories))
            {
                ((AbsolutePath)file).Copy(DeployPath / Path.GetFileName(file), ExistsPolicy.FileOverwrite);
            }

            foreach (var file in Directory.GetFiles(ArtifactsDirectory, $"*.{PackageVersion}.snupkg", SearchOption.AllDirectories))
            {
                ((AbsolutePath)file).Copy(DeployPath / Path.GetFileName(file), ExistsPolicy.FileOverwrite);
            }
        });

    string PackageVersion
        => IsRc ? BuildNo < 10 ? $"{Version}-RC0{BuildNo}" : $"{Version}-RC{BuildNo}" : Version;
}
