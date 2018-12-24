``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT
  Core   : .NET Core 2.2.0 (CoreCLR 4.6.27110.04, CoreFX 4.6.27110.04), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|               Method |      Mean |     Error |    StdDev | Ratio | RatioSD |
|--------------------- |----------:|----------:|----------:|------:|--------:|
|      ListIndexer_Int |  16.77 us | 0.0633 us | 0.0528 us |  1.00 |    0.00 |
|    PooledIndexer_Int | 157.49 us | 0.3858 us | 0.3609 us |  9.39 |    0.03 |
|   ListIndexer_String |  31.22 us | 0.0539 us | 0.0450 us |  1.86 |    0.01 |
| PooledIndexer_String | 157.65 us | 0.4083 us | 0.3819 us |  9.40 |    0.03 |
