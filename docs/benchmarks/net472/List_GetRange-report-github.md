``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.195 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|         Method |          Mean |         Error |        StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------- |--------------:|--------------:|--------------:|------:|------------:|------------:|------------:|--------------------:|
|   ListGetRange | 494,702.73 us | 8,829.5922 us | 8,259.2060 us | 1.000 | 683000.0000 | 552000.0000 | 552000.0000 |        2915638960 B |
| PooledGetRange |      82.32 us |     0.9020 us |     0.7996 us | 0.000 |           - |           - |           - |                   - |
