``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.292 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3324.0

Job=Clr  Runtime=Clr  

```
|         Method |       Mean |    Error |   StdDev | Ratio | Gen 0/1k Op | Gen 1/1k Op | Gen 2/1k Op | Allocated Memory/Op |
|--------------- |-----------:|---------:|---------:|------:|------------:|------------:|------------:|--------------------:|
|   ListGetRange | 2,004.3 ms | 9.132 ms | 8.095 ms |  1.00 | 864000.0000 | 785000.0000 | 679000.0000 |        2913380832 B |
| PooledGetRange |   896.1 ms | 6.162 ms | 5.462 ms |  0.45 |           - |           - |           - |                   - |
