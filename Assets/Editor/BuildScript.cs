using System.Linq;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace EVRC.BuildTools
{
    /// <summary>
    /// Command line build: Unity -batchmode -quit -projectPath . -executeMethod EVRC.BuildTools.BuildScript.BuildWindows64 [-buildPath Build/Elite VR Cockpit.exe]
    /// </summary>
    public static class BuildScript
    {
        public static void BuildWindows64()
        {
            var args = System.Environment.GetCommandLineArgs();
            var i = System.Array.IndexOf(args, "-buildPath");
            var path = i >= 0 && i + 1 < args.Length ? args[i + 1] : "Build/Elite VR Cockpit.exe";

            var options = new BuildPlayerOptions
            {
                scenes = EditorBuildSettings.scenes.Where(s => s.enabled).Select(s => s.path).ToArray(),
                locationPathName = path,
                target = BuildTarget.StandaloneWindows64,
                options = BuildOptions.None,
            };
            var report = BuildPipeline.BuildPlayer(options);
            Debug.Log($"Build {report.summary.result}: {path}, {report.summary.totalErrors} errors");
            if (report.summary.result != BuildResult.Succeeded)
                EditorApplication.Exit(1);
        }
    }
}
