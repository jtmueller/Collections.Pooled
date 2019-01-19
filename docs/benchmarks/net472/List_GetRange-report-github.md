``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|         Method |       Mean |     Error |    StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------- |-----------:|----------:|----------:|------:|------------:|------------:|------------:|--------------------:|
|   ListGetRange | 2,145.4 ms | 15.809 ms | 14.788 ms |  1.00 | 803000.0000 | 677000.0000 | 649000.0000 |        2913165048 B |
| PooledGetRange |   938.8 ms |  3.882 ms |  3.631 ms |  0.44 |           - |           - |           - |                   - |
