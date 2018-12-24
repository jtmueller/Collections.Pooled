``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT
  Core   : .NET Core 2.1.6 (CoreCLR 4.6.27019.06, CoreFX 4.6.27019.05), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                Method |      Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------- |----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      ListContains_Int |  21.35 us | 0.2926 us | 0.2737 us |  1.00 |    0.00 |           - |           - |           - |                   - |
|    PooledContains_Int |  20.91 us | 0.1822 us | 0.1704 us |  0.98 |    0.01 |           - |           - |           - |                   - |
|   ListContains_String | 115.63 us | 0.4654 us | 0.4125 us |  5.42 |    0.08 |           - |           - |           - |                   - |
| PooledContains_String | 114.75 us | 0.6972 us | 0.6521 us |  5.38 |    0.07 |           - |           - |           - |                   - |
