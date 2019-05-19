using System;
using System.IO;
using System.Linq;

var buildPlots = new FileInfo(Path.Combine(Environment.CurrentDirectory, "Collections.Pooled.Benchmarks", "BuildPlots.R"));
var artifactDir = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, @"Collections.Pooled.Benchmarks\bin\Release\netcoreapp3.0\BenchmarkDotNet.Artifacts\results\"));
var outDir = Path.Combine(Environment.CurrentDirectory, @"docs\benchmarks");
var scriptFullPath = Path.Combine(artifactDir.FullName, "BuildPlots.R");

buildPlots.CopyTo(scriptFullPath, true);

var defaultRPath = @"D:\Program Files\R\R-3.6.0\bin\RScript.exe";
var rscriptPath = File.Exists(defaultRPath) ? defaultRPath : FindInPath("Rscript.exe");

Console.WriteLine("Executing BuildPlots.R...");

var start = new ProcessStartInfo 
{
    UseShellExecute = false,
    RedirectStandardOutput = true,
    RedirectStandardError = true,
    CreateNoWindow = true,
    FileName = rscriptPath,
    WorkingDirectory = artifactDir.FullName,
    Arguments = scriptFullPath
};
using (var process = Process.Start(start))
{
    string output = process?.StandardOutput.ReadToEnd() ?? "";
    string error = process?.StandardError.ReadToEnd() ?? "";
    Console.WriteLine(output + Environment.NewLine + error);
    process?.WaitForExit();
}

var pngs = artifactDir.EnumerateFiles("*.png");
var csvs = artifactDir.EnumerateFiles("*report.csv");
var htmls = artifactDir.EnumerateFiles("*.html");
var mds = artifactDir.EnumerateFiles("*.md");
var pdfs = artifactDir.EnumerateFiles("*.pdf");

var allFiles = Enumerable.Concat(pngs, csvs).Concat(htmls).Concat(mds).Concat(pdfs);

Console.Write("Copying Files");
foreach (var file in allFiles)
{
    Console.Write('.');
    var newName = file.Name.Replace("Collections.Pooled.Benchmarks.", "");
    file.CopyTo(Path.Combine(outDir, newName), true);
}
Console.WriteLine();

private static string FindInPath(string fileName)
{
    string path = Environment.GetEnvironmentVariable("PATH");
    if (path == null)
        return null;
    
    var dirs = path.Split(Path.PathSeparator);
    foreach (string dir in dirs)
    {
        string trimmedDir = dir.Trim('\'', '"');
        try
        {
            string filePath = Path.Combine(trimmedDir, fileName);
            if (File.Exists(filePath))
                return filePath;
        }
        catch (Exception)
        {
            // Never mind
        }
    }
    return null;
}
