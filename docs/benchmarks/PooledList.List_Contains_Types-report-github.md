``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|        Method |  Job | Runtime |      Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|-------------- |----- |-------- |----------:|----------:|----------:|------:|--------:|------:|------:|------:|----------:|
|      List_Int |  Clr |     Clr |  59.36 us | 1.0888 us | 0.9652 us |  1.00 |    0.00 |     - |     - |     - |         - |
|    Pooled_Int |  Clr |     Clr |  19.81 us | 0.2088 us | 0.1630 us |  0.33 |    0.01 |     - |     - |     - |         - |
|   List_String |  Clr |     Clr | 173.76 us | 1.4622 us | 1.3678 us |  2.93 |    0.06 |     - |     - |     - |         - |
| Pooled_String |  Clr |     Clr | 122.99 us | 1.4637 us | 1.3692 us |  2.07 |    0.04 |     - |     - |     - |         - |
|               |      |         |           |           |           |       |         |       |       |       |           |
|      List_Int | Core |    Core |  20.29 us | 0.3159 us | 0.2955 us |  1.00 |    0.00 |     - |     - |     - |         - |
|    Pooled_Int | Core |    Core |  20.39 us | 0.1671 us | 0.1563 us |  1.00 |    0.01 |     - |     - |     - |         - |
|   List_String | Core |    Core | 112.48 us | 0.4657 us | 0.4128 us |  5.55 |    0.08 |     - |     - |     - |         - |
| Pooled_String | Core |    Core | 123.33 us | 1.7478 us | 1.6349 us |  6.08 |    0.12 |     - |     - |     - |         - |
