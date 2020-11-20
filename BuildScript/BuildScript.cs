using System;
using System.Diagnostics;
using FlubuCore.Context;
using FlubuCore.Scripting;
using FlubuCore.Tasks.Attributes;
using FlubuCore.Tasks.Versioning;

public class BuildScript : DefaultBuildScript
{
	//// Target fetches build version from FlubuExample.ProjectVersion.txt build version is stored. You can also explicitly set file name in attribute.
	//// Alternatively flubu supports fetching of build version out of the box with GitVersionTask. Just apply [GitVersion] attribute on property.
	//[FetchBuildVersionFromFile(AllowSuffix = true)]
	//public BuildVersion BuildVersion { get; set; }

	[FromArg("version", "Compile Version")]
	public string Version { get; set; } = "1.0.0";

	protected override void ConfigureBuildProperties(IBuildPropertiesContext context)
	{
		//context.Properties.Set(BuildProps.NUnitConsolePath, @"packages\NUnit.ConsoleRunner.3.6.0\tools\nunit3-console.exe");
		context.Properties.Set(BuildProps.ProductId, "WickedFlame.Yaml");
		context.Properties.Set(DotNetBuildProps.ProductName, "WickedFlame.Yaml");
		context.Properties.Set(BuildProps.SolutionFileName, "..\\WickedFlame.Yaml.sln");
		context.Properties.Set(BuildProps.BuildConfiguration, "Release");
	}

	protected override void ConfigureTargets(ITaskContext context)
	{
		//var clean = context.CreateTarget("Clean")
		//	.SetDescription("Clean's the solution.")
		//	.AddCoreTask(x => x.Clean()
		//		.AddDirectoryToClean(OutputDir, true));

		//var compile = context
		//	.CreateTarget("compile")
		//	.SetDescription("Compiles the VS solution and sets version to FlubuExample.csproj")
		//	.DependsOn(clean)
		//	.AddCoreTask(x => x.Restore())
		//	.AddCoreTask(x => x.Build()
		//		.Version(BuildVersion.Version.ToString()));

		//var version = context.CreateTarget("version")
		//	.AddTask(x => x.FetchVersionFromExternalSourceTask());

		var clean = context.CreateTarget("Clean")
			.SetDescription("Clean's the solution.")
			.AddCoreTask(x => x.Clean());

		var compile = context
			.CreateTarget("compile")
			.SetDescription("Compiles the VS solution and sets version")
			.DependsOn(clean)
			//.DependsOn(version)
			.AddCoreTask(x => x.Restore())
			.AddCoreTask(x => x.Build());
				//.Version(Version)
				//.WithArguments($"-p:PackageVersion={Version}", $"-p:AssemblyVersion={Version}", $"-p:FileVersion={Version}"));

		var test = context.CreateTarget("test")
			.AddCoreTaskAsync(x => x.Test().Project(@"..\src\WickedFlame.Yaml.Tests"));

		//var vsSolution = context.GetVsSolution();
		//var testProjects = vsSolution.FilterProjects("*.Tests");

		//var testAlternative = context.CreateTarget("test")
		//	.ForEach(testProjects,
		//		(project, target) =>
		//		{
		//			target.AddCoreTask(x => x.Test().Project("..\\src\\"+project.ProjectName));
		//		});

		context.CreateTarget("Rebuild")
			.SetAsDefault()
			.DependsOn(compile, test);
	}
}
