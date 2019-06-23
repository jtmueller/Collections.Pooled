``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview6-012264
  [Host] : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview6-27804-01 (CoreCLR 4.700.19.30373, CoreFX 4.700.19.30308), 64bit RyuJIT

Jit=RyuJit  Platform=X64  InvocationCount=1  
UnrollFactor=1  

```
|    Method |  Job | Runtime |      Mean |     Error |   StdDev |    Median | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|---------- |----- |-------- |----------:|----------:|---------:|----------:|------:|--------:|------:|------:|------:|----------:|
|   HashSet |  Clr |     Clr | 450.13 us | 24.776 us | 70.28 us | 422.05 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledSet |  Clr |     Clr | 564.51 us | 29.310 us | 83.15 us | 525.00 us |  1.28 |    0.25 |     - |     - |     - |         - |
|           |      |         |           |           |          |           |       |         |       |       |       |           |
|   HashSet | Core |    Core |  65.82 us |  6.820 us | 19.35 us |  56.80 us |  1.00 |    0.00 |     - |     - |     - |         - |
| PooledSet | Core |    Core |  90.77 us |  9.703 us | 28.31 us |  83.75 us |  1.46 |    0.53 |     - |     - |     - |         - |
