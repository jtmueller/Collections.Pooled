``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|             Method |  Job | Runtime |      Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0 | Gen 1 | Gen 2 | Allocated |
|------------------- |----- |-------- |----------:|----------:|----------:|------:|--------:|------:|------:|------:|----------:|
|           List_Int |  Clr |     Clr | 69.309 us | 1.2558 us | 1.1747 us |  1.00 |    0.00 |     - |     - |     - |         - |
|         Pooled_Int |  Clr |     Clr | 17.031 us | 0.2631 us | 0.2461 us |  0.25 |    0.01 |     - |     - |     - |         - |
|    Pooled_Span_Int |  Clr |     Clr | 13.707 us | 0.2436 us | 0.2278 us |  0.20 |    0.01 |     - |     - |     - |         - |
|        List_String |  Clr |     Clr | 51.492 us | 0.8001 us | 0.7484 us |  0.74 |    0.02 |     - |     - |     - |         - |
|      Pooled_String |  Clr |     Clr | 30.525 us | 0.4586 us | 0.4290 us |  0.44 |    0.01 |     - |     - |     - |         - |
| Pooled_Span_String |  Clr |     Clr | 13.750 us | 0.2232 us | 0.2088 us |  0.20 |    0.01 |     - |     - |     - |         - |
|                    |      |         |           |           |           |       |         |       |       |       |           |
|           List_Int | Core |    Core | 16.203 us | 0.3226 us | 0.3312 us |  1.00 |    0.00 |     - |     - |     - |         - |
|         Pooled_Int | Core |    Core | 19.927 us | 0.3460 us | 0.3237 us |  1.23 |    0.03 |     - |     - |     - |         - |
|    Pooled_Span_Int | Core |    Core |  4.962 us | 0.0826 us | 0.0732 us |  0.31 |    0.01 |     - |     - |     - |         - |
|        List_String | Core |    Core | 29.939 us | 0.5740 us | 0.5369 us |  1.85 |    0.04 |     - |     - |     - |         - |
|      Pooled_String | Core |    Core | 29.764 us | 0.5530 us | 0.5172 us |  1.83 |    0.05 |     - |     - |     - |         - |
| Pooled_Span_String | Core |    Core |  2.542 us | 0.0494 us | 0.0529 us |  0.16 |    0.01 |     - |     - |     - |         - |
