``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|       Method |  Job | Runtime |       Mean |     Error |    StdDev | Ratio |       Gen 0 |       Gen 1 |       Gen 2 |    Allocated |
|------------- |----- |-------- |-----------:|----------:|----------:|------:|------------:|------------:|------------:|-------------:|
|   List_Range |  Clr |     Clr | 2,047.3 ms |  9.277 ms |  8.224 ms |  1.00 | 759000.0000 | 648000.0000 | 630000.0000 | 2913239200 B |
| Pooled_Range |  Clr |     Clr |   941.3 ms | 18.594 ms | 19.095 ms |  0.46 |           - |           - |           - |            - |
|              |      |         |            |           |           |       |             |             |             |              |
|   List_Range | Core |    Core | 2,164.1 ms | 30.288 ms | 28.331 ms |  1.00 | 827000.0000 | 715000.0000 | 699000.0000 | 2903343712 B |
| Pooled_Range | Core |    Core |   382.8 ms |  5.650 ms |  5.008 ms |  0.18 |           - |           - |           - |            - |
