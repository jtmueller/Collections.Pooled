``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|                    Method |     Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|-------------------------- |---------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|           ListIndexer_Int | 66.37 us | 0.2664 us | 0.2361 us |  1.00 |           - |           - |           - |                   - |
|         PooledIndexer_Int | 19.17 us | 0.0427 us | 0.0399 us |  0.29 |           - |           - |           - |                   - |
|    PooledIndexer_Span_Int | 13.18 us | 0.0327 us | 0.0306 us |  0.20 |           - |           - |           - |                   - |
|        ListIndexer_String | 48.68 us | 0.0652 us | 0.0578 us |  0.73 |           - |           - |           - |                   - |
|      PooledIndexer_String | 28.72 us | 0.0723 us | 0.0676 us |  0.43 |           - |           - |           - |                   - |
| PooledIndexer_Span_String | 13.19 us | 0.0408 us | 0.0382 us |  0.20 |           - |           - |           - |                   - |
