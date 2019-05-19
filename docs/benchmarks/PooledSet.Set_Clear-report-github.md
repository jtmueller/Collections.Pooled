``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  InvocationCount=1  
UnrollFactor=1  

```
|    Method |  Job | Runtime | InitialSetSize |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------- |----- |-------- |--------------- |----------:|----------:|----------:|----------:|------:|--------:|------:|------:|------:|----------:|
|   HashSet |  Clr |     Clr |         320000 | 395.66 us | 13.608 us | 39.044 us | 383.90 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledSet |  Clr |     Clr |         320000 | 497.79 us | 17.067 us | 49.784 us | 481.65 us |  1.27 |    0.16 |     - |     - |     - |         - |
|           |      |         |                |           |           |           |           |       |         |       |       |       |           |
|   HashSet | Core |    Core |         320000 |  55.76 us |  1.140 us |  3.140 us |  54.80 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledSet | Core |    Core |         320000 |  84.29 us |  7.427 us | 21.783 us |  71.60 us |  1.54 |    0.39 |     - |     - |     - |         - |
