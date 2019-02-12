``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-010184
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                Method |      Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------- |----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      ListContains_Int |  19.24 us | 0.0692 us | 0.0614 us |  1.00 |    0.00 |           - |           - |           - |                   - |
|    PooledContains_Int |  19.20 us | 0.0365 us | 0.0324 us |  1.00 |    0.00 |           - |           - |           - |                   - |
|   ListContains_String | 111.73 us | 1.2731 us | 1.1908 us |  5.81 |    0.06 |           - |           - |           - |                   - |
| PooledContains_String | 109.74 us | 0.7253 us | 0.6056 us |  5.70 |    0.03 |           - |           - |           - |                   - |
