using BenchmarkDotNet.Running;

/* Powershell commands to shorten the benchmark output filenames:
Get-ChildItem | Rename-Item -NewName {$_.name -replace 'Collections.Pooled.Benchmarks.PooledList.','' }
Get-ChildItem | Rename-Item -NewName {$_.name -replace 'Collections.Pooled.Benchmarks.PooledDictionary.','' }
Get-ChildItem | Rename-Item -NewName {$_.name -replace 'Collections.Pooled.Benchmarks.PooledStack.','' }
Get-ChildItem | Rename-Item -NewName {$_.name -replace 'Collections.Pooled.Benchmarks.PooledSet.','' }
Get-ChildItem | Rename-Item -NewName {$_.name -replace 'Collections.Pooled.Benchmarks.PooledQueue.','' }
*/

namespace Collections.Pooled.Benchmarks
{
    class Program
    {
        public static void Main(string[] args) =>
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}
