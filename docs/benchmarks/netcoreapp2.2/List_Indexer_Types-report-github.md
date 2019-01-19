``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
.NET Core SDK=3.0.100-preview-009812
  [Host] : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT
  Core   : .NET Core 2.2.1 (CoreCLR 4.6.27207.03, CoreFX 4.6.27207.03), 64bit RyuJIT

Job=Core  Runtime=Core  

```
|                    Method |       Mean |     Error |    StdDev | Ratio | RatioSD |
|-------------------------- |-----------:|----------:|----------:|------:|--------:|
|           ListIndexer_Int |  17.541 us | 0.3471 us | 0.7834 us |  1.00 |    0.00 |
|         PooledIndexer_Int | 184.364 us | 3.5167 us | 3.2895 us | 10.78 |    0.45 |
|    PooledIndexer_Span_Int |   2.684 us | 0.0514 us | 0.0550 us |  0.16 |    0.01 |
|        ListIndexer_String |  33.403 us | 0.6604 us | 0.8817 us |  1.95 |    0.09 |
|      PooledIndexer_String | 186.989 us | 2.1449 us | 1.9014 us | 10.91 |    0.41 |
| PooledIndexer_Span_String |   5.338 us | 0.0828 us | 0.0775 us |  0.31 |    0.01 |
