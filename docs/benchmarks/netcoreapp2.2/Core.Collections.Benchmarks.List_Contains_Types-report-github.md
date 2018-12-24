``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT
  Core   : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|---------------------- |----------:|----------:|----------:|------:|--------:|
|      ListContains_Int |  19.24 us | 0.0591 us | 0.0493 us |  1.00 |    0.00 |
|    PooledContains_Int |  19.20 us | 0.0585 us | 0.0518 us |  1.00 |    0.00 |
|   ListContains_String | 115.44 us | 0.2474 us | 0.2314 us |  6.00 |    0.02 |
| PooledContains_String | 116.24 us | 1.0352 us | 0.8644 us |  6.04 |    0.04 |
