``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  InvocationCount=1  
UnrollFactor=1  

```
|          Method | InitialSetSize |     Mean |    Error |   StdDev | Ratio | RatioSD | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|---------------- |--------------- |---------:|---------:|---------:|------:|--------:|------------:|------------:|------------:|--------------------:|
|   HashSet_Clear |         320000 | 381.2 us | 11.83 us | 15.79 us |  1.00 |    0.00 |           - |           - |           - |                   - |
| PooledSet_Clear |         320000 | 474.5 us | 11.49 us | 19.82 us |  1.26 |    0.08 |           - |           - |           - |                   - |
