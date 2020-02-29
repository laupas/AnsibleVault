using System;
using static Bullseye.Targets;
using static SimpleExec.Command;

namespace Build
{
    static class Program
    {
        static void Main(string[] args)
        {
            Target("default", DependsOn(new string[]
            {
                "init",
                "restore",
                "build", 
                "test", 
                "pack"
            }));

            string buildNumber = string.Empty;
            string nugetOrgToken = string.Empty;

            Target("init", () =>
            {
                var versionPrefix = Environment.GetEnvironmentVariable("VERSION_PREFIX");
                var runNumber = Environment.GetEnvironmentVariable("GITHUB_RUN_NUMBER");
                nugetOrgToken = Environment.GetEnvironmentVariable("NUGET_ORG_API_KEY");
                if (string.IsNullOrEmpty(runNumber) || string.IsNullOrEmpty(versionPrefix))
                {
                    buildNumber = $"0.0.0-local";
                }
                else
                {
                    buildNumber = $"{versionPrefix}.{runNumber}";
                }

                Console.WriteLine($"BuildNumber: {buildNumber}");
            });

            Target("restore", DependsOn("init"), () =>
            {
                Run("dotnet", $"restore", workingDirectory: "./..");
            });

            Target("build", DependsOn("init"), () =>
            {
                Run("dotnet", $"build -c Release -p:Version={buildNumber}", "./..");
            });

            Target("test", DependsOn("init"), () =>
            {
                Run("dotnet", $"test -c Release --no-build", "./..");
            });

            Target("pack", DependsOn("init"), () =>
            {
                Run("dotnet", $"pack ./AnsibleVault/AnsibleVault.csproj --no-build -c Release -p:Version={buildNumber}", "./..");
                Run("dotnet", $"nuget push ./AnsibleVault/bin/Release/*.nupkg --no-symbols true -s https://api.nuget.org/v3/index.json -k {nugetOrgToken}", "./..");
            });
            
            RunTargetsAndExit(args);
        }
    }
}

