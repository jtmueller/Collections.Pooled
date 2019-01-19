``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                    Method |      Mean |     Error |    StdDev |    Median | Ratio | RatioSD |
|-------------------------- |----------:|----------:|----------:|----------:|------:|--------:|
|           ListIndexer_Int |  72.67 us | 0.2388 us | 0.2117 us |  72.63 us |  1.00 |    0.00 |
|         PooledIndexer_Int | 156.03 us | 3.0528 us | 5.1838 us | 153.18 us |  2.16 |    0.08 |
|    PooledIndexer_Span_Int |  14.43 us | 0.0531 us | 0.0497 us |  14.44 us |  0.20 |    0.00 |
|        ListIndexer_String |  53.38 us | 0.2564 us | 0.2398 us |  53.36 us |  0.73 |    0.00 |
|      PooledIndexer_String | 179.91 us | 3.3966 us | 3.1772 us | 180.58 us |  2.48 |    0.05 |
| PooledIndexer_Span_String |  13.36 us | 0.0846 us | 0.0791 us |  13.37 us |  0.18 |    0.00 |
