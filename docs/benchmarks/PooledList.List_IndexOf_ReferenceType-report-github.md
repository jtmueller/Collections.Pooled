``` ini

BenchmarkDotNet=v0.11.5, OS=Windows 10.0.18362
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview5-011568
  [Host] : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.8.3801.0
  Core   : .NET Core 3.0.0-preview5-27626-15 (CoreCLR 4.6.27622.75, CoreFX 4.700.19.22408), 64bit RyuJIT

Jit=RyuJit  Platform=X64  

```
|         Method |  Job | Runtime |      Mean |     Error |    StdDev | Ratio | Gen 0 | Gen 1 | Gen 2 | Allocated |
|--------------- |----- |-------- |----------:|----------:|----------:|------:|------:|------:|------:|----------:|
|   List_IndexOf |  Clr |     Clr |  95.55 us | 0.9014 us | 0.8431 us |  1.00 |     - |     - |     - |         - |
| Pooled_IndexOf |  Clr |     Clr |  93.80 us | 1.2457 us | 1.1652 us |  0.98 |     - |     - |     - |         - |
|                |      |         |           |           |           |       |       |       |       |           |
|   List_IndexOf | Core |    Core | 111.76 us | 0.7268 us | 0.6798 us |  1.00 |     - |     - |     - |         - |
| Pooled_IndexOf | Core |    Core | 111.58 us | 0.8452 us | 0.7493 us |  1.00 |     - |     - |     - |         - |
