using BenchmarkDotNet.Running;

namespace Collections.Pooled.Benchmarks
{
    class Program
    {
        public static void Main(string[] args) =>
            BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
    }
}
