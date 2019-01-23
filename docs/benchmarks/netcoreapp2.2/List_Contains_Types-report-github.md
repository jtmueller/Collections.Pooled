``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                Method |      Mean |     Error |    StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------------- |----------:|----------:|----------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|      ListContains_Int |  19.50 us | 0.1019 us | 0.0953 us |  1.00 |    0.00 |           - |           - |           - |                   - |
|    PooledContains_Int |  19.51 us | 0.0822 us | 0.0769 us |  1.00 |    0.01 |           - |           - |           - |                   - |
|   ListContains_String | 111.75 us | 1.1001 us | 1.0291 us |  5.73 |    0.06 |           - |           - |           - |                   - |
| PooledContains_String | 111.67 us | 1.1251 us | 1.0524 us |  5.73 |    0.06 |           - |           - |           - |                   - |
