``` ini

BenchmarkDotNet=v0.11.3, OS=Windows 10.0.17763.288 (1809/October2018Update/Redstone5)
Intel Core i7-6700HQ CPU 2.60GHz (Skylake), 1 CPU, 8 logical and 4 physical cores
  [Host] : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0
  Clr    : .NET Framework 4.7.2 (CLR 4.0.30319.42000), 64bit RyuJIT-v4.7.3260.0

Job=Clr  Runtime=Clr  

```
|                  Method |     Mean |     Error |    StdDev | Ratio | RatioSD |
|------------------------ |---------:|----------:|----------:|------:|--------:|
|   ListIndexOf_ValueType | 13.22 us | 0.2255 us | 0.2109 us |  1.00 |    0.00 |
| PooledIndexOf_ValueType | 12.22 us | 0.0783 us | 0.0612 us |  0.93 |    0.02 |
