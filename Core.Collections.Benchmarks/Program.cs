using BenchmarkDotNet.Running;

namespace Core.Collections.Benchmarks
{
    class Program
    {
        public static void Main(string[] args) =>
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}
