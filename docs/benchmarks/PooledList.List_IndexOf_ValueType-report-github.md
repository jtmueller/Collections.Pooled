``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|         Method |  Job | Runtime |     Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------- |----- |-------- |---------:|----------:|----------:|------:|--------:|------:|------:|------:|----------:|
|   List_IndexOf |  Clr |     Clr | 12.63 us | 0.2464 us | 0.2304 us |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_IndexOf |  Clr |     Clr | 12.67 us | 0.1994 us | 0.1865 us |  1.00 |    0.02 |     - |     - |     - |         - |
|                |      |         |          |           |           |       |         |       |       |       |           |
|   List_IndexOf | Core |    Core | 12.62 us | 0.2282 us | 0.2135 us |  1.00 |    0.00 |     - |     - |     - |         - |
| Pooled_IndexOf | Core |    Core | 12.65 us | 0.2509 us | 0.2464 us |  1.00 |    0.03 |     - |     - |     - |         - |
