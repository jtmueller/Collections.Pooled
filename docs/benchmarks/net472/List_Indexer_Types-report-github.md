``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                    Method |     Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------------- |---------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|           ListIndexer_Int | 66.31 us | 0.1266 us | 0.1184 us |  1.00 |           - |           - |           - |                   - |
|         PooledIndexer_Int | 19.13 us | 0.0378 us | 0.0335 us |  0.29 |           - |           - |           - |                   - |
|    PooledIndexer_Span_Int | 13.15 us | 0.0185 us | 0.0164 us |  0.20 |           - |           - |           - |                   - |
|        ListIndexer_String | 48.78 us | 0.1554 us | 0.1454 us |  0.74 |           - |           - |           - |                   - |
|      PooledIndexer_String | 28.69 us | 0.0823 us | 0.0643 us |  0.43 |           - |           - |           - |                   - |
| PooledIndexer_Span_String | 14.37 us | 0.0360 us | 0.0301 us |  0.22 |           - |           - |           - |                   - |
